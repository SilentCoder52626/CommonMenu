using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModule.Service.Menu
{
    public class CompanyTypeService : ICompanyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyTypeRepository _typeRepo;

        public CompanyTypeService(IUnitOfWork unitOfWork, ICompanyTypeRepository typeRepo)
        {
            _unitOfWork = unitOfWork;
            _typeRepo = typeRepo;
        }

        public void Create(CompanyTypeCreateDto model)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    ValidateCompanyCode(model.Code);

                    var Type = new CompanyType()
                    {
                        Name = model.Name,
                        Code = model.Code,
                    };
                    _typeRepo.Insert(Type);
                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Update(CompanyTypeDto model)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var Type = _typeRepo.GetById(model.Id) ?? throw new CustomException("Company Type Not Found.");

                    ValidateCompanyCode(model.Code, model.Id);
                   
                    Type.Name = model.Name;
                    Type.Code = model.Code;

                    _typeRepo.Update(Type);
                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void ValidateCompanyCode(string? code, int? id = null)
        {
            var CompanyTypeByCode = _typeRepo.GetQueryable().FirstOrDefault(a => a.Code == code);
            if(CompanyTypeByCode != null && CompanyTypeByCode.Id != id)
            {
                throw new CustomException($"Company Type with code {code} already exists.");
            }
            return;
        }
    }
}
