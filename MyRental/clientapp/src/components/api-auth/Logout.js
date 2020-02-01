import React, { Component } from 'react'
import authService, { AuthenticationResultStatus } from './AuthorizeService'
import { LogoutActions, QueryParameterNames, ApplicationPaths } from './Constants'

export default class Logout extends Component {
  constructor(props){
    super(props);
    this.state = {
      message: undefined,
      isReady: false,
      authenticated: false
    }
  }

  componentDidMount(){
    const action = this.props.action;
    switch(action){
      case LogoutActions.Logout:
        if (!!window.history.state.state.local) {
          this.logout(this.getReturnUrl());
        } else {
          // prevent regular links to /authentication/logout from triggering a logout
          this.setState({ isReady: true, message: 'The logout was not initiated from within the page.' });
        }
        break;
      case LogoutActions.LogoutCallback:
        this.processLogoutCallback();
        break;
      case LogoutActions.LoggedOut:
        this.setState({ isReady: true, message: 'You successfully logged out.' });
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
    this.populateAuthenticationState();
  }

  render() {
    const { isReady, message } = this.state;
    if(!isReady) {
      return <div></div>
    }
    if(!!message){
      return (<div>{message}</div>)
    } else {
      const action = this.props.action;
      switch(action){
        case LogoutActions.Logout:
          return (<div>Processing logout</div>);
        case LogoutActions.LogoutCallback:
          return (<div>Processing logout callback</div>)
        case LogoutActions.LoggedOut:
          return (<div>{message}</div>);
        default:
          throw new Error(`Invalid action '${action}'`);
      }
    }
  }

  async logout(returnUrl){
    const state = { returnUrl };
    const authenticated = await authService.isAuthenticated();
    if (authenticated) {
      const result = await authService.signOut(state);
      switch (result.status){
        case AuthenticationResultStatus.Redirect:
          break;
        case AuthenticationResultStatus.Success:
          await this.navigateToReturnUrl(returnUrl);
          break;
        case AuthenticationResultStatus.Fail:
          this.setState({ message: result.message });
          break;
        default:
          throw new Error('Invalid authentication result status.');
      } else {
        this.setState({ message: 'You successfully logged out!' });
      }
    }
  }

  async processLogoutCallback() {
    const url = window.location.href;
    const result = await authService.completeSignOut(url);
    switch(result.status){
      case AuthenticationResultStatus.Redirect:
        // should not be redirect only if in flow
        throw new Error(`Ahould not redirect.`);
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(this.getReturnUrl(result.state));
        break;
      case AuthenticationResultStatus.Fail:
        this.setState({ message: result.message });
        break;
      default:
        throw new Error('Invalid authentication result status');
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ isReady: true, authenticated });
  }

  getReturnUrl(state){
    const params = new URLSearchParams(window.location.search);
    const fromQuery = params.get(QueryParameterNames.ReturnUrl);
    if(fromQuery && !fromQuery.startsWith(`${windows.location.origin}/`)){
      // check if redirect url is the same within the same site
      throw new Error('Invalid return url. The return url should be the same origin as the current page.')
    }
    return (state && state.returnUrl) || fromQuery || `${window.location.origin}${ApplicationPaths.LoggedOut}`;
  }

  navigateToReturnUrl(returnUrl) {
    return window.location.replace(returnUrl);
  }
}
