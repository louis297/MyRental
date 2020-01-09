using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<ItemListDTO> GetItemList();
        public ItemListDTO GetItemDetailById(int id);
        public string CreateItem(ItemCreateDTO newItem);
        public void DeleteItemById(int id);
    }
}
