import React, { Component } from 'react';
import { Route, Switch } from 'react-router'
import logo from './logo.svg';
import './App.css';
import { Layout } from './components/Layout';
import Home from './components/Home';
import Mylist from './components/Mylist';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Switch>
          <Route exact path='/' component={Home} />
          <Route path='/mylist' component={Mylist} />
        </Switch>
      </Layout> 
    )
  }
}
