import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './Constants';

export default class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null
    }
  }
  
  componentDidMount(){
    this._subscription = authService.subscribe( () => this.populateState() );
    this.populateState();
  }

  componentWillMount(){
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);
    this.setState({
      isAuthenticated,
      userName: user && user.name
    })
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if(!isAuthenticated) {
      return this.anonymousView(`${ApplicationPaths.Register}`, `${ApplicationPaths.Login}`)
    } else {
      return this.authenticatedView(userName, 
              `${ApplicationPaths.Profile}`, 
              { pathname:`${ApplicationPaths.LogOut}`, state: {local:true} } )
    }
  }

  authenticatedView(userName, profilePath, logoutPath) {
    return (
    <Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={profilePath}>Hello {userName} </NavLink>/>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
      </NavItem>
    </Fragment>)
  }

  anonymousView(registerPath, loginPath){
    return (
      <Fragment>
        <NavItem>
          <NavLink tag={Link} className="text-dark" to={registerPath}>Register</NavLink>
        </NavItem>
        <NavItem>
          <NavLink tag={Link} className="text-dark" to={loginPath}>Login</NavLink>
        </NavItem>
      </Fragment>
    )
  }
}
