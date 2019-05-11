using BizNest.Core.Data.Repository.App;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Data.DB;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;
using System;
using BizNest.Core.Common;
using System.Linq;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Form.App;


namespace BizNest.Core.Logic.App
{
    public class StakeHolderService : IStakeHolderService
    {
        StakeHolderRepository repo;
        public StakeHolderService(AppDbContext context)
        {
            this.repo = new StakeHolderRepository(context);
        }

        public async Task DeleteAsync(long id)
        {
            var item = await Task.Run(() => repo.Get(id));
            if (item is null) throw new ArgumentException("The item was not found");
            await Task.Run(() => repo.Delete(item));
        }

        public async Task<BaseDataModel<StakeHolderForm>> GetAllAsync()
        {
            var query = repo.Query();
           
            return new BaseDataModel<StakeHolderForm>
            {
                Items = query.OrderByDescending(x => x.Id).Select(x => DataMapper.Map<StakeHolderForm, StakeHolder>(x)).ToList(),
                Total = query.LongCount()
            };
        }

        public async Task<StakeHolderForm> GetSingleAsync(long id)
        {
            var item = await Task.Run(() => repo.Get(id));
            if (item is null) return null;
            return item is null ? null : DataMapper.Map<StakeHolderForm, StakeHolder>(item);
        }

        public async Task<List<StakeHolderForm>> GetStakeHoldersByBusinessId(long businessId)
        {
            var query = repo.Query().Where(x=>x.BusinessId == businessId);
            return await Task.Run(()=> query.Select(x => DataMapper.Map<StakeHolderForm, StakeHolder>(x)).ToList());
        }

        public async Task<StakeHolderForm> InsertAsync(StakeHolderModel model)
        {
            if (model.Id < 1) throw new ArgumentException("Please provide a valid business type");
            var obj = DataMapper.Map<StakeHolder, StakeHolderModel>(model);
            var item = await Task.Run(() => repo.Insert(obj));
           
            return DataMapper.Map<StakeHolderForm, StakeHolder>(item);
        }

        public async Task<StakeHolderForm> UpdateAsync(StakeHolderModel model)
        {
            if ((await GetSingleAsync(model.Id)) is null) throw new ArgumentException("Cannot locate business");
            var item = DataMapper.Map<StakeHolder, StakeHolderModel>(model);
           
            await Task.Run(() => repo.Update(item));
            return DataMapper.Map<StakeHolderForm, StakeHolder>(item);
        }
    }
}
