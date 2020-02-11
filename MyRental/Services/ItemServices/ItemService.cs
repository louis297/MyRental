using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models;
using MyRental.Models.ItemModels;
using MyRental.Models.UserModel;
using MyRental.MyRentalExceptions;

namespace MyRental.Services.ItemServices
{
    public class ItemService : IItemService
    {

        private MyRentalDbContext context;
        public ItemService(MyRentalDbContext c)
        {
            context = c;
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

        public Item CreateItem(ItemCreateDTO newItem, ApplicationUser user)
        {
            var userId = user.Id;
            Item item = new Item
            {
                ItemName = newItem.ItemName,
                Detail = newItem.Detail,
                Price = newItem.Price,
                ExpireTime = newItem.ExpireTime,
                AuthorID = user.Id
            };
            IList<ItemImage> images = new List<ItemImage>();
            foreach(int imageID in newItem.Images)
            {
                var image = context.itemImages.Where(i => i.ImageId == imageID).First();
                if(image.UserID != userId)
                {
                    throw (new UserCheckException());
                }
                images.Add(image);
            }
            foreach(var image in images)
            {
                item.Images.Add(image);
            }
            context.items.Add(item);
            context.SaveChanges();

            var itemAdded = GetItemDetailById(item.ItemID);
            return itemAdded;

        }

        public int SaveFile(byte[] fileBytes, string contentType, ApplicationUser user)
        {
            ItemImage image = new ItemImage
            {
                ImageContent = fileBytes,
                ImageType = contentType,
                UserID = user.Id
            };
            context.itemImages.Add(image);
            context.SaveChanges();
            return image.ImageId;
        }

        public Item UpdateItem(int itemId, ItemCreateDTO newItem, ApplicationUser user)
        {

            var item = context.items.Where(i => i.ItemID == itemId)
                .FirstOrDefault();
            if(item == null)
            {
                return null;
            }
            if (!item.AuthorID.Equals(user.Id))
            {
                throw new UserCheckException();
            }

            item.Detail = newItem.Detail;
            item.ItemName = newItem.ItemName;
            item.ExpireTime = newItem.ExpireTime;
            item.Price = newItem.Price;
            // remove previous image links
            //foreach(var itemImage in item.itemImages)
            //{
            //    context.itemImages.Remove(itemImage);
            //}
            //// TODO: Remove previous images on disk

            //// add new image links
            //foreach (string imageUrl in newItem.Images)
            //{
            //    ItemImage itemImage = new ItemImage
            //    {
            //        ImagePath = imageUrl,
            //        ItemId = item.ItemID,
            //    };
            //    context.itemImages.Add(itemImage);
            //}

            context.SaveChanges();
            var itemUpdated = GetItemDetailById(item.ItemID);
            return itemUpdated;

        }

    }
}
