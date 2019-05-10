using BizNest.Core.Data.DB;
using BizNest.Core.Data.Repository.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Data.Module
{
    public class DataModule
    {

        private AppDbContext context;
        private AppDbContext _context
        {
            get
            {
                if (context == null)
                {
                    context = new AppDbContext();
                }
                return context;
            }
        }

        private BusinessTypeRepository _businesstypes;
        public BusinessTypeRepository BusinessTypes { get { if (_businesstypes == null) { _businesstypes = new BusinessTypeRepository(_context); } return _businesstypes; } }


    }
}
