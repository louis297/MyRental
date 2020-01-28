using System;
using MyRental.DTOs.ItemDTOs;

namespace MyRental.Models.ResponseModels.ItemResponseModels
{
    public class ItemAddUpdateResponseModel: BaseResponseModel
    {
        public ItemDetailDTO Item { get; set; }
    }
}
