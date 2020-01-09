using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models;
using MyRental.Models.ItemModels;

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
            var items = context.items
                .Where(item => item.Active)
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

            return items;
        }

        public ItemDetailDTO GetItemDetailById(int id)
        {
            var items = context.items.Where(i => i.ItemID == id)
                .Select(item => new ItemDetailDTO()
                {
                    ItemName = item.ItemName,
                    Detail = item.Detail,
                    Price = item.Price,
                    PostTime = item.PostTime,
                    ExpireTime = item.ExpireTime,
                    Active = item.Active
                    //TODO: add AuthorName field
                    //AuthorName = item.Author.userName
                });
            return items.Count() > 0 ? items.First() : null;
        }

        public void DeleteItemById(int id)
        {

        }

        public string ItemArchive(int id)
        {
            try
            {
                var item = context.items
                    .Where(i => i.ItemID == id)
                    .FirstOrDefault();
                if (item == null)
                {
                    return "{'result':'failed','reason':'Item not found'}";
                }
                else
                {
                    item.Active = false;
                    context.SaveChanges();
                    return "{'result':'success'}";
                }
            } catch
            {
                return "{'result':'failed','reason':'DB error'}";
            }
        }

        public string CreateItem(ItemCreateDTO newItem)
        {
            try
            {
                // validation
                if(newItem.ItemName.Length > 250)
                {
                    return "{'result': 'failed', 'reason': 'Item name is too long'}";
                }
                if(newItem.Detail.Length > 999)
                {
                    return "{'result': 'failed', 'reason': 'Item detail is too long'}";
                }
                if(newItem.Price < 0)
                {
                    return "{'result': 'failed', 'reason': 'Price should not be negative'}";
                }
                if(newItem.ExpireTime < DateTime.Now)
                {
                    return "{'result': 'failed', 'reason': 'Expire time should not be in the past'}";
                }

                Item item = new Item
                {
                    ItemName = newItem.ItemName,
                    Detail = newItem.Detail,
                    Price = newItem.Price,
                    ExpireTime = newItem.ExpireTime
                };
                context.items.Add(item);
                context.SaveChanges();
                //int itemId = context.items.Where(item)
                //IList<ItemImage> imageUrls = new List<ItemImage>();
                foreach (string imageUrl in newItem.ImageUrls)
                {
                    ItemImage itemImage = new ItemImage
                    {
                        ImagePath = imageUrl,
                        ItemId = item.ItemID,
                    };
                    //imageUrls.Add(itemImage);
                    context.itemImages.Add(itemImage);
                }
                context.SaveChanges();
                return $"{{'result': success', 'entity': {JsonSerializer.Serialize(item)}}}";
            }
            catch
            {
                return "{'result': 'failed', 'reason': 'DB error'}";
            }
            
        }
    }
}
