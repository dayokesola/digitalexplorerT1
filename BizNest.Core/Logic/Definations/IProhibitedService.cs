using System;
using System.Threading.Tasks;
using BizNest.Core.Data.Repository;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Form.App;

namespace BizNest.Core.Logic.Definations
{
    public interface IProhibitedService
    {
         Task<BaseDataModel<ProhibitedForm>> SearchForWords(string query,int skip,int max);

         Task InsertWord (ProhibitedWordModel model);

         Task UpdateWord (ProhibitedWordModel model);

         Task DeleteWord(ProhibitedWordModel model);

    }
}