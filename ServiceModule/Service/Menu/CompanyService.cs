using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface;
using DomainModule.ServiceInterface.Menu;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModule.Service.Menu
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepo;
        private readonly IAttachmentService _attachmentService;

        public CompanyService(IUnitOfWork unitOfWork, ICompanyRepository companyRepo, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _companyRepo = companyRepo;
            _attachmentService = attachmentService;
        }

        public int AddOrUpdate(CompanyCreateDto model)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = new Company();
                    if(model.Id > 0)
                    {
                        entity = _companyRepo.GetById(model.Id) ?? throw new CustomException("Company Not Found.");
                        ConfigueCompanyEntity(model, entity);
                        _companyRepo.Update(entity);

                    }
                    else
                    {
                        ConfigueCompanyEntity(model, entity);
                        _companyRepo.Insert(entity);

                    }
                    tx.Commit();
                    _unitOfWork.Complete();
                    return entity.Id;

                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void ConfigueCompanyEntity(CompanyCreateDto model, Company entity)
        {
            entity.Address = model.Address;
            entity.LandLineNumber = model.LandLineNumber;
            entity.MobileNumber = model.MobileNumber;
            entity.LogoId = _attachmentService.Create(model.LogoModel);
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Email = model.Email;
            entity.CompanyTypeId = model.CompanyTypeId;
            entity.Website = model.Website;
        }
    }
}
