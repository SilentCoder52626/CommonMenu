using DomainModule.Dto.Menu;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using WebApp.ViewModel;

namespace WebApp.Areas.API.Controllers
{
    [Route("api/companyType")]
    [ApiController]
    public class CompanyTypeApiController : ControllerBase
    {
        private readonly ICompanyTypeRepository _typeRepo;
        private readonly ICompanyTypeService _typeService;

        public CompanyTypeApiController(ICompanyTypeRepository typeRepo, ICompanyTypeService typeService)
        {
            _typeRepo = typeRepo;
            _typeService = typeService;
        }

        [Authorize(Policy = "CompanyType-View")]
        [HttpGet("")]
        public IActionResult GetCompanyTypes()
        {
            var users = _typeRepo.GetQueryable().ToList();
            var returnModels = users.Select(a => new CompanyTypeDto()
            {
                Code = a.Code,
                Id = a.Id,
                Name = a.Name,
            }).ToList();
            return Ok(returnModels);
        }
        [Authorize(Policy = "CompanyType-Create")]
        [HttpPost("create")]
        public IActionResult Create(CompanyTypeCreateDto model)
        {
            try
            {
                _typeService.Create(model);
                return Ok("Company Type Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Policy = "CompanyType-Update")]
        [HttpPost("update")]
        public IActionResult Update(CompanyTypeDto model)
        {
            try
            {
                _typeService.Update(model);
                return Ok("Company Type Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
