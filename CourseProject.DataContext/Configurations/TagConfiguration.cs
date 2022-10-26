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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag));
            builder.HasKey(tag => tag.TagId);
            builder.HasMany(tags => tags.Items)
                .WithMany(items => items.Tags);
            builder.HasMany(tags => tags.Collections)
                .WithMany(collections => collections.Tags);
        }
    }
}
