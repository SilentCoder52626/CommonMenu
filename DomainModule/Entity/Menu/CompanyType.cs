﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.Entity.Menu
{
    public class CompanyType
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Code { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
