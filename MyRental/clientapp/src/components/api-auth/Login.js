import React, { Component } from 'react'
import authService, { AuthenticationResultStatus } from './AuthorizeService'
import { LoginActions, QueryParameterNames, ApplicationPaths } from './Constants'

// Any component need authentication could simply redirect to this component with returnUrl parameter
export default class Login extends Component {
  constructor(props){
    super(props);
    this.state = {
      message: undefined
    }
  }

  componentDidMount(){
    const action = this.props.action;
    switch (action) {
      case LoginActions.Login:
        this.login(this.getReturnUrl());
        break;
      case LoginActions.LoginCallback:
        this.processLoginCallback();
        break;
      case LoginActions.LoginFailed:
        const params = new URLSearchParams(window.location.search);
        const error = params.get(QueryParameterNames.Message);
        this.setState({ message: error });
        break;
      case LoginActions.Profile:
        this.redirectToProfile();
        break;
      case LoginActions.Register:
        this.redirectToRegister();
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }
  render() {
    const action = this.props.action;
    const { message } = this.state;
    if (!!message) {
      return <div>{message}</div>
    } else {
      switch (action) {
        case LoginActions.Login:
          return (<div>Processing login</div>);
        case LoginActions.LoginCallback:
          return (<div>Processing login callback</div>);
        case LoginActions.Profile:
        case LoginActions.Register:
          return (<div></div>)
        default:
          throw new Error(`Invalid action '${action}'`);
      }
    }

  }

  async login(returnUrl){
    const state = { returnUrl };
    const result = await authService.signIn(state);
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
        throw new Error(`Invalid status result ${result.status}.`);
    }
  }

  async processLoginCallback() {
    const url = window.location.href;
    const result = await authService.completeSignIn(url);
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        // redirect should only exist in flow
        throw new Error('Should not redirect.');
      case AuthenticationResultStatus.Success:
        await this.navigateToReturnUrl(this.getReturnUrl(result.state));
        break;
      case AuthenticationResultStatus.Fail:
        this.setState({ message: result.message });
        break;
      default:
        throw new Error(`Invalid authentication result status '${result.status}'`);
    }
  }

  getReturnUrl(state){
    const params = new URLSearchParams(window.location.search);
    const fromQuery = params.get(QueryParameterNames.ReturnUrl);
    if(fromQuery && !fromQuery.startsWith(`${windows.location.origin}/`)){
      // check if redirect url is the same within the same site
      throw new Error('Invalid return url. The return url should be the same origin as the current page.')
    }
    return (state && state.returnUrl) || fromQuery || `${window.location.origin}/`;
  }

  redirectToRegister(){
    this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?${QueryParameterNames.ReturnUrl}=${encodeURI(ApplicationPaths.Login)}`);
  }

  redirectToProfile(){
    this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
  }

  redirectToApiAuthorizationPath(apiAuthorizationPath){
    const redirectUrl = `${window.location.origin}${apiAuthorizationPath}`;
    // Avoid "back" to the endpoint on this component
    window.location.replace(redirectUrl);
  }

  navigateToReturnUrl(returnUrl){
    // Remove the callback uri with the fragment containing the tokens from browser history
    window.location.replace(returnUrl);
  }
}
