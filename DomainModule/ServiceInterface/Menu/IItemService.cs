using DomainModule.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.ServiceInterface.Menu
{
    public interface IItemService
    {
        int AddOrUpdateItem(ItemCreateDto model);
        void BulkUpdateItem(List<ItemCreateDto> model);
        void ActivateStatus(int id);
        void DeactivateStatus(int id);
    }
}
