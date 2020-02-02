import React, { Component } from 'react';
import { Route, Switch } from 'react-router'
import logo from './logo.svg';
import './App.css';
import { Layout } from './components/Layout';
import Home from './components/Home';
import Mylist from './components/Mylist';
import { ApplicationPaths } from './components/api-auth/Constants';
import ApiAuthorizationRoutes from './components/api-auth/ApiAuthorizationRoutes'
import AuthorizeRoute from './components/api-auth/AuthorizeRoute'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        
        <Route exact path='/' component={Home} />
        <AuthorizeRoute path='/mylist' component={Mylist} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout> 
    )
  }
}
