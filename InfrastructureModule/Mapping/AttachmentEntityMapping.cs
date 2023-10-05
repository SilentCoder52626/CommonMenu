using DomainModule.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Mapping
{
    public class AttachmentEntityMapping : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder
              .HasKey(a => a.Id);

            builder
                  .Property(a => a.Id)
                  .HasColumnName("id");

            builder
                    .Property(a => a.FileName)
                    .HasColumnName("file_name")
                    .IsRequired();
            builder
                    .Property(a => a.Path)
                    .HasColumnName("path")
                    .IsRequired();
            builder
                    .Property(a => a.UploadedDateTime)
                    .HasColumnName("uploaded_dateTime")
                    .IsRequired();
            builder
                .Property(a => a.UploadedBy)
                .HasColumnName("uploaded_by")
                .IsRequired();
            
        }
    }
}
