using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyRental.Models.UserModel;

namespace MyRental.Models.ItemModels
{
    public class ItemImage
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public byte[] ImageContent { get; set; }
        [Required]
        public string ImageType { get; set; }
        [Required]
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }
    }
}
