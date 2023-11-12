using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class MenuCategoryImages
    {
        public virtual int Id { get; set; }
        public virtual int CategoryId { get; set; }  
        public virtual MenuCategory Category { get; set; }  
        public virtual int AttachmentId { get; set; }
        public virtual Attachment Image { get; set; }
    }
}
