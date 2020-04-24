using Microsoft.EntityFrameworkCore;
using MyRental.DTOs.MessageDTOs;
using MyRental.Models;
using MyRental.Models.MessageModels;
using MyRental.Models.UserModel;
using MyRental.MyRentalExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private MyRentalDbContext context;
        public MessageService(MyRentalDbContext c)
        {
            context = c;
        }
        public IEnumerable<MyRentalMessage> GetMessageList(int ItemID)
        {
            var messages = context
                        .messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Include(m => m.TargetItem)
                        .Where(m => m.ItemID == ItemID);
            return messages;
        }

        public MyRentalMessage GetMessageByID(int messageID)
        {
            var message = context
                        .messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Include(m => m.TargetItem)
                        .Where(m => m.MessageID == messageID)
                        .FirstOrDefault();
            return message;
        }

        public MyRentalMessage SendMessage(MessageSendDTO newMessage, ApplicationUser user)
        {
            var userId = user.Id;
            var receiver = context
                .Users
                .Where(u => u.Id.Equals(newMessage.ReceiverID))
                .FirstOrDefault();
            if(receiver == null)
            {
                throw new UserCheckException();
            }

            MyRentalMessage message = new MyRentalMessage
            {
                SenderID = userId,
                ReceiverID = newMessage.ReceiverID,
                Title = newMessage.Title,
                Content = newMessage.Content,
                ItemID = newMessage.ItemID
            };
            context.messages.Add(message);
            context.SaveChanges();
            return message;
        }
    }
}
