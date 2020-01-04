using System;
namespace Rental.DTOs.ItemDTOs
{
    public class ItemListDTO
    {
        //public ItemListDTO()
        //{
        //}

        public string Title { get; set; }
        public string Detail { get; set; }
        //TODO: Add AuthorName field after User authentication implemented
        //public string AuthorName { get; set; }
        public DateTime PostTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Price { get; set; }
    }
}
