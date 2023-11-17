using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Entity;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Extensions;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.API.Controllers
{
    [Route("api/menuCategory")]
    [ApiController]
    public class MenuCategoryApiController : ControllerBase
    {
        private readonly IMenuCategoryRepository _menuCategoryRepo;
        private readonly IMenuCategoryService _menuCategoryService;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IWebHostEnvironment _hostingEnv;


        public MenuCategoryApiController(IMenuCategoryRepository MenuCategoryRepo, IMenuCategoryService MenuCategoryService, IWebHostEnvironment hostingEnv, IAttachmentRepository attachmentRepo)
        {
            _menuCategoryRepo = MenuCategoryRepo;
            _menuCategoryService = MenuCategoryService;
            _hostingEnv = hostingEnv;
            _attachmentRepo = attachmentRepo;
        }

        [Authorize("MenuCategory-View")]
        [HttpGet("")]
        public IActionResult GetMenuCategory()
        {
            var userId = GetCurrentUserExtension.GetCurrentUserId(this);

            var MenuCategoryTypes = _menuCategoryRepo.GetQueryable().Where(a=>a.Company.CreatedBy == userId).ToList();
            var MenuCategoryDtos = MenuCategoryTypes.Select(a => new MenuCategoryDto()
            {
                Description = a.Description,
                CompanyId = a.CompanyId,
                Company = a.Company.Name,
                Images = a.Images.Select(x=> new AttachmentDto()
                {
                    FileName = x.Image.FileName,
                    Path = x.Image.Path,
                    UploadedBy = x.Image.UploadedBy,
                    UploadedDateTime = x.Image.UploadedDateTime,
                    Id = x.AttachmentId,
                }).ToList(),
                Status = a.Status,
                Id = a.Id,
                Name = a.Name,
            }).ToList();

            return Ok(new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = MenuCategoryDtos
            });
        }
        [Authorize("MenuCategory-View")]
        [HttpGet("GetCategoryOfCompany")]
        public IActionResult GetCategoryOfCompany(int companyId)
        {

            var MenuCategories = _menuCategoryRepo.GetQueryable().Where(a=>a.CompanyId == companyId).Select(a=> new GenericDropdownDto()
            {
                Id = a.Id,
                Name =a.Name,
            }).ToList();
            
            return Ok(new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = MenuCategories
            });
        }
        [Authorize(Policy = "MenuCategory-AddOrUpdate")]
        [HttpPost("addorupdate")]
        public IActionResult AddOrUpdate([FromForm] MenuCategoryCreateDto model)
        {
            try
            {                

                var files = Request.Form.Files;

                if (files.Any())
                {

                    foreach (var file in files)
                    {
                        model.Images.Add(ConfigureAttachmentCreateModel(file));
                    }
                }

                var CategoryId = _menuCategoryService.AddOrUpdate(model);

                
                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Menu Category Add/Updated Successfully",
                    Data = new
                    {
                        Id = CategoryId,
                    }
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);

                return BadRequest(ex.Message);
            }
        }

        private AttachmentDto ConfigureAttachmentCreateModel(IFormFile? file)
        {
            var FileDir = "Attachments";
            string FullDir = Path.Combine(_hostingEnv.WebRootPath, FileDir);


            if (!Directory.Exists(FullDir))
                Directory.CreateDirectory(FullDir);

            //Fetch the File Name.
            string fileName = Path.GetFileName(file.FileName);


            var filePath = Path.Combine(FullDir, file.FileName);


            //Save the File in Folder.
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
            }
            return new AttachmentDto()
            {
                FileName = fileName,
                UploadedDateTime = DateTime.Now,
                Path = $"/{FileDir}/{fileName}",
                UploadedBy = this.GetCurrentUserId(),
            };
        }
        [Authorize(Policy = "MenuCategory-AddOrUpdate")]
        [HttpPost("RemoveImage")]
        public IActionResult RemoveImage([FromForm]int categoryId,[FromForm]int attachmentId)
        {
            try
            {
                var file = _attachmentRepo.GetById(attachmentId) ?? throw new CustomException("File Not Found");

                _menuCategoryService.RemoveImage(categoryId, attachmentId);
                RemoveFileFromDirectory(file);

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Category Image Deleted.",
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);

                return BadRequest(ex.Message);
            }
        }

        private void RemoveFileFromDirectory(Attachment file)
        {
            var FileDir = "Attachments";
            string FullDir = Path.Combine(_hostingEnv.WebRootPath, FileDir);
            var filePath = Path.Combine(FullDir, file.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);

            }
        }

        [Authorize(Policy = "MenuCategory-Status")]
        [HttpPost("Activate")]
        public IActionResult Activate([FromForm]int categoryId)
        {
            try
            {
                _menuCategoryService.ActivateCategory(categoryId);

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Menu Category Activated.",
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "MenuCategory-Status")]
        [HttpPost("DeActivate")]
        public IActionResult DeActivate([FromForm]int categoryId)
        {
            try
            {
                _menuCategoryService.DeActivateCategory(categoryId);

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Menu Category Deactivated.",
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);

                return BadRequest(ex.Message);
            }
        }

    }
}
