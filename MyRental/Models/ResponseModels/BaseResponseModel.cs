using System;
namespace MyRental.Models.ResponseModels
{
    public abstract class BaseResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}
