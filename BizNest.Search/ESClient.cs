using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Search
{
    public class SearchClient
    {
        ElasticClient client;
        public SearchClient()
        {
            var settings = new ConnectionSettings(new Uri("http://example.com:9200"))
    .DefaultIndex("people");

            var client = new ElasticClient(settings);
        }
    }
}
