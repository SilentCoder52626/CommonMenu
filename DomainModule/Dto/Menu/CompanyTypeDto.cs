using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class CompanyTypeDto : CompanyTypeCreateDto
    {
        public int? Id { get; set; }
        
    }
    public class CompanyTypeModel
    {
        public List<CompanyTypeDto> CompanyTypes { get; set; } = new List<CompanyTypeDto>();
    }
    public class CompanyTypeCreateDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
    public class CompanyTypeDropDownModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
