using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity
{
    public class Attachment
    {
        public virtual int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Path { get; set; }
        public virtual DateTime UploadedDateTime { get; set; }
        public virtual string UploadedBy { get; set; }
        
    }
}
