﻿using DomainModule.BaseRepo;
using DomainModule.Dto;
using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.RepositoryInterface.Menu
{
    public interface IMenuCategoryRepository : BaseRepositoryInterface<MenuCategory>
    {
        MenuCategoryModel GetAllMenuCategory(string userId);
        MenuCategoryModel GetAllFilteredMenuCategory(MenuCategoryFilterModel filter, string userId);
        List<GenericDropdownDto> GetMenuCategoryDropDown(string userId);
    }
}
