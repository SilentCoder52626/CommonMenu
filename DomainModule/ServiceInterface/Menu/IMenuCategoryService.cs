using DomainModule.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.ServiceInterface.Menu
{
    public interface IMenuCategoryService
    {
        int AddOrUpdate(MenuCategoryCreateDto model);
        void ActivateCategory(int id);
        void DeActivateCategory(int id);
        void RemoveImage(int categoryId,int attachmentId);
    }
}
