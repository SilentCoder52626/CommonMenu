using DomainModule.Entity;
using DomainModule.RepositoryInterface.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Report.Controllers
{
    [Area("Report")]
    public class CompanyReportController : Controller
    {
        private readonly ICompanyRepository _companyRepo;

        public CompanyReportController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }
        [Authorize("Report-Company")]
        public IActionResult Index()
        {
            var model = _companyRepo.GetAllCompany();
            return View(model);
        }
    }
}
