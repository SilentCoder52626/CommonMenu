using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NToastNotify;
using System.Runtime.CompilerServices;
using WebApp.Extensions;
using WebApp.Helper;

namespace WebApp.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class MenuCategoryController : Controller
    {
        private readonly IMenuCategoryRepository _menuCategoryRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IToastNotification _notify;

        public MenuCategoryController(IMenuCategoryRepository MenuCategoryRepo, IToastNotification notify, ICompanyRepository companyRepo)
        {
            _menuCategoryRepo = MenuCategoryRepo;
            _notify = notify;
            _companyRepo = companyRepo;
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserExtension.GetCurrentUserId(this);

            var model = _menuCategoryRepo.GetAllMenuCategory(userId);
            return View(model);
        }

        public IActionResult AddOrUpdate(int? id)
        {
            try
            {
                var userId = GetCurrentUserExtension.GetCurrentUserId(this);
                ViewBag.Companies = _companyRepo.GetCompanyDropDown(userId);

                if (id.GetValueOrDefault() > 0)
                {
                    var entity = _menuCategoryRepo.GetById(id.GetValueOrDefault()) ?? throw new CustomException("MenuCategory Not Found.");
                    var model = new MenuCategoryCreateDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        CompanyId = entity.CompanyId,
                    };
                    if(entity.Images!= null)
                    {
                        model.Images = entity.Images.Select(a => new AttachmentDto()
                        {
                            FileName = a.Image.FileName,
                            Path = a.Image.Path,
                            UploadedBy = a.Image.UploadedBy,
                            UploadedDateTime = a.Image.UploadedDateTime,
                            Id = a.AttachmentId,
                        }).ToList();
                    }
                    return View(model);
                }
                else
                {
                    var model = new MenuCategoryCreateDto();
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
