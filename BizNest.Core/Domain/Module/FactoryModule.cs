using BizNest.Core.Domain.Factory.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Module
{
    public class FactoryModule
    {
        private BusinessTypeFactory _businesstype;

        /// <summary>
        /// BusinessType Factory Module
        /// </summary>
        public BusinessTypeFactory BusinessTypes { get { if (_businesstype == null) { _businesstype = new BusinessTypeFactory(); } return _businesstype; } }

    }
}
