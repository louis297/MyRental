using MyRental.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ResponseModels.MessageResponseModels
{
    public class MessageSendResponseModel: BaseResponseModel
    {
        public MessageCreateDTO MessageContent { get; set; }


    }
}
