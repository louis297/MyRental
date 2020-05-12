import React, {useState, useEffect} from 'react'
import axios from 'axios'
import authService from './api-auth/AuthorizeService'
import { Container,Row,Col,ListGroup,ListGroupItem,Button } from 'reactstrap'
import '../css/ItemDetail.css'

export default function ItemDetail(props) {
  const itemID = props.match.params.id;
  const [pageTitle, setPageTitle] = useState(`Item Detail of ${itemID}`);
  const [itemContents, setItemContents] = useState(<p><em>Loading...</em></p>);
  const [messageContents, setMessageContents] = useState(<p><em>Loading message list...</em></p>);
  const [currentUserID, setCurrentUserID] = useState("");
  
  const renderItemDetail = async () => {
    const token = await authService.getAccessToken();
    const user = await authService.getUser();
    setCurrentUserID(user.sub);
    axios({
      url: `/api/item/${itemID}`,
      method: 'get',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      if(response.data.isSuccess === true){
        ItemDetailComponent(response.data.itemDetail);
      } else {
        console.log(response);
        setItemContents(<p><em>Get Item detail failed...<br/>Please refresh the page</em></p>);
      }      
    })
    .catch(response => {
      console.log(response)
      setItemContents(<p><em>Get Item detail failed...<br/>Please refresh the page</em></p>);
    })
  }
  
  const renderMessageList = async () => {
    const token = await authService.getAccessToken();
    axios({
      url: `/api/message/${itemID}`,
      method: 'get',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })
    .then(response => {
      console.log(response)
      if(response.data.isSuccess === true){
        MessageListComponent(response.data.messageList);
      } else {
        console.log(response);
        setMessageContents(<p><em>Get Item detail failed...<br/>Please refresh the page</em></p>);
      }      
    })
    .catch(response => {
      console.log(response)
      setMessageContents(<p><em>Get Item detail failed...<br/>Please refresh the page</em></p>);
    })
  }

  const ItemDetailComponent = (itemDetail) => {
    setPageTitle(itemDetail.itemName);
    const authorComponent = <p>Author: {itemDetail.authorName}</p>;
    var postTime = new Date(itemDetail.postTime);
    const postTimeComponent = <p>Post: {postTime.toString()}</p>;
    var expireTime = new Date(itemDetail.expireTime);
    const expireTimeComponent = <p>Expire: {expireTime.toString()}</p>
    const detailComponent = <p>{itemDetail.detail}</p>;
    const imageThumbnailComponent = <div></div>
    const imageLargeComponent = <div></div>
    
    const itemComponent = <div>
      {authorComponent}
      {postTimeComponent}
      {expireTimeComponent}
      {detailComponent}
      {imageThumbnailComponent}
      {imageLargeComponent}
    </div>
    setItemContents(itemComponent);
  }

  const MessageListComponent = (messageList) => {
    let s = <div>
      <ListGroup >
      {messageList.map(message => 
        <ListGroupItem key={message.messageID} >
          <Container>
            <Row>
              <Col xs="12" lg="4">From: {message.sender}</Col>
              <Col xs="12" lg="4">To: {message.receiver}</Col>
            </Row>
            <h4>{message.title}</h4>
            <p>{message.content}</p>
          </Container>
        </ListGroupItem>
        )}
      </ListGroup>
    </div>
    setMessageContents(s);
  }

  const SendMessageComponent = currentUserID === "" ?
    <div></div> :
    <div>
      <Button>Send Message</Button>
    </div>;

  useEffect(()=> {

    renderItemDetail();
    renderMessageList();
  },[]);

  return (
    <Container>
      <h1 id="tableLabel">{pageTitle}</h1>
      {itemContents}
      {messageContents}
    </Container>
  )
}
