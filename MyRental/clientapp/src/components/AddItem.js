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
    if(this.state.uploading){
      alert('Another file is uploading, please wait for a while');
      return;
    }
    this.state.uploading = true;
    post("/api/item/uploadimage", event.target.files[0], { 
      'content-type': 'multipart/form-data'
    })
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

  onClickHandler = (event) => {
    // const data = new FormData()
    // for(var x = 0; x<this.state.selectedFile.length; x++) {
    //     data.append('file', this.state.selectedFile[x])
    // }
    // this.setState({images: data});
    if(this.state.uploading){
      event.preventDefault();
      alert('Image is uploading, please wait for a while');
      return;
    }
    var data = {
      itemName: this.state.itemName,
      itemDetail: this.state.itemDetail,
      price: this.state.price,
      expireTime: this.state.expireTime,
      images: this.state,images
    };
    post("/api/item/upload", data, { 
       'content-type': 'application/json'
    })
    .then(res => {
      if(response.data.isSuccess === 'true'){
        window.location.href('/mylist');
    } else {

    }
    })
  }
  render() {
    let filelist = <div></div>;
    if(this.state.filenames != []){
      filelist = <ul>
        {this.state.filenames.map((value, index)=>{
          return <li key={value.id}>{value.filename}</li>
        })}
      </ul>
    }
    let uploadErrorComponent = <div></div>;
    if(this.state.uploadError){
      uploadErrorComponent = <p className="text-danger">this.state.uploadErrorMsg</p>
    }
    let submitErrorComponent = <div></div>;
    if(this.state.submitError){
      submitErrorComponent = <p className="text-danger">this.state.submitErrorMsg</p>
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
            <label htmlFor="itemDetail">Item details:</label>
            <textarea key='itemDetail' cols="80" rows="10" className="form-control" id="itemDetail" name="itemDetail" value={this.state.itemDetail} 
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
