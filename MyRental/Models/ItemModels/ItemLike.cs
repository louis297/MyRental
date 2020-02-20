using MyRental.Models.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ItemModels
{
    public class ItemLike
    {
        [Key]
        [Column(Order = 1)]
        public int ItemLikedID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserID { get; set; }
        public virtual Item ItemLiked  { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
