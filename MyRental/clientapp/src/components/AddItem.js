import React, { Component } from 'react'
import { post } from 'axios';

export default class AddItem extends Component {
  static displayName = AddItem.name;
  
  constructor(props) {
    super(props);
    this.state = {
      itemName: '',
      itemDetail: '',
      price: 0,
      expireTime: '',
      images: null
    };

    this.onClickHandler = this.onClickHandler.bind(this);
    this.onChangeHandler = this.onChangeHandler.bind(this);
  }

  onChangeHandler=event=>{
    const target = event.target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  }

  onClickHandler = () => {
    // const data = new FormData()
    // for(var x = 0; x<this.state.selectedFile.length; x++) {
    //     data.append('file', this.state.selectedFile[x])
    // }
    // this.setState({images: data});
    post("/api/item/upload", this.state, { 
       'content-type': 'multipart/form-data'
    })
 
    .then(res => { // then print response status
     console.log(res.statusText)
    })
  }
  render() {
    return (
      <div>
        <h1>Add New Item</h1>
        <form id='uploadForm' onSubmit={this.onClickHandler}>
          <div className="form-group">
            <label htmlFor="itemName">Item name:</label>
            <input key='itemName' type="text" className="form-control" id="itemName" name="itemName" value={this.state.itemName} 
              onChange={this.onChangeHandler} />
          </div>
          <div className="form-group">
            <label htmlFor="itemDetail">Item details:</label>
            <textarea key='itemDetail' cols="80" rows="10" className="form-control" id="itemDetail" name="itemDetail" value={this.state.itemDetail} 
              onChange={this.onChangeHandler} />
          </div>
          <div className="form-group">
            <label htmlFor="images">Select images(optional):</label>
            <input key='images' type="file" className="form-control" id="images" name="images" multiple onChange={this.onChangeHandler}/>
          </div>

          <div className="container">
            <div className="row">
            <div className="form-group col-6">
              <label htmlFor="price">Price:</label>
              <input key='price' type="number" className="form-control" id="price" name="price" alue={this.state.price} 
                onChange={this.onChangeHandler} />
            </div>
            <div className="form-group col-6">
              <label htmlFor="expireTime">Expire date:</label>
              <input key='expireTime' type="datetime-local" className="form-control" id="expireTime" name="expireTime" value={this.state.expireTime} 
                onChange={this.onChangeHandler}/>
            </div>
            </div>
          </div>

          <input type="submit" className="btn btn-primary" value="Add new item" />
          <input type="button" id="resetBtn" className="btn btn-outline-danger" value="Clear images" 
            onClick={()=>{document.getElementById('uploadForm').reset(); }}/>
        </form>
      </div>
    );
    
  }
}
