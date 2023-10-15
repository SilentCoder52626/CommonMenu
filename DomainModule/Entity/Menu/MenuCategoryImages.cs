using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class MenuCategoryImages
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }  
        public MenuCategory Category { get; set; }  
        public int AttachmentId { get; set; }
        public Attachment Image { get; set; }
    }
}
