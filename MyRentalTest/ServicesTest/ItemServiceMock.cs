using System;
using System.Collections.Generic;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ItemModels;
using MyRental.Services.ItemServices;

namespace MyRental.Tests.ControllerTest
{
    public class ItemServiceMock: IItemService
    {
        public ItemServiceMock()
        {
        }

        public string CreateItem(ItemCreateDTO newItem)
        {
            try
            {
                if (newItem.ItemName.Length > 250)
                {
                    return "{'result': 'failed', 'reason': 'Item name is too long'}";
                }
                if (newItem.Detail.Length > 999)
                {
                    return "{'result': 'failed', 'reason': 'Item detail is too long'}";
                }
                if (newItem.Price < 0)
                {
                    return "{'result': 'failed', 'reason': 'Price should not be negative'}";
                }
                if (newItem.ExpireTime < DateTime.Now)
                {
                    return "{'result': 'failed', 'reason': 'Expire time should not be in the past'}";
                }

                return "{'result': 'success'}";
            } catch
            {
                return "{'result': 'failed'}";
            }
            throw new NotImplementedException();
        }

        public void DeleteItemById(int id)
        {
            throw new NotImplementedException();
        }

        public ItemListDTO GetItemDetailById(int id)
        {
            if (id == 1)
            {
                var item = new ItemListDTO
                {
                    ItemName = "item1",
                    Detail = "details1",
                    ExpireTime = DateTime.Parse("2020-06-01 00:00:00"),
                    Price = 200
                };
                return item;
            }
            else
            {
                return null;
            }
            
        }

        public IEnumerable<ItemListDTO> GetItemList()
        {
            var items = new List<ItemListDTO>();
            items.Add(new ItemListDTO
            {
                ItemName = "item1",
                Detail = "details1",
                ExpireTime = DateTime.Parse("2020-06-01 00:00:00"),
                Price = 200
            });
            items.Add(new ItemListDTO
            {
                ItemName = "item2",
                Detail = "details2",
                ExpireTime = DateTime.Parse("2020-07-01 06:00:00"),
                Price = 100
            });
            return items;
        }
    }
}
