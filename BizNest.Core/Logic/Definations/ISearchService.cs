using System;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity;

namespace BizNest.Core.Logic.Definations
{

    //Core infrastructore ....data here should be persisted only on the infrastructure layer
    public interface ISearchService
    {

        //Core search
         Task<SearchResult> SearchAsync(string query);

         Task InsertBusinessNamesAsync(params string[] names);

         Task InsertProhibitedNameAsync(params string[] names);


         Task RemoveBusinessNameAsync(string name);

        Task RemoveProhibitedNameAsync(string name);
         
    }
}