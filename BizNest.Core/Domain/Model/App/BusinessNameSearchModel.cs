using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Model.App
{
    public class BusinessNameSearchModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Match { get; set; }
    }

    public class BusinessSearchModel
    {
        public List<BusinessNameSearchModel> Results { get; set; }
        public int Time { get; set; }
        public float MaxScore { get; set; }
    }
}
