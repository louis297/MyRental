using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ItemModels;
using MyRental.Models.UserModel;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<Item> GetItemList(ApplicationUser user = null);
        public IEnumerable<Item> GetItemListByAmount(int start, int amount, ApplicationUser user = null, bool active = true);
        public Item GetItemDetailById(int id);
        public Item CreateItem(ItemCreateDTO newItem, ApplicationUser user);
        public int SaveFile(byte[] fileBytes, string contentType, ApplicationUser user);
        public Item UpdateItem(int itemId, ItemCreateDTO newItem, ApplicationUser user);
        public void DeleteItemById(int id, ApplicationUser user);
        public Item ItemToggleArchive(int id, ApplicationUser user);
        public bool DeleteImageById(int id, ApplicationUser user);

    }
}
