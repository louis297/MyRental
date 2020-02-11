using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyRental.Models.ItemModels;
using MyRental.Models.UserModel;

namespace MyRental.Models
{
    public class MyRentalDbContext: ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Item> items { get; set; }
        public DbSet<ItemImage> itemImages { get; set; }


        public MyRentalDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql(connectionString: CustomSettings.ConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Author)
                .WithMany();

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Images)
                .WithOne();

            modelBuilder.Entity<ItemImage>()
                .HasOne(i => i.User)
                .WithMany();


            //modelBuilder.Entity<Item>().HasData(
            //    new Item
            //    {
            //        ItemID = 1,
            //        ItemName = "item1",
            //        Detail = "details1",
            //        ExpireTime = DateTime.Parse("2020-06-01 00:00:00"),
            //        Price = 200,
            //        Active = true,
            //        AuthorID = "4a056406-8a4b-47e9-85b7-13d311da1672",
            //        //Author = _userContext.
            //    },
            //    new Item
            //    {
            //        ItemID = 2,
            //        ItemName = "item2",
            //        Detail = "details2",
            //        ExpireTime = DateTime.Parse("2020-07-01 06:00:00"),
            //        Price = 100,
            //        Active = true,
            //        AuthorID = "4a056406-8a4b-47e9-85b7-13d311da1672"
            //    }
            //);
        }
    }
}
