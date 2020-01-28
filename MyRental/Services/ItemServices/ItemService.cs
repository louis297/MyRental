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

        public IEnumerable<ItemListDTO> GetItemList()
        {
            var items = context.items
                .Where(item => item.Active)
                .Select(item => new ItemListDTO()
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    Detail = item.Detail,
                    Price = item.Price,
                    //TODO: deal with date time format
                    PostTime = item.PostTime.ToString("dd/MM/yyyy HH:mm"),
                    ExpireTime = item.ExpireTime.ToString("dd/MM/yyyy HH:mm")
                    //TODO: add AuthorName field
                    //AuthorName = item.Author.userName
                });

            return items;
        }

        public IEnumerable<ItemListDTO> GetItemListByAmount(int start, int amount)
        {
            var items = context.items
                .Where(item => item.Active)
                .Where(i => i.ItemID >= start)
                .OrderBy(i => i.ItemID)
                .Take(amount)
                .Select(item => new ItemListDTO()
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    Detail = item.Detail,
                    Price = item.Price,
                    //TODO: deal with date time format
                    PostTime = item.PostTime.ToString("dd/MM/yyyy HH:mm"),
                    ExpireTime = item.ExpireTime.ToString("dd/MM/yyyy HH:mm")
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

        public ItemAddUpdateResponseModel ItemArchive(int id)
        {
            try
            {
                var item = context.items
                    .Where(i => i.ItemID == id)
                    .FirstOrDefault();
                if (item == null)
                {
                    return new ItemAddUpdateResponseModel {
                    isSuccess = false,
                    Message = "Item not found",
                    Item = null};
                }
                else
                {
                    item.Active = false;
                    context.SaveChanges();
                    return new ItemAddUpdateResponseModel
                    {
                        isSuccess = true,
                        Message = "",
                        Item = null
                    };
                }
            } catch
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "DB error",
                    Item = null
                };
            }
        }

        public ItemAddUpdateResponseModel CreateItem(ItemCreateDTO newItem)
        {
            try
            {
                var v = ItemCreateDTOValidation(newItem);
                if (!v.Equals("success"))
                {
                    return new ItemAddUpdateResponseModel
                    {
                        isSuccess = false,
                        Message = v,
                        Item = null
                    };
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

                var itemAdded = GetItemDetailById(item.ItemID);
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = itemAdded
                };
            }
            catch
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "DB error",
                    Item = null
                };
            }
            
        }

        public ItemAddUpdateResponseModel UpdateItem(int itemId, ItemCreateDTO newItem)
        {
            var v = ItemCreateDTOValidation(newItem);
            if (!v.Equals("success"))
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = v,
                    Item = null
                };
            }

            var item = context.items.Where(i => i.ItemID == itemId)
                .FirstOrDefault();
            if(item == null)
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "Item not found",
                    Item = null
                };
            }
            try
            {
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
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = itemUpdated
                };
            } catch
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "DB error",
                    Item = null
                };
            }
        }

        private string ItemCreateDTOValidation(ItemCreateDTO item)
        {
            if (item.ItemName.Length > 250)
            {
                return "Item name is too long";
            }
            if (item.Detail.Length > 999)
            {
                return "Item detail is too long";
            }
            if (item.Price < 0)
            {
                return "Price should not be negative";
            }
            if (item.ExpireTime < DateTime.Now)
            {
                return "Expire time should not be in the past";
            }

            return "success";
        }
    }
}
