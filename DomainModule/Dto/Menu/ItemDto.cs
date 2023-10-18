using DomainModule.Entity.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class ItemDto : ItemCreateDto
    {
        public string Category { get; set; }

    }
    public class ItemCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public AttachmentCreateDto Image { get; set; }

    }
}
