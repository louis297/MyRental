using System;
using MyRental.Models.ItemModels;

namespace MyRental.Models.ResponseModels.ItemResponseModels
{
    public class ItemImageResponseModel
    {
        public byte[] Image { get; set; }
        public string ImageType { get; set; }

        public ItemImageResponseModel(ItemImage itemImage)
        {
            Image = itemImage.ImageContent;
            ImageType = itemImage.ImageType;
        }

        public ItemImageResponseModel()
        {
        }
    }
}
