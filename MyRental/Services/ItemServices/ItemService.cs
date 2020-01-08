using System;
using System.Collections.Generic;
using System.Linq;
using myrental.Models;
using Rental.DTOs.ItemDTOs;

namespace MyRental.Services.ItemServices
{
    public class ItemService : IItemService
    {

        private MyRentalDbContext context;
        public ItemService()
        {
            context = new MyRentalDbContext();
        }

        public IEnumerable<ItemListDTO> GetItemList()
        {
            var items = context.items.Select(item => new ItemListDTO()
            {
                ItemName = item.ItemName,
                Detail = item.Detail,
                Price = item.Price,
                PostTime = item.PostTime,
                ExpireTime = item.ExpireTime
                //TODO: add AuthorName field
                //AuthorName = item.Author.userName
            });

            return items;
        }

        public ItemListDTO GetItemDetailById(int id)
        {
            var items = context.items.Where(i => i.ItemID == id)
                .Select(item => new ItemListDTO()
                {
                    ItemName = item.ItemName,
                    Detail = item.Detail,
                    Price = item.Price,
                    PostTime = item.PostTime,
                    ExpireTime = item.ExpireTime
                    //TODO: add AuthorName field
                    //AuthorName = item.Author.userName
                });
            return items.Count() > 0 ? items.First() : null;
        }

        public void DeleteItemById(int id)
        {

        }

        public void CreateItem(string value)
        {

        }
    }
}
