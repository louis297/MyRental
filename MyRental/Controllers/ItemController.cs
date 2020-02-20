using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyRental.DTOs.ItemDTOs;
using MyRental.Models.ResponseModels.ItemResponseModels;
using MyRental.Models.UserModel;
using MyRental.MyRentalExceptions;
using MyRental.Services.ItemServices;
using MyRental.Utils;

namespace MyRental.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        private IItemService _service;
        private UserManager<ApplicationUser> _userManager;

        public ItemController(IItemService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<ItemListDTO> Get()
        {
            var items = _service.GetItemList();
            var DTO = items
                .Select(item => new ItemListDTO(item));
            return DTO;
        }

        [HttpGet("mylist")]
        public async Task<IEnumerable<ItemListDTO>> GetMyListAsync([FromQuery]int start = 1, [FromQuery]int amount = 10)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var items = _service.GetItemListByAmount(start, amount, user);
            if (items.Count() != 0)
            {
                var DTO = items.Select(item => new ItemListDTO(item));
                return DTO;
            }
            else
            {
                return new List<ItemListDTO>();
            }
        }

        [HttpGet("myarchivedlist")]
        public async Task<IEnumerable<ItemListDTO>> GetMyArchivedListAsync([FromQuery]int start = 1, [FromQuery]int amount = 10)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var items = _service.GetItemListByAmount(start, amount, user, false);
            if (items.Count() != 0)
            {
                var DTO = items.Select(item => new ItemListDTO(item));
                return DTO;
            }
            else
            {
                return new List<ItemListDTO>();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ItemDetailDTO Get(int id)
        {
            var item = _service.GetItemDetailById(id);
            var DTO = new ItemDetailDTO(item);
            foreach(var image in item.Images)
            {
                DTO.Images.Add(new ItemImageResponseModel(image));
            }
            return DTO;
        }

        // Toggle Item.Active
        [HttpGet("archive/{id}")]
        public async Task<ItemAddUpdateResponseModel> ArchiveAsync([FromRoute]int id)
        {
            
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var item = _service.ItemToggleArchive(id, user);
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
            catch (UserCheckException)
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "No permission",
                    Item = null
                };
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

        //public class TestModel
        //{
        //    public string ItemName { get; set; }
        //    public int Price { get; set; }
        //    public DateTime ExpireTime { get; set; }
        //    public List<int> Images { get; set; }
        //}
        //[HttpPost("testupload")]
        //public async Task<string> TestPostAsync([FromBody]TestModel model)
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    Console.WriteLine(model);
        //    return "success";
        //}

        // POST api/values
        [HttpPost("upload")]
        public async Task<ItemAddUpdateResponseModel> PostAsync([FromBody]ItemCreateDTO model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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

                var item = _service.CreateItem(model, user);
                
                var DTO = new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = new ItemDetailDTO(item)
                };
                return DTO;
            }
            catch (UserCheckException)
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "No permission",
                    Item = null
                };
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

        [HttpPost("uploadimage")]
        // The argument variable name 'body' should be same from the frontend
        // Frontend should use 'FormData' to append ('body', '<content>')
        public async Task<UploadImageResponseModel> UploadFileAsync([FromForm(Name ="body")]IFormFile body)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await body.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
                var contentType = Util.GetImageFormat(fileBytes);
                if(contentType.Equals(Util.ImageFormat.unknown))
                {
                    return new UploadImageResponseModel
                    {
                        isSuccess = false,
                        Message = "File type error"
                    };
                }
                var imageId = _service.SaveFile(fileBytes, contentType.ToString(), user);
                return new UploadImageResponseModel {
                    isSuccess = true,
                    Message = imageId.ToString()
                };
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new UploadImageResponseModel
                {
                    isSuccess = false,
                    Message = "Write file failed"
                };
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ItemAddUpdateResponseModel> PutAsync([FromRoute]int id, [FromBody]ItemCreateDTO model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
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

                var item = _service.UpdateItem(id, model, user);

                var DTO = new ItemAddUpdateResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    Item = new ItemDetailDTO(item)
                };
                return DTO;
            }
            catch(UserCheckException)
            {
                return new ItemAddUpdateResponseModel
                {
                    isSuccess = false,
                    Message = "No permission",
                    Item = null
                };
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

        // Delete image
        [HttpDelete("deleteimage/{id}")]
        public async Task<UploadImageResponseModel> DeleteImageAsync([FromRoute]int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var result = _service.DeleteImageById(id, user);
                if (result)
                {
                    return new UploadImageResponseModel { isSuccess = true, Message = "" };
                } else
                {
                    return new UploadImageResponseModel { isSuccess = false, Message = "Image not found" };
                }
            } catch (UserCheckException)
            {
                return new UploadImageResponseModel { isSuccess = false, Message = "No permission" };
            } catch
            {
                return new UploadImageResponseModel { isSuccess = false, Message = "Unknown server error" };
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
