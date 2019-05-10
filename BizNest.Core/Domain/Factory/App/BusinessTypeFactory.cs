using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Factory.App
{
    /// <summary>
    /// BusinessType Factory
    /// </summary>
    public class BusinessTypeFactory : BaseFactory<BusinessType, BusinessTypeModel, BusinessTypeForm, int>
    {

    }

    /// <summary>
    /// Business Factory
    /// </summary>
    public class BusinessFactory : BaseFactory<Business, BusinessModel, BusinessForm, long>
    {

    }
}
