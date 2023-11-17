using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class MenuCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Status { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual Company Company { get; set; }    
        public virtual ICollection<MenuCategoryImages> Images { get; set; } = new List<MenuCategoryImages>();
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
