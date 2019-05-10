using BizNest.Core.Data.DB;
using BizNest.Core.Logic.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Logic.Module
{
    public class LogicModule
    {
        public LogicModule(AppDbContext context)
        {
            _context = context;
        }
        private AppDbContext _context;
        private BusinessTypeService _businesstype;

        /// <summary>
        /// BusinessType Service Module
        /// </summary>
        public BusinessTypeService BusinessTypeService { get { if (_businesstype == null) { _businesstype = new BusinessTypeService(_context); } return _businesstype; } }

    }
}
