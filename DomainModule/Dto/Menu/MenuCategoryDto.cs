using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Dto.Menu
{
    public class MenuCategoryDto : MenuCategoryCreateDto
    {
        public string Status { get; set; }
    }
    public class MenuCategoryCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AttachmentCreateDto> Images { get; set; } = new List<AttachmentCreateDto>();

    }
}
