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
    public class CompanyTypeEntityMapping : IEntityTypeConfiguration<CompanyType>
    {
        public void Configure(EntityTypeBuilder<CompanyType> builder)
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
                   .Property(a => a.Code)
                   .HasColumnName("code")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
