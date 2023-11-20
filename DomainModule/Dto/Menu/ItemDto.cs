using DomainModule.Entity.Menu;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class BulkItemModel
    {
        public List<ItemCreateDto> ItemCreateDtos { get;set;} = new List<ItemCreateDto>();
        public int CategoryId { get;set;}
        public int CompanyId { get;set;}
    }
    public class ItemFilterModel
    {
        public int? CompanyId { get; set; }
        public int? CategoryId { get; set; }
    }
    public class ItemModel
    {
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
        public ItemFilterModel Filter { get; set; }
    }
    public class ItemDto : ItemCreateDto
    {
        public string Category { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }


    }
    public class ItemCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public AttachmentCreateDto? Image { get; set; }
        public IFormFile? file { get; set; }

    }
}
