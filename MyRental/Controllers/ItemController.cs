using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyRental.DTOs.ItemDTOs;
using MyRental.Services.ItemServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ItemListDTO Get(int id)
        {
            var item = _service.GetItemDetailById(id);
            return item;
        }

        // POST api/values
        [HttpPost]
        public string Post(ItemCreateDTO model)
        {
            var r = _service.CreateItem(model);
            return r;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
