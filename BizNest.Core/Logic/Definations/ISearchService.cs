using System;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Entity.App;

namespace BizNest.Core.Logic.Definations
{

    //Core infrastructore ....data here should be persisted only on the infrastructure layer
    public interface ISearchService
    {

        //Core search
         Task<SearchResult> SearchAsync(string query);

         Task InsertBusinessNamesAsync(params Business[] businesses);

         Task InsertProhibitedNameAsync(params ProhibitedName[] names);


         Task RemoveBusinessNameAsync(Business name);

        Task RemoveProhibitedNameAsync(ProhibitedName name);
         
    }
}