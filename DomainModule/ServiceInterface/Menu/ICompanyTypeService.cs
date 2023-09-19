using DomainModule.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.ServiceInterface.Menu
{
    public interface ICompanyTypeService
    {
        void Create(CompanyTypeCreateDto model);
        void Update(CompanyTypeDto model);
    }
}
