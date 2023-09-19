using DomainModule.Entity.Menu;
using DomainModule.RepositoryInterface.Menu;
using InfrastructureModule.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureModule.Repository.Menu
{
    public class CompanyTypeRepository : BaseRepository<CompanyType>, ICompanyTypeRepository
    {
        public CompanyTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
