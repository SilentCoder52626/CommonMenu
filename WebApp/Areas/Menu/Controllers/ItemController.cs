using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using InfrastructureModule.Repository.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NToastNotify;
using System.Runtime.CompilerServices;
using WebApp.Helper;

namespace WebApp.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class ItemController : Controller
    {
        private readonly IItemRepository _ItemRepo;
        private readonly IMenuCategoryRepository _meunCategoryRepo;
        private readonly IToastNotification _notify;

        public ItemController(IItemRepository ItemRepo, IToastNotification notify, IMenuCategoryRepository MeunCategoryRepo)
        {
            _ItemRepo = ItemRepo;
            _notify = notify;
            _meunCategoryRepo = MeunCategoryRepo;
        }

        public IActionResult Index()
        {
            var model = _ItemRepo.GetAllItem();
            return View(model);
        }

        public IActionResult AddOrUpdate(int? id)
        {
            try
            {
                ViewBag.MeunCategories = _meunCategoryRepo.GetMenuCategoryDropDown();
                if (id.GetValueOrDefault() > 0)
                {
                    var entity = _ItemRepo.GetById(id.GetValueOrDefault()) ?? throw new CustomException("Item Not Found.");
                    var model = new ItemCreateDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                       Description = entity.Description,
                       CategoryId = entity.CategoryId,
                       Price = entity.Price,
                       Image = entity.ImageId > 0 ? new AttachmentCreateDto()
                       {
                           FileName = entity.Image.FileName,
                           Path = entity.Image.Path,
                           UploadedBy = entity.Image.UploadedBy,
                           UploadedDateTime = entity.Image.UploadedDateTime,
                       } : null,
                    };
                    return View(model);
                }
                else
                {
                    var model = new ItemCreateDto();
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
