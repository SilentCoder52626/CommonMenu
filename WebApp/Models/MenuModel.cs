using DomainModule.Dto.Menu;

namespace WebApp.Models
{
    public class MenuFilterModel
    {
        public int? CompanyId { get; set; }
    }
    public class MenuDetailsViewModel
    {
        public CompanyDto Company { get; set; }
        public MenuCategoryViewModel Categories { get; set; }
    }
   

}
