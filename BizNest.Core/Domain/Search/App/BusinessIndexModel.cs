using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Search.App
{
    [ElasticsearchType(Name = "business")]
    public class BusinessIndexModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
        public int AddressCountryId { get; set; }
    }
}
