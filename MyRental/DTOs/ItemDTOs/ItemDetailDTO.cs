using System;
using System.Collections.Generic;
using MyRental.Models.ItemModels;
using MyRental.Models.ResponseModels.ItemResponseModels;

namespace MyRental.DTOs.ItemDTOs
{
    public class ItemDetailDTO
    {
        public string ItemName { get; set; }
        public string Detail { get; set; }
        public string AuthorName { get; set; }
        public string AuthorID { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
        public IList<ItemImageResponseModel> Images { get; set; }

        public ItemDetailDTO(Item item)
        {
            ItemName = item.ItemName;
            Detail = item.Detail;
            Price = item.Price;
            PostTime = item.PostTime;
            ExpireTime = item.ExpireTime;
            Active = item.Active;
            AuthorID = item.AuthorID;
            AuthorName = item.Author.UserName;
        }

        public ItemDetailDTO()
        {
            Images = new List<ItemImageResponseModel>();
        }
    }
}
