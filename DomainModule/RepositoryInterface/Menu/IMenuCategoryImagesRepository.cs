﻿using DomainModule.BaseRepo;
using DomainModule.Entity.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.RepositoryInterface.Menu
{
    public interface IMenuCategoryImagesRepository : BaseRepositoryInterface<MenuCategoryImages>
    {
        MenuCategoryImages? GetByAttachmentIdandCategoryId(int categoryId, int attachmentId);
    }
}
