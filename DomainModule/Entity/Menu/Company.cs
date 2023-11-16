using DomainModule.ServiceInterface.Menu;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class Company
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string? LandLineNumber { get; set; }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual string? Website { get; set; }
        public virtual string? Description { get; set; }
        public virtual int? LogoId { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual int CompanyTypeId { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
        
    }
}
