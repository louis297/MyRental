import React, { Component } from 'react'
import authService from './api-auth/AuthorizeService'

export default class ItemList extends Component {
  static displayName = ItemList.name;

  constructor(props){
    super(props);
    this.state = {
      items: [],
      loading: true,
      currentUserID: null,
      isLoggedIn: false
    }
    this.renderItemList = this.renderItemList.bind(this);
    this.getCurrentUserID = this.getCurrentUserID.bind(this);
    this.addButton = this.addButton.bind(this);
  }

componentDidMount(){
    this.getCurrentUserID();
    this.getItemList();
  }

  async getCurrentUserID(){
    var loggedIn = await authService.isAuthenticated();
    if(loggedIn){
      const user = await authService.getUser();
      this.setState({currentUserID: user.sub, isLoggedIn: true})
    }
  }

  async getItemList() {
    const response = await fetch('api/item');
    const data = await response.json();
    this.setState({ items: data, loading: false })
  }

  addButton = (itemID, authorID) => {
    if(this.state.isLoggedIn && this.state.currentUserID !== authorID){
      return (
      <td>
        <button className="btn btn-sm btn-success">Like</button> 
        <button className="btn btn-sm btn-primary">Contact</button>
      </td>
      )
    } else {
      return (<td></td>)
    }
  }

  renderItemList(items){
    return (
      <table className='table' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Item Name</th>
            <th>Detail</th>
            <th>Owner</th>
            <th>Expire time</th>
            <th>Price</th>
            <th>Like/Contact</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
          <tr key={item.itemID}>
            <td>{item.itemName}</td>
            <td>{item.detail}</td>
            <td>{item.authorName}</td>
            <td>{item.expireTime}</td>
            <td>{item.price}</td>
            {this.addButton(item.itemID, item.authorID)}
          </tr>
          )}
        </tbody>
      </table>
    )
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderItemList(this.state.items)
    return (
      <div>
        <h1 id="tableLabel">Item List</h1>
        {contents}
      </div>
    )
  }
}
