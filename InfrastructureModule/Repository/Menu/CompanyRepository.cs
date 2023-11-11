using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.RepositoryInterface.Menu;
using InfrastructureModule.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Repository.Menu
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public CompanyModel GetAllCompany()
        {
            var CompanyList =  GetQueryable().Select(a => new CompanyDto()
            {
                Address = a.Address,
                CompanyType = a.CompanyType.Code,
                CompanyTypeId = a.CompanyTypeId,
                Description = a.Description,
                Email = a.Email,
                Id = a.Id,
                LandLineNumber = a.LandLineNumber,
                MobileNumber = a.MobileNumber,
                Name = a.Name,
                Website = a.Website,
                LogoModel = new AttachmentCreateDto()
                {
                    FileName = a.Attachment.FileName,
                    Path = a.Attachment.Path,
                    UploadedBy = a.Attachment.UploadedBy,
                    UploadedDateTime = a.Attachment.UploadedDateTime,
                }
            }).ToList();

            return new CompanyModel()
            {
                CompanyList = CompanyList
            };

        }
    }
}
