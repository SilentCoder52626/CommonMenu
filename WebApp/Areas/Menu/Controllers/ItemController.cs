using Castle.Components.DictionaryAdapter.Xml;
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
using WebApp.Extensions;
using WebApp.Helper;
using static Dapper.SqlMapper;

namespace WebApp.Areas.Menu.Controllers
{
    [Area("Menu")]
    public class ItemController : Controller
    {
        private readonly IItemRepository _ItemRepo;
        private readonly IMenuCategoryRepository _meunCategoryRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IToastNotification _notify;

        public ItemController(IItemRepository ItemRepo, IToastNotification notify, IMenuCategoryRepository MeunCategoryRepo, ICompanyRepository companyRepo)
        {
            _ItemRepo = ItemRepo;
            _notify = notify;
            _meunCategoryRepo = MeunCategoryRepo;
            _companyRepo = companyRepo;
        }

        public IActionResult Index(ItemFilterModel filter)
        {
            var userId = GetCurrentUserExtension.GetCurrentUserId(this);
            ViewBag.Companies = _companyRepo.GetCompanyDropDown(userId);
            ViewBag.MeunCategories = _meunCategoryRepo.GetMenuCategoryDropDown(userId);

            var model = _ItemRepo.GetAllFilteredItem(filter, userId);
            model.Filter = filter;
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
                    var entity = _ItemRepo.GetById(id.GetValueOrDefault()) ?? throw new CustomException("Item Not Found.");
                    var model = new ItemCreateDto()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        CompanyId = entity.CompanyId,
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
        public IActionResult ItemRow(int companyId, int categoryId, int? id)
        {
            var model = new ItemCreateDto();
            model.CompanyId = companyId;
            model.CategoryId = categoryId;
            if (id.GetValueOrDefault() > 0)
            {
                var entity = _ItemRepo.GetById(id.GetValueOrDefault()) ?? throw new CustomException("Item Not Found.");

                model.Id = entity.Id;
                model.Name = entity.Name;
                model.CompanyId = entity.CompanyId;
                model.Description = entity.Description;
                model.CategoryId = entity.CategoryId;
                model.Price = entity.Price;
                model.Image = entity.ImageId > 0 ? new AttachmentCreateDto()
                {
                    FileName = entity.Image.FileName,
                    Path = entity.Image.Path,
                    UploadedBy = entity.Image.UploadedBy,
                    UploadedDateTime = entity.Image.UploadedDateTime,
                } : null;

            }
            return PartialView("Item/Partial/ItemRow.cshtml", model);
        }
        public IActionResult BulkUpdate(int? companyId, int? categoryId)
        {
            try
            {
                var userId = GetCurrentUserExtension.GetCurrentUserId(this);

                ViewBag.Companies = _companyRepo.GetCompanyDropDown(userId);
                var model = new BulkItemModel();

                if (companyId.GetValueOrDefault() > 0 && categoryId.GetValueOrDefault() > 0)
                {
                    var Datas = _ItemRepo.GetQueryable().Where(a => a.CompanyId == companyId && a.CategoryId == categoryId).Select(a => new ItemCreateDto()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        CompanyId = a.CompanyId,
                        Description = a.Description,
                        CategoryId = a.CategoryId,
                        Price = a.Price,

                    }).ToList();
                    model.ItemCreateDtos = Datas;
                    model.CategoryId = categoryId.GetValueOrDefault();
                    model.CompanyId = companyId.GetValueOrDefault();

                }
                
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
