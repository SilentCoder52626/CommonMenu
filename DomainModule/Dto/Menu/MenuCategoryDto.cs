﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class MenuCategoryModel
    {
        public List<MenuCategoryDto> MenuCategories { get; set; } = new List<MenuCategoryDto>();
    }
    public class MenuCategoryDto : MenuCategoryCreateDto
    {
        public string Status { get; set; }
    }
    public class MenuCategoryCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AttachmentDto> Images { get; set; } = new List<AttachmentDto>();

    }
}