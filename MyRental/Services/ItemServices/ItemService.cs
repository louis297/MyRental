using System;
using System.Collections.Generic;
using System.Linq;
using myrental.Models;
using Rental.DTOs.ItemDTOs;

namespace MyRental.Services.ItemServices
{
    public class ItemService
    {

        private MyRentalDbContext context;
        public ItemService()
        {
            context = new MyRentalDbContext();
        }

        public IEnumerable<ItemListDTO> getItemList()
        {
            var items = context.items.Select(item => new ItemListDTO()
            {
                Title = item.Title,
                Detail = item.Detail,
                Price = item.Price,
                PostTime = item.PostTime,
                ExpireTime = item.ExpireTime
                //TODO: add AuthorName field
                //AuthorName = item.Author.userName
            });

            return items;
        }

        public ItemListDTO getItemDetail(int id)
        {
            var items = context.items.Where(i => i.ItemID == id)
                .Select(item => new ItemListDTO()
                {
                    Title = item.Title,
                    Detail = item.Detail,
                    Price = item.Price,
                    PostTime = item.PostTime,
                    ExpireTime = item.ExpireTime
                    //TODO: add AuthorName field
                    //AuthorName = item.Author.userName
                });
            return items.Count() > 0 ? items.First() : null;
        }
    }
}
