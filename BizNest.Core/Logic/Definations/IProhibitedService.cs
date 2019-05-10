using System;
using System.Threading.Tasks;
using BizNest.Core.Data.Repository;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;


namespace BizNest.Core.Logic.Definations
{
    public interface IProhibitedService
    {
         Task<List<ProhibitedName>> SearchForWords(string query,int max);

         Task InsertWord (ProhibitedWordModel model);

         Task UpdateWord (ProhibitedWordModel model);

         Task DeleteWord(ProhibitedWordModel model);

    }
}