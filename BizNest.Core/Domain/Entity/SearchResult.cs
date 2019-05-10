using System;
using System.Collections.Generic;
using BizNest.Core.Domain.Entity;

namespace BizNest.Core.Domain.Entity
{
    public class SearchResult
    {

        //This is basically the time it to took to search
        public TimeSpan SearchTime { get; set; }

        public List<SearchItem> Results { get; set; }

        public string Summary { get; set; }
    }
}