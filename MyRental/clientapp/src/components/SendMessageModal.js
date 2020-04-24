import React, { Component, useState } from 'react'
import { Modal, Button, ModalFooter, ModalBody, ModalHeader } from 'reactstrap'
import axios from 'axios'
import authService from './api-auth/AuthorizeService'

export default function SendMessageModal(props) {
  const [modalShow, setModalShow] = useState(false);
  const toggle = () => setModalShow(!modalShow);
  const itemID = props.itemID; 
  const receiverID = props.receiverID;


  const MessageModal = (props) => {
    return (
      <Modal
        {...props}
        size="lg"
        aria-labelledby="message-modal"
        centered
        >
        <ModalHeader toggle={toggle}>
            Send Message
        </ModalHeader>

        <SendMessageForm
          toggle = {toggle}
          itemID = {itemID}
          receiverID = {receiverID}
        />

      </Modal>
    );
  }

  return (
    <>
    <MessageModal
      isOpen={modalShow}
      toggle={toggle}
    />
    <Button color="primary" className="btn-sm" key={itemID} onClick={toggle}>
      Contact
    </Button>        
  </>
  )
}

function SendMessageForm(props) {
  // state should be in this child component to avoid re-rendering the whole modal
  const [title,setTitle] = useState('');
  const [content,setContent] = useState('');

  const SendMessage = async () => {
    const token = await authService.getAccessToken();
    // const user = await authService.getUser();
    const data = {
      title: title,
      content: content,
      receiverID: props.receiverID,
      itemID: props.itemID
    }
    axios({
      url:"/api/message/send", 
      method: 'post',
      data: data,
      headers: {
        'content-type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      if(response.data.isSuccess === true){
        window.location.href='/';
      } else {
        console.log(response.data.message);
      }
    })
  }

  return (
    <div>
      <ModalBody>
      <form>
        <div className="form-group">
          <label htmlFor="messageTitle">Title:</label>
          <input key='messageTitle' type="text" className="form-control" id="messageTitle" name="messageTitle" value={title} 
            onChange={e => setTitle(e.target.value)}/>
        </div>
        <div className="form-group">
          <label htmlFor="messageContent">Content:</label>
          <textarea key='messageContent' type="text" cols="60" rows="8" className="form-control" id="messageContent" name="messageContent" value={content} 
            onChange={e => setContent(e.target.value)}/>
        </div>
      </form>
      </ModalBody>
      <ModalFooter>
        <Button 
          color="primary" 
          onClick={SendMessage} 
        >
          Send
        </Button>
        <Button color="outline-danger" onClick={props.toggle}>Close</Button>
      </ModalFooter>
      
    </div>
  )
}
