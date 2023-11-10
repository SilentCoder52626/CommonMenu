using DomainModule.Dto;
using DomainModule.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.ServiceInterface.Menu
{
    public interface ICompanyService
    {
        int AddOrUpdate(CompanyCreateDto model, AttachmentCreateDto? logoModel = null);
    }
}
