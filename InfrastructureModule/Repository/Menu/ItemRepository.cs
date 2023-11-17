using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.RepositoryInterface.Menu;
using InfrastructureModule.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Repository.Menu
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(AppDbContext context) : base(context)
        {
        }

        public ItemModel GetAllItem(string userId)
        {
            var Items =  GetQueryable().Where(a=>a.Company.CreatedBy == userId).Select(a => new ItemDto()
            {
                Category = a.Category.Name,
                CategoryId = a.CategoryId,
                CompanyId = a.CompanyId,
                Company = a.Company.Name,
                Description = a.Description,
                Id = a.Id,
                Name = a.Name,
                Image = a.ImageId > 0 ? new AttachmentCreateDto()
                {
                    FileName = a.Image.FileName,
                    Path = a.Image.Path,
                    UploadedBy = a.Image.UploadedBy,
                    UploadedDateTime = a.Image.UploadedDateTime,
                } : null,
                Price = a.Price,
                Status = a.Status
            }).ToList();
            var model = new ItemModel()
            {
                Items = Items
            };
            return model;
        }
    }
}
