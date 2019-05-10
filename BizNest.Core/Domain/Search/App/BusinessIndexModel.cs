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
        [Text(Name = "name", Fielddata = true)]
        public string Name { get; set; }
        public int AddressCountryId { get; set; }
    }
}
