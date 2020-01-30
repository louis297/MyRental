using System;
using MyRental.Models.ItemModels;

namespace MyRental.DTOs.ItemDTOs
{
    public class ItemListDTO
    {
        public ItemListDTO()
        {
        }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Detail { get; set; }
        //TODO: Add AuthorName field after User authentication implemented
        //public string AuthorName { get; set; }
        public string PostTime { get; set; }
        public string ExpireTime { get; set; }
        public int Price { get; set; }

        public ItemListDTO(Item item)
        {
            ItemID = item.ItemID;
            ItemName = item.ItemName;
            Detail = item.Detail;
            Price = item.Price;
            PostTime = item.PostTime.ToString();
            ExpireTime = item.ExpireTime.ToString();
        }
    }
}
