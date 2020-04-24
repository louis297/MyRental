import React, { Component } from 'react'
import { Route, Switch } from 'react-router'
import logo from './logo.svg'
import './App.css'
import { Layout } from './components/Layout'
import Home from './components/Home'
import Mylist from './components/Mylist'
import AddItem from './components/AddItem'
import ModifyItem from './components/ModifyItem'
import { ApplicationPaths } from './components/api-auth/Constants'
import ApiAuthorizationRoutes from './components/api-auth/ApiAuthorizationRoutes'
import AuthorizeRoute from './components/api-auth/AuthorizeRoute'
import MyArchiveList from './components/MyArchivedList'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        
        <Route exact path='/' component={Home} />
        <AuthorizeRoute path='/mylist' component={Mylist} />
        <AuthorizeRoute path='/myarchivedlist' component={MyArchiveList} />
        <AuthorizeRoute path='/newitem' component={AddItem} />
        <AuthorizeRoute path='/updateitem/:id' component={ModifyItem} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout> 
    )
  }
}
