using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ResponseModels.ItemResponseModels;
using MyRental.Services.ItemServices;

namespace MyRental.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        private IItemService _service;

        public ItemController(IItemService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ItemListDTO> Get()
        {
            var items = _service.GetItemList();
            var DTO = items.Select(item => new ItemListDTO(item));
            return DTO;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ItemDetailDTO Get(int id)
        {
            var item = _service.GetItemDetailById(id);
            var DTO = new ItemDetailDTO(item);
            return DTO;
        }

        [HttpGet("archive/{id}")]
        public ItemAddUpdateResponseModel Archive(int id)
        {
            
            try
            {
                var item = _service.ItemArchive(id);
                if (item == null)
                {
                    return new ItemAddUpdateResponseModel
                    {
                        isSuccess = false,
                        Message = "Item not found",
                        Item = null
                    };
                }
                else
                {
                    var DTO = new ItemAddUpdateResponseModel
                    {
                        isSuccess = true,
                        Message = "",
                        Item = new ItemDetailDTO(item)
                    };
                    return DTO;
                }
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

        // POST api/values
        [HttpPost]
        public ItemAddUpdateResponseModel Post(ItemCreateDTO model)
        {
            try
            {
                var v = ItemCreateDTOValidation(model);
                if (!v.Equals("success"))
                {
                    return new ItemAddUpdateResponseModel
                    {
                        isSuccess = false,
                        Message = v,
                        Item = null
                    };
                }

                var item = _service.CreateItem(model);
                
                var DTO = new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = new ItemDetailDTO(item)
                };
                return DTO;
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public ItemAddUpdateResponseModel Put(int id, ItemCreateDTO model)
        {
            try
            {
                var v = ItemCreateDTOValidation(model);
                if (!v.Equals("success"))
                {
                    return new ItemAddUpdateResponseModel
                    {
                        isSuccess = false,
                        Message = v,
                        Item = null
                    };
                }

                var item = _service.UpdateItem(id, model);

                var DTO = new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = new ItemDetailDTO(item)
                };
                return DTO;
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
