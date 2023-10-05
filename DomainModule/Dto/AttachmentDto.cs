using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto
{
    public class AttachmentDto : AttachmentUpdateDto
    {
        public string UploadByName { get; set; }
    }
    public class AttachmentCreateDto
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public string UploadedBy { get; set; }
    }
    public class AttachmentUpdateDto : AttachmentCreateDto
    {
        public int Id { get; set; }

    }
}
