using MyRental.DTOs.MessageDTOs;
using MyRental.Models.MessageModels;
using MyRental.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Services.MessageServices
{
    public interface IMessageService
    {
        public IEnumerable<MyRentalMessage> GetMessageList(int ItemID);
        public MyRentalMessage SendMessage(MessageSendDTO newMessage, ApplicationUser user);
        public MyRentalMessage GetMessageByID(int messageID);
    }
}
