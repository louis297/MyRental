using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRental.Models.ItemModels
{
    public class ItemImage
    {
        public int ItemId { get; set; }
        public string ImagePath { get; set; }
        //[ForeignKey("Item")]
        //public Item ItemFK { get; set; }
    }
}
