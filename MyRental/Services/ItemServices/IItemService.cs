using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ResponseModels.ItemResponseModels;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<ItemListDTO> GetItemList();
        public IEnumerable<ItemListDTO> GetItemListByAmount(int start, int amount);
        public ItemDetailDTO GetItemDetailById(int id);
        public ItemAddUpdateResponseModel CreateItem(ItemCreateDTO newItem);
        public ItemAddUpdateResponseModel UpdateItem(int itemId, ItemCreateDTO newItem);
        public void DeleteItemById(int id);
        public ItemAddUpdateResponseModel ItemArchive(int id);
    }
}
