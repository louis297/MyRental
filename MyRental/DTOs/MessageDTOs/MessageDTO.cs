using MyRental.Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public int ItemID { get; set; }
        public DateTime SentTime { get; set; }

        public MessageDTO(MyRentalMessage message)
        {
            Title = message.Title;
            Content = message.Content;
            SenderID = message.SenderID;
            ReceiverID = message.ReceiverID;
            ItemID = message.ItemID;
            SentTime = message.SentTime;
        }

        public MessageDTO()
        {

        }
    }
}
