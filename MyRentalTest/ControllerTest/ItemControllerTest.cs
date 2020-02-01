using System;
using System.Linq;
using System.Text.Json;
using MyRental.DTOs.ItemDTOs;
using MyRental.Services.ItemServices;
using MyRental.Tests.ControllerTest;
using Xunit;

namespace MyRentalTest.ControllerTest
{
    public class ItemControllerTest
    {
        private IItemService service;


        public ItemControllerTest()
        {
            service = new ItemServiceMock();
            Console.WriteLine("aasfawefawefawefwfawef");
        }
        [Fact]
        public void GetListTest()
        {
            var r = service.GetItemList();
            var i = r.GetEnumerator();

            Assert.Equal(@"[{""ItemName"":""item1"",""Detail"":""details1"",""PostTime"":""0001-01-01T00:00:00"",""ExpireTime"":""2020-06-01T00:00:00"",""Price"":200},{""ItemName"":""item2"",""Detail"":""details2"",""PostTime"":""0001-01-01T00:00:00"",""ExpireTime"":""2020-07-01T06:00:00"",""Price"":100}]", JsonSerializer.Serialize(r));

        }

        [Fact]
        public void GetDetailTest()
        {
            var r = service.GetItemDetailById(1);
            Assert.Equal(@"{""ItemName"":""item1"",""Detail"":""details1"",""PostTime"":""0001-01-01T00:00:00"",""ExpireTime"":""2020-06-01T00:00:00"",""Price"":200}", JsonSerializer.Serialize(r));
        }

        [Fact]
        public void CreateItemTest()
        {
            var m = new ItemCreateDTO
            {
                ItemName = "aa",
                Detail = "bb",
                Price = 30,
                ExpireTime = DateTime.Parse("2030-06-01T00:00:00")
            };
            var r = service.CreateItem(m);
            Assert.Equal("{'result': 'success'}", r);

            m.ItemName = string.Concat(Enumerable.Repeat("abc", 100)); ;
            r = service.CreateItem(m);
            Assert.Equal("{'result': 'failed', 'reason': 'Item name is too long'}", r);

        }
    }
}
