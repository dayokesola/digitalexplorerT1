using System;
using System.Collections.Generic;


namespace BizNest.Core.Domain.Form
{
    public class BaseDataModel<T>
    {
        public List<T> Items { get; set; }

        public long Total { get; set; }
    }
}