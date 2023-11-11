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
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string? LandLineNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
        public int? LogoId { get; set; }
        public Attachment Attachment { get; set; }
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
    }
}
