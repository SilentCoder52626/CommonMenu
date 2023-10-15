using DomainModule.Entity.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Mapping.Menu
{
    public class MenuCategoryImagesEntityMapping : IEntityTypeConfiguration<MenuCategoryImages>
    {
        public void Configure(EntityTypeBuilder<MenuCategoryImages> builder)
        {
            builder
              .HasKey(a => a.Id);
            builder
                .HasOne(a => a.Category)
                .WithMany(a => a.Images)
                .HasForeignKey(a => a.CategoryId);

           builder
                .HasOne(a => a.Image)
                .WithMany()
                .HasForeignKey(a => a.AttachmentId);
        }
    }
}
