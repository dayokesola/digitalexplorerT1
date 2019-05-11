using System;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;

namespace BizNest.Core.Logic.Definations
{
    public interface IStakeHolderService
    {
        Task<BaseDataModel<StakeHolderForm>> GetAllAsync();

        Task<List<StakeHolderForm>> GetStakeHoldersByBusinessId(long businessId);

        Task<StakeHolderForm> GetSingleAsync(long id);

        Task<StakeHolderForm> InsertAsync(StakeHolderModel model);

        Task<StakeHolderForm> UpdateAsync(StakeHolderModel model);

        Task DeleteAsync(long id);
    }
}
