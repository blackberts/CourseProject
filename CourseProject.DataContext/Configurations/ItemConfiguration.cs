using CourseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable(nameof(Item));
            builder.HasKey(item => item.ItemId);
            builder.HasMany(item => item.Tags)
                .WithMany(tag => tag.Items);
            builder.HasMany(item => item.Collections)
                .WithMany(collections => collections.Items);
            builder.HasMany(item => item.Comments)
                .WithOne(comment => comment.Item);
        }
    }
}
