using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.Enums;
using DomainModule.RepositoryInterface.Menu;
using InfrastructureModule.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Repository.Menu
{
    public class MenuCategoryRepository : BaseRepository<MenuCategory>, IMenuCategoryRepository
    {
        public MenuCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public MenuCategoryModel GetAllMenuCategory(string userId)
        {
            var datas = GetQueryable().Where(a=>a.Company.CreatedBy == userId).Select(a => new MenuCategoryDto()
            {
                Description = a.Description,
                CompanyId = a.CompanyId,
                Company = a.Company.Name,
                Id = a.Id,
                Name = a.Name,
                Status = a.Status,
                Images = a.Images.Select(x => new AttachmentDto()
                {
                    FileName = x.Image.FileName,
                    Path = x.Image.Path,
                    UploadedBy = x.Image.UploadedBy,
                    UploadedDateTime = x.Image.UploadedDateTime,
                    Id = x.AttachmentId,
                }).ToList()
            }).ToList();
            var model = new MenuCategoryModel()
            {
                MenuCategories = datas
            };
            return model;
        }

        public List<GenericDropdownDto> GetMenuCategoryDropDown(string userId)
        {
            return GetQueryable().Where(a => a.Status == Status.Active.ToString() && a.Company.CreatedBy == userId).Select(a => new GenericDropdownDto()
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
    }
}
