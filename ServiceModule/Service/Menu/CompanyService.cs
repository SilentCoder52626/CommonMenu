using DomainModule.Dto;
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

        public int AddOrUpdate(CompanyCreateDto model, AttachmentCreateDto? logoModel = null)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = new Company();
                    if (model.Id > 0)
                    {
                        entity = _companyRepo.GetById(model.Id) ?? throw new CustomException("Company Not Found.");
                        ConfigueCompanyEntity(model, entity);
                        ConfigureLogo(logoModel, entity);

                        _companyRepo.Update(entity);
                    }
                    else
                    {
                        ConfigueCompanyEntity(model, entity);
                        ConfigureLogo(logoModel, entity);
                        _companyRepo.Insert(entity);
                    }
                    _unitOfWork.Complete();
                    tx.Commit();
                    return entity.Id;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ConfigureLogo(AttachmentCreateDto? logoModel, Company entity)
        {
            if (logoModel != null)
            {
                entity.LogoId = _attachmentService.CreateWihoutTransaction(logoModel);

            }
        }

        private void ConfigueCompanyEntity(CompanyCreateDto model, Company entity)
        {
            entity.Address = model.Address;
            entity.LandLineNumber = model.LandLineNumber;
            entity.MobileNumber = model.MobileNumber;
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Email = model.Email;
            entity.CompanyTypeId = model.CompanyTypeId;
            entity.Website = model.Website;
        }
    }
}
