using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<ItemListDTO> GetItemList();
        public ItemDetailDTO GetItemDetailById(int id);
        public string CreateItem(ItemCreateDTO newItem);
        public void DeleteItemById(int id);
        public string ItemArchive(int id);
    }
}
