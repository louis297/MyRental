using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRental.Models.ItemModels
{
    public class Item
    {
        //public Item()
        //{

        //}
        [Key]
        public int ItemID { get; set; }
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        [Required]
        [StringLength(1000)]
        public string Detail { get; set; }
        [Required]
        public DateTime PostTime { get; set; }
        [Required]
        public DateTime ExpireTime { get; set; }
        [Required]
        public int Price { get; set; }
        
        public List<ItemImage> itemImages { get; set; }

        //TODO: Add User related fields
    }
}
