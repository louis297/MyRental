import React, { Component } from 'react'

export default class ItemList extends Component {
  static displayName = ItemList.name;

  constructor(props){
    super(props);
    this.state = {
      items: [],
      loading: true
    }
  }

  componentDidMount(){
    this.getItemList();
  }

  async getItemList() {
    const response = await fetch('api/item');
    const data = await response.json();
    this.setState({ items: data, loading: false })
  }

  static renderItemList(items){
    return (
      <table className='table' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Item Name</th>
            <th>Detail</th>
            <th>Expire time</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
          <tr key={item.itemID}>
            <td>{item.itemName}</td>
            <td>{item.detail}</td>
            <td>{item.expireTime}</td>
            <td>{item.price}</td>
          </tr>
          )}
        </tbody>
      </table>
    )
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : ItemList.renderItemList(this.state.items)
    return (
      <div>
        <h1 id="tableLabel">Item List</h1>
        {contents}
      </div>
    )
  }
}
