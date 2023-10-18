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
    public class CompanyEntityMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
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
                   .Property(a => a.MobileNumber)
                   .HasColumnName("mobile_number")
                   .HasMaxLength(100)
                   .IsRequired();
            builder
                   .Property(a => a.LandLineNumber)
                   .HasColumnName("land_line_number")
                   .HasMaxLength(100);
            builder
                   .Property(a => a.Address)
                   .HasColumnName("address")
                   .HasMaxLength(500)
                   .IsRequired();
            builder
                   .Property(a => a.Email)
                   .HasColumnName("email")
                   .HasMaxLength(500);
            builder
                    .Property(a => a.Website)
                    .HasColumnName("website")
                    .HasMaxLength(500);
            builder
                   .Property(a => a.Description)
                   .HasColumnName("desciption")
                   .HasMaxLength(1000)
                   .IsRequired();
            builder
                   .Property(a => a.LogoId)
                   .HasColumnName("logo")
                   .IsRequired();
            builder
                .HasOne(a => a.Attachment)
                .WithMany()
                .HasForeignKey(a => a.LogoId);
            builder
                   .Property(a => a.CompanyTypeId)
                   .HasColumnName("company_type_id")
                   .IsRequired();
            builder
                .HasOne(a => a.CompanyType)
                .WithMany(a => a.Companies)
                .HasForeignKey(a => a.CompanyTypeId);
                
        }
    }
}
