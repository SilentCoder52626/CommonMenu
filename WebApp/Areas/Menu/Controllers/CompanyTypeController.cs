using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Runtime.CompilerServices;
using WebApp.Helper;

namespace WebApp.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class CompanyTypeController : Controller
    {
        private readonly ICompanyTypeRepository _companyTypeRepo;
        private readonly IToastNotification _notify;

        public CompanyTypeController(ICompanyTypeRepository companyTypeRepo, IToastNotification notify)
        {
            _companyTypeRepo = companyTypeRepo;
            _notify = notify;
        }

        public IActionResult Index()
        {
            var model = _companyTypeRepo.GetCompanyTypeModel();
            return View(model);
        }
        public IActionResult Create()
        {
            try
            {
                var model = new CompanyTypeCreateDto();

                return View(model);
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                _notify.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult Update(int id)
        {
            try
            {
                var entity = _companyTypeRepo.GetById(id) ?? throw new CustomException("Company Type Not Found.");
                var model = new CompanyTypeDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code
                };
                return View(model);
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
