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
        public DataModule(AppDbContext context)
        {
            _context = context;
        }

        private AppDbContext _context;

        private BusinessTypeRepository _businesstypes;
        public BusinessTypeRepository BusinessTypes { get { if (_businesstypes == null) { _businesstypes = new BusinessTypeRepository(_context); } return _businesstypes; } }


    }
}
