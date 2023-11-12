using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
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
    [Route("api/company")]
    [ApiController]
    public class CompanyApiController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _hostingEnv;


        public CompanyApiController(ICompanyRepository companyRepo, ICompanyService companyService, IWebHostEnvironment hostingEnv)
        {
            _companyRepo = companyRepo;
            _companyService = companyService;
            _hostingEnv = hostingEnv;
        }

        [Authorize("Company-View")]
        [HttpGet("")]
        public IActionResult GetCompany()
        {
            var companyTypes = _companyRepo.GetQueryable().ToList();
            var CompanyDtos = companyTypes.Select(a => new CompanyDto()
            {
                Address = a.Address,
                CompanyType = a.CompanyType?.Name ?? String.Empty,
                CompanyTypeId = a.CompanyTypeId,
                Description = a.Description,
                Email = a.Email,
                LandLineNumber = a.LandLineNumber,
                LogoModel = new AttachmentCreateDto()
                {
                    FileName = a.Attachment.FileName,
                    Path = a.Attachment.Path,
                    UploadedBy = a.Attachment.UploadedBy,
                    UploadedDateTime = a.Attachment.UploadedDateTime
                },
                MobileNumber = a.MobileNumber,
                Website = a.Website,
                Id = a.Id,
                Name = a.Name,
            }).ToList();
            return Ok(new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = CompanyDtos
            });
        }
        [Authorize(Policy = "Company-Create")]
        [HttpPost("create")]
        public IActionResult Create([FromForm] CompanyCreateDto model)
        {
            try
            {
                var action = "New";
                var currentFilePath = "";
                if (model.Id > 0)
                {
                    action = "Update";
                    currentFilePath = _companyRepo.GetQueryable().Where(a => a.Id == model.Id).Select(a => a.Attachment.Path).FirstOrDefault();
                }
                else
                {
                    if (model.file == null)
                    {
                        throw new CustomException("Company Logo is needed to create company.");
                    }
                }
                AttachmentCreateDto? attachmentCreateDto = null;
                if (model.file != null)
                {
                    attachmentCreateDto = ConfigureAttachmentCreateModel(model.file);
                }

                var CompanyId = _companyService.AddOrUpdate(model, attachmentCreateDto);

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
                    Message = "Company Created Successfully",
                    Data = new
                    {
                        Id  = CompanyId
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
    }
}
