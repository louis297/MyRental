using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ItemModels
{
    public class ItemLikeConfiguration : IEntityTypeConfiguration<ItemLike>
    {
        public void Configure(EntityTypeBuilder<ItemLike> builder)
        {
            builder
                .HasOne(l => l.ItemLiked)
                .WithMany()
                .HasForeignKey(l => l.ItemLikedID)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserID)
                // use Restrict because when user is deleted, item will delete cascadely, then itemlike
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasKey(l => new { l.UserID, l.ItemLikedID });
        }
    }
}
