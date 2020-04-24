using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyRental.DTOs.MessageDTOs;
using MyRental.Models.ResponseModels.MessageResponseModels;
using MyRental.Models.UserModel;
using MyRental.MyRentalExceptions;
using MyRental.Services.MessageServices;

namespace MyRental.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    //[ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _service;
        private UserManager<ApplicationUser> _userManager;

        public MessageController(IMessageService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public MessageListResponseModel Get([FromQuery]int ItemID)
        {
            try
            {
                var messages = _service.GetMessageList(ItemID)
                    .Select(m => new MessageDTO(m))
                    .ToList();
                var responseModel = new MessageListResponseModel
                {
                    isSuccess = true,
                    Message = "",
                    MessageList = messages
                };
                return responseModel;
            }
            catch
            {
                return new MessageListResponseModel
                {
                    isSuccess = false,
                    Message = "Cannot get message list",
                    MessageList = null
                };
            }
        }

        [HttpPost("send")]
        public async Task<MessageSendResponseModel> PostAsync([FromBody]MessageSendDTO message)
        {
            Console.WriteLine(message);
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var r = _service.SendMessage(message, user);
                return new MessageSendResponseModel
                { 
                    isSuccess=true,
                    Message="",
                    MessageContent = new MessageDTO(r)
                };
            }
            catch (UserCheckException)
            {
                return new MessageSendResponseModel
                {
                    isSuccess = false,
                    Message = "Bad receiver ID",
                    MessageContent = null
                };
            }
            catch
            {
                return new MessageSendResponseModel
                {
                    isSuccess = false,
                    Message = "Message sent failed",
                    MessageContent = null
                };
            }
        }
    }
}