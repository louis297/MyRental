using MyRental.DTOs.MessageDTOs;
using MyRental.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ResponseModels.MessageResponseModels
{
    public class MessageListResponseModel: BaseResponseModel
    {
        //public string Title { get; set; }
        //public string Content { get; set; }
        //public string SenderID { get; set; }
        //public string ReceiverID { get; set; }
        //public int ItemID { get; set; }
        //public DateTime SentTime { get; set; }
        public IEnumerable<MessageDTO> MessageList { get; set; }

        public MessageListResponseModel()
        {

        }

        //public MessageResponseModel(MyRentalMessage message)
        //{
        //    Title = message.Title;
        //    Content = message.Content;
        //    SenderID = message.SenderID;
        //    ReceiverID = message.ReceiverID;
        //    ItemID = message.ItemID;
        //    SentTime = message.SentTime;
        //}
    }
}
