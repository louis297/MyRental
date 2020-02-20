using MyRental.Models.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.MessageModels
{
    public class MyRentalMessage
    {
        [Key]
        public int MessageID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime SentTime { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
