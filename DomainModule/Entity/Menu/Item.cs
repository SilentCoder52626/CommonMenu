using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class Item
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Price { get; set; }
        public virtual string Status { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual MenuCategory Category { get; set; }
        public virtual int ImageId { get; set; }
        public virtual Attachment Image { get; set; } 

    }
}
