import { UserManager, WebStorageStateStore } from 'oidc-client';
import { ApplicationPaths, ApplicationName } from './Constants';

export class AuthorizeService {
  _callbacks = [];
  _nextSuscriptionId = 0;
  _user = null;
  _isAuthenticated = false;

  // not properly on Edge (comment by VS default)
  _popUpDisabled = true;

  async isAuthenticated() {
    const user = await this.getUser();
    return !!user;
  }

  async getUser(){
    if (this._user && this._user.profile) {
      return this._user._profile;
    }

    await this.ensureUsermanagerInitialized();
    const user = await this.userManager.getuUser();
    return user && user.profile;
  }

  async getAccessToken() {
    await this.ensureUsermanagerInitialized();
    const user = await this.userManager.getUser();
    return user && user.access_token;
  }

  // 3 ways of auth:
  // 1. if already logged in
  // 2. popup, when it is not disabled
  // 3. redirect to login
  async signIn(state) {
    await this.ensureUsermanagerInitialized();
    try {
      const silentUser = await this.userManager.signinSilent(this.createArguments());
      this.updateState(silentUser);
      return this.success(state);
    } catch (silentError) {
      // TODO: may remove the output
      console.log("Silent authentication error: ", silentError);
      try {
        if (this._popUpDisabled) {
          throw new Error('Popup disabled.')
        }
        const popUpUser = await this.userManager.signinPopup(this.createArguments());
        this.updateState(popUpUser);
        return this.success(state);
      } catch (popUpError) {
        if (popUpError.message === 'Popup window closed') {
          return this.error("The user closed the window.");
        } else if (!this._popUpDisabled) {
          // TODO: may remove the output
          console.log("Popup authentication error: ", popUpError);
        }

        try {
          await this.userManager.signinRedirect(this.createArguments(state));
          return this.redirect();
        } catch (redirectError) {
          // TODO: may remove the output
          console.log("Redirect authentication error: ", redirectError);
          return this.error(redirectError)
        }
      }
    }
  }

  async completeSignIn(url) {
    try {
      await this.ensureUsermanagerInitialized();
      const user = await this.userManager.signinCallback(url);
      this.updateState(user);
      return this.success(user && user.state);
    } catch (error) {
      return this.error('There was an error signing in.')
    }
  }

  // 2 way of signout
  // 1. signout with popup window if not disabled
  // 2. redirect to signout
  async signOut(state){
    await this.ensureUsermanagerInitialized();
    try {
      if (this._popUpDisabled) {
        throw new Error('Popup disabled.')
      }
      await this.userManager.signoutPopup(this.createArguments());
      this.updateState(undefined);
      return this.success(state);
    } catch (popupSignOutError) {
      try {
        await this.userManager.signoutRedirect(this.createArguments(state));
        return this.redirect();
      } catch (redirectSignOutError) {
        return this.error(redirectSignOutError);
      }
    }
  }

  async completeSignOut(url){
    await this.ensureUsermanagerInitialized();
    try {
      const response = await this.userManager.signoutCallback(url);
      this.updateState(null);
      return this.success(response && response.data);
    } catch (error) {
      return this.error(error);
    }
  }

  updateState(user){
    this._user = user;
    this._isAuthenticated = !!this._user;
    this.notifySubscribers();
  }

  subscribe(callback){
    this._callback.push({ callback, subscription: this._nextSuscriptionId++});
    return this._nextSuscriptionId-1;
  }

  unsubscribe(subscriptionId) {
    const subscriptionIndex = this._callbacks
      .map((element, index) => element.suscription === subscriptionId? { found: true, index} : { found: false })
      .filter(element => element.found === true);
    if (subscriptionIndex.length !== 1){
      throw new Error(`Found an invalid number of subscriptions ${subscriptionIndex.length}`);
    }
  }

  notifySubscribers() {
    for (let i=0; i<this._callbacks.length; i++){
      const callback = this._callbacks[i].callback;
      callback();
    }
  }

  createArguments(state){
    return { useReplaceToNavigate: true, data: state };
  }

  error(message){
    return { status: AuthenticationResultStatus.Fail, message };
  }

  success(state){
    return { status: AuthenticationResultStatus.Success, state };
  }

  redirect() {
    return { status: AuthenticationResultStatus.Redirect };
  }

  async ensureUsermanagerInitialized() {
    if(this.userManager !== undefined) {
      return;
    }
    let response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
    if(!response.ok) {
      throw new Error(`Count not load settings for '${ApplicationName}`);
    }
    let settings = await response.json();
    settings.automaticSilentRenew = true;
    settings.includeIdTokenInSilentRenew = true;
    settings.userStore = new WebStorageStateStore({
      prefix: ApplicationName
    });
    this.userManager = new UserManager(settings);

    this.userManager.events.addUserSignedOut( async () => {
      await this.userManager.removeUser();
      this.updateState();
    })
  }
  static get instance() { return authService }
}

const authService = new AuthorizeService();
export default authService;

export const AuthenticationResultStatus = {
  Redirect: 'redirect',
  Success: 'success',
  Fail: 'fail'
}