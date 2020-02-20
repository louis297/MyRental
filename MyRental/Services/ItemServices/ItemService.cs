using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private IEnumerable<Item> CheckUser(IQueryable<Item> items, ApplicationUser user)
        {
            return user == null? items : items.Where(item => item.AuthorID.Equals(user.Id));
        }

        // IMPORTANT: this function is ambigous and dangerous when there are too many items
        public IEnumerable<Item> GetItemList(ApplicationUser user = null)
        {
            var rawItems = context.items
                .Include(item => item.Author)
                .Where(item => item.Active);
            var items = CheckUser(rawItems, user);
            return items;
        }

        public IEnumerable<Item> GetItemListByAmount(int start, int amount, ApplicationUser user=null, bool active = true)
        {
            var rawItems = context.items
                .Where(item => item.Active == active)
                .Where(i => i.ItemID >= start)
                .OrderBy(i => i.ItemID);
            var items = CheckUser(rawItems, user)
                .Take(amount);
            return items;
        }


        public int GetItemAmount(ApplicationUser user = null)
        {
            var rawItems = context.items
                .Where(item => item.Active);
            var count = CheckUser(rawItems, user)
                .Count();
            return count;
        }

        public Item GetItemDetailById(int id)
        {
            var items = context.items
                .Where(i => i.ItemID == id)
                .Include(item => item.Images);
            return items.Count() > 0 ? items.First() : null;
        }

        public void DeleteItemById(int id, ApplicationUser user)
        {

        }

        public Item ItemToggleArchive(int id, ApplicationUser user)
        {
            var item = context.items
                .Where(i => i.ItemID == id)
                .FirstOrDefault();
            if (item == null)
            {
                return null;
            }
            else if(!item.AuthorID.Equals(user.Id)) {
                throw new UserCheckException();
            } 
            else
            {
                item.Active = !item.Active;
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
                AuthorID = user.Id,
                Author = user
            };
            item.Images = new List<ItemImage>();
            foreach(int imageID in newItem.Images)
            {
                var image = context.itemImages.Where(i => i.ImageId == imageID).First();
                if(image.UserID != userId)
                {
                    throw (new UserCheckException());
                }
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
            var userId = user.Id;
            var item = context.items.Where(i => i.ItemID == itemId)
                .FirstOrDefault();
            if(item == null)
            {
                return null;
            }
            if (!item.AuthorID.Equals(userId))
            {
                throw new UserCheckException();
            }

            item.Detail = newItem.Detail;
            item.ItemName = newItem.ItemName;
            item.ExpireTime = newItem.ExpireTime;
            item.Price = newItem.Price;
            item.Images = new List<ItemImage>();
            foreach (int imageID in newItem.Images)
            {
                var image = context.itemImages.Where(i => i.ImageId == imageID).First();
                if (image.UserID != userId)
                {
                    throw (new UserCheckException());
                }
                item.Images.Add(image);
            }

            context.SaveChanges();
            var itemUpdated = GetItemDetailById(item.ItemID);
            return itemUpdated;

        }

        public bool DeleteImageById(int id, ApplicationUser user)
        {
            var userId = user.Id;
            var image = context.itemImages
                .Where(i => i.ImageId.Equals(id))
                .FirstOrDefault();
            if (image == null)
                return false;
            if (!image.UserID.Equals(userId))
                throw new UserCheckException();
            context.itemImages.Remove(image);
            context.SaveChanges();
            return true;
        }
    }
}
