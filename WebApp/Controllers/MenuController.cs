using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Enums;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WebApp.Extensions;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IMenuCategoryRepository _menuCategoryRepo;
        private readonly IToastNotification _notify;

        public MenuController(ICompanyRepository companyRepo, IMenuCategoryRepository menuCategoryRepo, IToastNotification notify)
        {
            _companyRepo = companyRepo;
            _menuCategoryRepo = menuCategoryRepo;
            _notify = notify;
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserExtension.GetCurrentUserId(this);
            ViewBag.Companies = _companyRepo.GetCompanyDropDown(userId);

            return View();
        }
        public IActionResult MenuDetails(int? companyId, string? type)
        {
            try
            {
                if (companyId.GetValueOrDefault() > 0)
                {
                    if (!String.IsNullOrEmpty(type))
                    {
                        if (type == MenuTypeEnum.Default.ToString())
                        {
                            return RedirectToAction(nameof(DefaultMenuDetails), new { companyId });
                        }
                    }
                    else
                    {
                        throw new CustomException("Please select a style type to generate menu.");
                    }
                }
                throw new CustomException("Please select a company to genarate menu.");
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                _notify.AddErrorToastMessage(ex.Message);
                return BadRequest(ex);
            }
        }
        public IActionResult DefaultMenuDetails(int companyId)
        {
            try
            {
                if (companyId == 0)
                    throw new CustomException("Please select company to generate menu.");
                var model = new MenuDetailsViewModel();
                var Company = _companyRepo.GetById(companyId) ?? throw new CustomException("Company not found.");
                var CompanyModel = new CompanyDto()
                {

                    Name = Company.Name,
                    Address = Company.Address,
                    LandLineNumber = Company.LandLineNumber,
                    MobileNumber = Company.MobileNumber,
                    CompanyTypeId = Company.CompanyTypeId,
                    CompanyType = Company.CompanyType.Name,
                    Description = Company.Description,
                    Email = Company.Email,
                    Website = Company.Website,
                    LogoModel = new AttachmentCreateDto()
                    {
                        FileName = Company.Attachment.FileName,
                        Path = Company.Attachment.Path,
                        UploadedBy = Company.Attachment.UploadedBy,
                        UploadedDateTime = Company.Attachment.UploadedDateTime
                    }
                };
                model.Company = CompanyModel;
                return PartialView(model);
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }
    }
}
