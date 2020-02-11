using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyRental.Models.ItemModels;

namespace MyRental.Models.UserModel
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Item> Student { get; set; }
    }
}
