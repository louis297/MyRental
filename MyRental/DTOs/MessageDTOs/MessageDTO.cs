﻿using MyRental.Models.MessageModels;
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
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int ItemID { get; set; }
        public DateTime SentTime { get; set; }
        public int MessageID { get; set; }

        public MessageDTO(MyRentalMessage message)
        {
            Title = message.Title;
            Content = message.Content;
            Sender = message.Sender.UserName;
            Receiver = message.Receiver.UserName;
            ItemID = message.ItemID;
            SentTime = message.SentTime;
            MessageID = message.MessageID;
        }

        public MessageDTO()
        {

        }
    }
}
