using System;
using System.Collections.Generic;

namespace MyRental.DTOs.ItemDTOs
{
    public class ItemCreateDTO
    {
        public string ItemName { get; set; }
        public string Detail { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
