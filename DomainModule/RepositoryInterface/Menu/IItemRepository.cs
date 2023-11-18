using DomainModule.BaseRepo;
using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.RepositoryInterface.Menu
{
    public interface IItemRepository : BaseRepositoryInterface<Item>
    {
        ItemModel GetAllItem(string userId);
        ItemModel GetAllFilteredItem(ItemFilterModel filter,string userId);
    }
}
