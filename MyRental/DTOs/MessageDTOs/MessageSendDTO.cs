using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.DTOs.MessageDTOs
{
    public class MessageSendDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ReceiverID { get; set; }
        public int ItemID { get; set; }

        public MessageSendDTO()
        {

        }
    }
}
