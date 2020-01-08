using System;
using System.Collections.Generic;
using Rental.DTOs.ItemDTOs;

namespace MyRental.Services.ItemServices
{
    public interface IItemService
    {
        public IEnumerable<ItemListDTO> GetItemList();
        public ItemListDTO GetItemDetailById(int id);
        public void CreateItem(string value);
        public void DeleteItemById(int id);
    }
}
