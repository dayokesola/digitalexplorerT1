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
    public interface IBusinessService
    {
        Task<BaseDataModel<BusinessForm>> SearchForBusinessAsync(string name,string contact,string token,int skip,int max);

        Task<BusinessForm> InsertBusinessAsync(BusinessModel model);

        Task<BusinessForm> UpdateBusinessAsync(BusinessModel model);

        Task ChangeStatus(BusinessStatusChangeModel model);

        Task<BusinessForm> GetById(long id);

        Task<BusinessForm> GetByToken(string token);

    }
}
