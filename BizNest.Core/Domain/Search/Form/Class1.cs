using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Search.Form
{  
    public class SearchForm
    {
        public Query query { get; set; }

        public void SetName(string name)
        {
            query = new Query()
            {
                match = new Match()
                {
                    name = new Name()
                    {
                        fuzziness = 2,
                        query = name,
                        prefix_length = 1
                    }
                }
            };
        }
    }

    public class Query
    {
        public Match match { get; set; }
    }

    public class Match
    {
        public Name name { get; set; }
    }

    public class Name
    {
        public string query { get; set; }
        public int fuzziness { get; set; }
        public int prefix_length { get; set; }
    }

}
