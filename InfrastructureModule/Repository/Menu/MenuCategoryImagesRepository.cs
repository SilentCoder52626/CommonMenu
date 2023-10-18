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
    public class MenuCategoryImagesRepository : BaseRepository<MenuCategoryImages>, IMenuCategoryImagesRepository
    {
        public MenuCategoryImagesRepository(AppDbContext context) : base(context)
        {
        }

        public MenuCategoryImages? GetByAttachmentIdandCategoryId(int categoryId, int attachmentId)
        {
            return this.GetQueryable().Where(a => a.AttachmentId == attachmentId && a.CategoryId == categoryId).FirstOrDefault();
        }
    }
}
