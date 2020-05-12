using MyRental.DTOs.ItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ResponseModels.ItemResponseModels
{
    public class ItemDetailResponseModel: BaseResponseModel
    {
        public ItemDetailDTO ItemDetail { get; set; }
    }
}
