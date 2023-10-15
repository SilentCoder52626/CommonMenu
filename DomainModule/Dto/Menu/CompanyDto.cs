using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class CompanyDto : CompanyCreateDto
    {
        
        public string CompanyType{ get; set; }
    }
    public class CompanyCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string LandLineNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string LogoPath { get; set; }
        public int CompanyTypeId { get; set; }
    }
}
