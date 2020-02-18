import React, { Component } from 'react'
import axios, { post } from 'axios';
import authService from './api-auth/AuthorizeService'
import { Redirect } from 'react-router';

export default class AddItem extends Component {
  static displayName = AddItem.name;
  
  constructor(props) {
    super(props);
    this.state = {
      itemName: '',
      detail: '',
      price: 0,
      expireTime: '',
      images: [],
      uploadedFilenames: [],
      uploading: false,
      uploadError: false,
      uploadErrorMsg: '',
      submitError: false,
      submitErrorMsg: ''
    };

    this.onClickHandler = this.onClickHandler.bind(this);
    this.onChangeHandler = this.onChangeHandler.bind(this);
    this.imageUploadHandler = this.imageUploadHandler.bind(this);
  }

  onChangeHandler=event=>{
    const target = event.target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  }

  async imageUploadHandler(event){
    // VERY important, without persist event will be re-use and set to null in async function
    event.persist();
    if(this.state.uploading){
      alert('Another file is uploading, please wait for a while');
      return;
    }
    this.state.uploading = true;
    const token = await authService.getAccessToken();
    const formData = new FormData();

    formData.append('body', event.target.files[0]);
    post(
      "/api/item/uploadimage", formData, 
      {
        headers: {
        'content-type': 'multipart/form-data',
        'Authorization': `Bearer ${token}`
        }
      }
    )
    .then(response => {
      this.setState({uploading: false});
      console.log(response.data);
      if(response.data.isSuccess === 'true'){
        // save image index to state
        var newimages = this.state.images;
        var imageIndex = parseInt(response.data.message);
        newimages.push(imageIndex);
        // save local image filename to state
        var filenames = this.state.uploadedFilenames;
        filenames.push({id:imageIndex, filename: event.target.files[0].name});
        this.setState({uploadedFilenames: filenames, images: newimages});
        // clear error message
        this.setState({uploadError: false, uploadErrorMsg: '', uploading:false});
      } else {
        this.setState({uploadError: true, uploadErrorMsg: response.data.message, uploading:false});
      }
    })
    .catch(error=>{
      this.setState({uploadError: true, uploadErrorMsg: 'Unknown server error', uploading:false});
    });
  }

  onClickHandler = async (event) => {
    // VERY important, without persist event will be re-use and set to null in async function
    event.persist();
    event.preventDefault();
    if(this.state.uploading){
      alert('Image is uploading, please wait for a while');
      return;
    }
    const token = await authService.getAccessToken();
    // const formData = new FormData();
    // formData.append('itemName', this.state.itemName);
    // formData.append('detail', this.state.detail);
    // formData.append('price', this.state.price);
    // formData.append('expireTime', this.state.expireTime);
    // formData.append('images', this.state.itemName);
    var data = {
      itemName: this.state.itemName,
      detail: this.state.detail,
      expireTime: this.state.expireTime,
      price: parseInt(this.state.price),
      images: this.state.images
      // images: [1,]
    };
    axios({
      url:"/api/item/upload", 
      method: 'post',
      // data: {itemName:'some str', price: '333', expireTime:"2020-02-29T03:04", images:[1,], detail: 'some detail'}, 
      data: data,
      headers: {
        'content-type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      if(response.data.isSuccess === true){
        window.location.href='/mylist';
      } else {
        console.log(response.data.message);
      }
    })
    // axios({
    //   url:"/api/item/testupload", 
    //   method: 'post',
    //   data: {itemName:'some str', price: 333, expireTime:"2020-02-29T03:04", images:[1,]}, 
    //   headers: {
    //     'content-type': 'application/json',
    //     'Authorization': `Bearer ${token}`
    //   }
    // })
    // .then(response => {
    //   console.log(response.data.message);
    //   event.preventDefault();
    // })
  }
  render() {
    let filelist = <div></div>;
    if(this.state.uploadedFilenames !== null && this.state.uploadedFilenames.length != 0){
      filelist = <ul>
        {this.state.uploadedFilenames.map((value, index)=>{
          return <li key={value.id}>{value.filename}</li>
        })}
      </ul>
    }
    let uploadErrorComponent = <div></div>;
    if(this.state.uploadError){
      uploadErrorComponent = <p className="text-danger">{this.state.uploadErrorMsg}</p>
    }
    let submitErrorComponent = <div></div>;
    if(this.state.submitError){
      submitErrorComponent = <p className="text-danger">{this.state.submitErrorMsg}</p>
    }
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
            <label htmlFor="detail">Item details:</label>
            <textarea key='detail' cols="80" rows="10" className="form-control" id="detail" name="detail" value={this.state.detail} 
              onChange={this.onChangeHandler} />
          </div>
          <div className="form-group">
            <label htmlFor="images">Select images(optional):</label>
            <input key='images' type="file" className="form-control" id="images" name="images" onChange={this.imageUploadHandler}
              disabled={this.state.uploading} />
            {uploadErrorComponent}
            <div>
              {filelist}
            </div>
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
          {submitErrorComponent}
          <input type="submit" className="btn btn-primary" value="Add new item" />
          <input type="button" id="resetBtn" className="btn btn-outline-danger" value="Clear images" 
            onClick={()=>{document.getElementById('uploadForm').reset(); }}/>
        </form>
      </div>
    );
    
  }
}
