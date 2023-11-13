using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NToastNotify;
using System.Runtime.CompilerServices;
using WebApp.Helper;

namespace WebApp.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly ICompanyTypeRepository _companyTypeRepo;
        private readonly IToastNotification _notify;

        public CompanyController(ICompanyRepository CompanyRepo, IToastNotification notify, ICompanyTypeRepository companyTypeRepo)
        {
            _companyRepo = CompanyRepo;
            _notify = notify;
            _companyTypeRepo = companyTypeRepo;
        }

        public IActionResult Index()
        {
            var model = _companyRepo.GetAllCompany();
            return View(model);
        }

        public IActionResult AddOrUpdate(int? id)
        {
            try
            {
                ViewBag.CompanyTypes = _companyTypeRepo.GetCompanyTypeDropDown();
                if (id.GetValueOrDefault() > 0)
                {
                    var entity = _companyRepo.GetById(id.GetValueOrDefault()) ?? throw new CustomException("Company Not Found.");
                    var model = new CompanyCreateDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Address = entity.Address,
                        CompanyTypeId = entity.CompanyTypeId,
                        Description =entity.Description,
                        Email = entity.Email,
                        LandLineNumber = entity.LandLineNumber,
                        MobileNumber = entity.MobileNumber,
                        Website = entity.Website
                    };
                    return View(model);
                }
                else
                {
                    var model = new CompanyCreateDto();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                _notify.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
