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
    public class MenuCategoryEntityMapping : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder
              .HasKey(a => a.Id);

            builder
                  .Property(a => a.Id)
                  .HasColumnName("id");

            builder
                    .Property(a => a.Name)
                    .HasColumnName("name")
                    .IsRequired();
            builder
                   .Property(a => a.Status)
                   .HasColumnName("status")
                   .HasMaxLength(100)
                   .IsRequired();
            builder
                   .Property(a => a.Description)
                   .HasColumnName("description")
                   .HasMaxLength(1000);
            builder
                .HasOne(a => a.Company)
                .WithMany(a => a.MenuCategories)
                .HasForeignKey(a => a.CompanyId)
                .IsRequired();
            builder
                .HasMany(a => a.Images)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);
        }
    }
}
