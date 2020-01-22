import React, { Component } from 'react';
import ItemList from './ItemList';

export default class Home extends Component {
  static displayName = Home.name;
  render() {
    return (
      <div>
        <h1>Hello, MyRental</h1>
        <ItemList />
      </div>
    )
  }
}
