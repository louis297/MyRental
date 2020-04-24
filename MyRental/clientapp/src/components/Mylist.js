import React, { Component } from 'react'
import axios from 'axios'
import authService from './api-auth/AuthorizeService'

export default class Mylist extends Component {
  static displayName = Mylist.name;

  constructor(props){
    super(props);
    this.state = {
      items: [],
      loading: true,
      archiving: false
    }
    this.renderItemList = this.renderItemList.bind(this);
    this.toggleArchive = this.toggleArchive.bind(this);
    this.getItemList = this.getItemList.bind(this);
  }

  componentDidMount(){
    this.getItemList(1, 10);
  }

  async getItemList(start, amount) {
    // const response = await fetch(`api/item/mylist?start=${start}&amount=${amount}`);
    // const data = await response.json();
    // this.setState({ items: data, loading: false })
    const token = await authService.getAccessToken();

    axios({
      url: `/api/item/mylist?start=${start}&amount=${amount}`,
      headers: {
      'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      this.setState({ items: response.data, loading: false })
    })
  }

  async toggleArchive(itemID){
    const token = await authService.getAccessToken();
    if(this.state.archiving === true) {
      alert('Item is archiving, please wait for a while...');
      return;
    }

    this.setState({archiving: true}) 
    axios({
      url: `/api/item/archive/${itemID}`,
      method: 'get',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      if(response.data.isSuccess === true){
        document.getElementById(`item${itemID}`).style.display = "none"; 
      } else {
        console.log(response.data.message);
      }
      this.setState({archiving: false}) 
    })
    .catch(response => {
      this.setState({archiving: false}) 
    })
  }

  renderItemList = (items) =>{

    return (
      <table className='table' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Item Name</th>
            <th>Detail</th>
            <th>Expire time</th>
            <th>Price</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
          <tr key={item.itemID} id={'item'+item.itemID}>
            <td>{item.itemName}</td>
            <td>{item.detail}</td>
            <td>{item.expireTime}</td>
            <td>{item.price}</td>
            <td>
              <button className='btn btn-danger' onClick={ () => this.toggleArchive(item.itemID)}>Archive</button>
              <button className='btn btn-primary' onClick={ () => window.location.href=`/updateitem/${item.itemID}`}>Modify</button>
            </td>
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
