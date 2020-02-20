using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRental.Models.ItemModels
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .HasOne(i => i.Author)
                .WithMany()
                .HasForeignKey(i => i.AuthorID);

            builder
                .HasMany(i => i.Images)
                .WithOne();

            builder
                .Property(i => i.PostTime)
                .HasDefaultValueSql("getdate()");

            builder
                .Property(i => i.Active)
                .HasDefaultValue(1);
        }
    }
}
