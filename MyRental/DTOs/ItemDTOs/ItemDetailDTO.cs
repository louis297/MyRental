using System;
namespace MyRental.DTOs.ItemDTOs
{
    public class ItemDetailDTO
    {
        public string ItemName { get; set; }
        public string Detail { get; set; }
        //TODO: Add AuthorName field after User authentication implemented
        //public string AuthorName { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
    }
}
