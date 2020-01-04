using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myrental.Models;
using Rental.DTOs.ItemDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyRental.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        private MyRentalDbContext context;

        public ItemController()
        {
            context = new MyRentalDbContext();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ItemListDTO> Get()
        {
            var items = context.items.Select(item => new ItemListDTO()
            {
                Title = item.Title,
                Detail = item.Detail,
                Price = item.Price,
                PostTime = item.PostTime,
                ExpireTime = item.ExpireTime
                //TODO: add AuthorName field
                //AuthorName = item.Author.userName
            });
            return items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ItemListDTO Get(int id)
        {
            var items = context.items.Where(i => i.ItemID == id)
                .Select(item => new ItemListDTO()
                {
                    Title = item.Title,
                    Detail = item.Detail,
                    Price = item.Price,
                    PostTime = item.PostTime,
                    ExpireTime = item.ExpireTime
                    //TODO: add AuthorName field
                    //AuthorName = item.Author.userName
                });
            return items.Count() > 0 ? items.First() : null;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
