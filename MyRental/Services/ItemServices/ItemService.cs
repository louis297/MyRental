using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models;
using MyRental.Models.ItemModels;
using MyRental.Models.ResponseModels.ItemResponseModels;

namespace MyRental.Services.ItemServices
{
    public class ItemService : IItemService
    {

        private MyRentalDbContext context;
        public ItemService()
        {
            context = new MyRentalDbContext();
        }

        public IEnumerable<Item> GetItemList()
        {
            var items = context.items
                .Where(item => item.Active);

            return items;
        }

        public IEnumerable<Item> GetItemListByAmount(int start, int amount)
        {
            var items = context.items
                .Where(item => item.Active)
                .Where(i => i.ItemID >= start)
                .OrderBy(i => i.ItemID)
                .Take(amount);

            return items;
        }

        public Item GetItemDetailById(int id)
        {
            var items = context.items.Where(i => i.ItemID == id);
            return items.Count() > 0 ? items.First() : null;
        }

        public void DeleteItemById(int id)
        {

        }

        public Item ItemArchive(int id)
        {
            var item = context.items
                .Where(i => i.ItemID == id)
                .FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            else
            {
                item.Active = false;
                context.SaveChanges();
                return item;
            }
        }

        public Item CreateItem(ItemCreateDTO newItem)
        {
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

            var itemAdded = GetItemDetailById(item.ItemID);
            return itemAdded;

        }

        public Item UpdateItem(int itemId, ItemCreateDTO newItem)
        {

            var item = context.items.Where(i => i.ItemID == itemId)
                .FirstOrDefault();
            if(item == null)
            {
                return null;
            }

            item.Detail = newItem.Detail;
            item.ItemName = newItem.ItemName;
            item.ExpireTime = newItem.ExpireTime;
            item.Price = newItem.Price;
            // remove previous image links
            foreach(var itemImage in item.itemImages)
            {
                context.itemImages.Remove(itemImage);
            }
            // TODO: Remove previous images on disk

            // add new image links
            foreach (string imageUrl in newItem.ImageUrls)
            {
                ItemImage itemImage = new ItemImage
                {
                    ImagePath = imageUrl,
                    ItemId = item.ItemID,
                };
                context.itemImages.Add(itemImage);
            }

            context.SaveChanges();
            var itemUpdated = GetItemDetailById(item.ItemID);
            return itemUpdated;

        }

    }
}
