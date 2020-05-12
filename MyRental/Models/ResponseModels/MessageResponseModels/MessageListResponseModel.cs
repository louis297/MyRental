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

        public IEnumerable<MessageDTO> MessageList { get; set; }

        public MessageListResponseModel()
        {

        }

    }
}
