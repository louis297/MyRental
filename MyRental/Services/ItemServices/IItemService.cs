using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ItemModels;
using MyRental.Models.ResponseModels.ItemResponseModels;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<Item> GetItemList();
        public IEnumerable<Item> GetItemListByAmount(int start, int amount);
        public Item GetItemDetailById(int id);
        public Item CreateItem(ItemCreateDTO newItem);
        public Item UpdateItem(int itemId, ItemCreateDTO newItem);
        public void DeleteItemById(int id);
        public Item ItemArchive(int id);

    }
}
