using DomainModule.Dto;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Net;
using WebApp.Extensions;
using WebApp.Helper;
using WebApp.Models;

namespace WebApp.Areas.API.Controllers
{
    [Route("api/attachment")]
    [ApiController]
    public class AttachmentApiController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IAttachmentService _attachmentService;

        public AttachmentApiController(IAttachmentRepository attachmentRepo, IWebHostEnvironment hostingEnv, IAttachmentService attachmentService)
        {
            _attachmentRepo = attachmentRepo;
            _hostingEnv = hostingEnv;
            _attachmentService = attachmentService;
        }

        [Authorize("Attachment-View")]
        [HttpGet("")]
        public IActionResult GetAttachments()
        {
            var attachments = _attachmentRepo.GetQueryable().Select(a => new
            {
                Id = a.Id,
                FileName = a.FileName,
                UploadedDate = a.UploadedDateTime,
                FilePath = a.Path
            }).ToList();
            return Ok(new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = attachments
            });
        }
        [Authorize("Attachment-Upload")]
        [HttpDelete("Delete/{id:int}")]
        public IActionResult DeleteAttachment(int id)
        {
            try
            {
                var file = _attachmentRepo.GetById(id) ?? throw new CustomException("File Not Found");

                var result =  _attachmentService.Delete(id);
                if (result)
                {
                    var FileDir = "Attachments";
                    string FullDir = Path.Combine(_hostingEnv.ContentRootPath, FileDir);
                    var filePath = Path.Combine(FullDir, file.FileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);

                    }

                    return Ok(new ApiResponseModel()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Attachment Delted Successfully",
                        Data = new
                        {
                            Id = file.Id,
                            FileName = file.FileName,
                        }

                    });
                }
                else
                {
                    throw new CustomException("Error while deleting attachment. Please contact adminstration.");
                }
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);

            }
        }

        [Authorize("Attachment-Upload")]
        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file)
        {
            try
            {

                var FileDir = "Attachments";
                string FullDir = Path.Combine(_hostingEnv.ContentRootPath, FileDir);


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
                var attachmentId = _attachmentService.Create(new AttachmentCreateDto()
                {
                    FileName = fileName,
                    UploadedDateTime = DateTime.Now,
                    Path = $"/{FileDir}/{fileName}",
                    UploadedBy = this.GetCurrentUserId()
                });

                return Ok(new ApiResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Attachment Uploaded Successfully",
                    Data = new
                    {
                        FileName = fileName,
                        AttachmentId = attachmentId,
                        Path = $"/{FileDir}/{fileName}",
                    }

                });



            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex.Message,ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
