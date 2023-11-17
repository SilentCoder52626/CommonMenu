using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceModule.Service.Menu;
using WebApp.Extensions;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.API.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemApiController : ControllerBase
    {
        private readonly IItemRepository _itemRepo;
        private readonly IItemService _itemService;
        private readonly IWebHostEnvironment _hostingEnv;


        public ItemApiController(IItemRepository ItemRepo, IItemService ItemService, IWebHostEnvironment hostingEnv)
        {
            _itemRepo = ItemRepo;
            _itemService = ItemService;
            _hostingEnv = hostingEnv;
        }

        [Authorize("Item-View")]
        [HttpGet("")]
        public IActionResult GetItem()
        {
            var ItemTypes = _itemRepo.GetQueryable().ToList();
            var ItemDto = ItemTypes.Select(a => new ItemDto()
            {
                Description = a.Description,
                Image = a.ImageId > 0 ? new AttachmentCreateDto()
                {
                    FileName = a.Image.FileName,
                    Path = a.Image.Path,
                    UploadedBy = a.Image.UploadedBy,
                    UploadedDateTime = a.Image.UploadedDateTime,
                } : null,
                Id = a.Id,
                Name = a.Name,
                Category = a.Category.Name,
                CategoryId = a.CategoryId,
                Price = a.Price,
                CompanyId = a.CompanyId,
                Company = a.Company.Name,
                Status = a.Status
            }).ToList();

            return Ok(new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = ItemDto
            });
        }
        [Authorize(Policy = "Item-AddOrUpdate")]
        [HttpPost("AddOrUpdate")]
        public IActionResult AddOrUpdate([FromForm] ItemCreateDto model)
        {
            try
            {
                var action = "New";
                var currentFilePath = "";
                if (model.Id > 0)
                {
                    action = "Update";
                    currentFilePath = _itemRepo.GetQueryable().Where(a => a.Id == model.Id).Select(a => a.Image.Path).FirstOrDefault();
                }
                
                if (model.file != null)
                {
                    model.Image = ConfigureAttachmentCreateModel(model.file);
                }

                var ItemId = _itemService.AddOrUpdateItem(model);

                if (action == "Update" && model.file != null)
                {
                    if (System.IO.File.Exists(currentFilePath))
                    {
                        System.IO.File.Delete(currentFilePath);
                    }
                }
                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item Created Successfully",
                    Data = new
                    {
                        Id  = ItemId
                    }
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        private AttachmentCreateDto ConfigureAttachmentCreateModel(IFormFile? file)
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
            return new AttachmentCreateDto()
            {
                FileName = fileName,
                UploadedDateTime = DateTime.Now,
                Path = $"/{FileDir}/{fileName}",
                UploadedBy = this.GetCurrentUserId()
            };
        }

        [Authorize(Policy = "Item-Status")]
        [HttpPost("Activate")]
        public IActionResult Activate([FromForm] int itemId)
        {
            try
            {
                _itemService.ActivateStatus(itemId);

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item Activated.",
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "Item-Status")]
        [HttpPost("DeActivate")]
        public IActionResult DeActivate([FromForm] int itemId)
        {
            try
            {
                _itemService.DeactivateStatus(itemId);

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Item Deactivated.",
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
