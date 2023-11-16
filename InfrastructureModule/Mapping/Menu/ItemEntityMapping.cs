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
    public class ItemEntityMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
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
                   .Property(a => a.Price)
                   .HasColumnName("price")
                   .IsRequired();
            builder
                   .Property(a => a.Description)
                   .HasColumnName("description")
                   .HasMaxLength(1000);
            builder
                .HasOne(a => a.Category)
                .WithMany(a=>a.Items)
                .HasForeignKey(a=>a.CategoryId);
            builder
                .HasOne(a => a.Company)
                .WithMany(a=>a.Items)
                .HasForeignKey(a=>a.CompanyId);
            builder
                .HasOne(a => a.Image)
                .WithMany()
                .HasForeignKey(a=>a.ImageId);
        }
    }
}
