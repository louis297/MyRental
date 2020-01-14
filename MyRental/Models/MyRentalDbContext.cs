using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyRental.Models.ItemModels;
using MyRental.Models.UserModel;

namespace MyRental.Models
{
    public class MyRentalDbContext: DbContext
    {
        public DbSet<Item> items { get; set; }
        public DbSet<ItemImage> itemImages { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //TODO: move connect string to config file
            optionsBuilder.UseMySql("Server=localhost;Database=Rental;user=root;password=sqL3345!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
            .HasMany(i => i.itemImages)
            .WithOne();

            modelBuilder.Entity<ItemImage>()
                .HasKey(i => new { i.ItemId, i.ImagePath });


            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    ItemID = 1,
                    ItemName = "item1",
                    Detail = "details1",
                    ExpireTime = DateTime.Parse("2020-06-01 00:00:00"),
                    Price = 200,
                    Active = true
                },
                new Item
                {
                    ItemID = 2,
                    ItemName = "item2",
                    Detail = "details2",
                    ExpireTime = DateTime.Parse("2020-07-01 06:00:00"),
                    Price = 100,
                    Active = true
                }
            );
        }
    }
}
