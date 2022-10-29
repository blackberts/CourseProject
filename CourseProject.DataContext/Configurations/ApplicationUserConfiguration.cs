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
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser));
            builder.HasKey(user => user.Id);
            builder.HasMany(user => user.Collections)
                .WithMany(collections => collections.Users);
            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.User);
        }
    }
}
