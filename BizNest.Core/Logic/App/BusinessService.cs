using System;
using System.Threading.Tasks;
using BizNest.Core.Data.DB;
using BizNest.Core.Data.Repository.App;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Common;
using BizNest.Core.Domain.Entity.App;
using System.Linq;

namespace BizNest.Core.Logic.App
{
    public class BusinessService : IBusinessService
    {
        BusinessRepository repo;
        private readonly IEmailService emailService;
        private readonly ISearchService searchService;

        public BusinessService(AppDbContext context,IEmailService emailService,ISearchService searchService)
        {
            repo = new BusinessRepository(context);
            this.emailService = emailService;
            this.searchService = searchService;
        }

        public async Task ChangeStatus(BusinessStatusChangeModel model)
        {
            var item = await Task.Run(() => repo.GetSingleById(model.BusinessId));
            if (item is null) throw new ArgumentException("Cannot find the business");

            item.Status = model.Status;
            await Task.Run(() => repo.Update(item));
            var message = string.IsNullOrEmpty(model.Message) ? $"The status of your application has been moved to {model.Status}" : $"The status of your application has been moved to {model.Status} and the team commented '{model.Message}'";
            await emailService?.SendEmailAsync(item.Contact1Email, "hello@biznest.com", "Business Registration Update", message);

        }

        public async Task<BusinessForm> GetById(long id)
        {
            var item = await Task.Run(()=> repo.GetSingleById(id));
            if (item is null) return null;
            return item is null ? null : DataMapper.Map<BusinessForm, Business>(item);
        }

        public async Task<BusinessForm> GetByToken(string token)
        {
            var item = await Task.Run(() => repo.GetSingleByToken(token));
            if (item is null) return null;
            return item is null ? null : DataMapper.Map<BusinessForm, Business>(item);
        }

        public async Task<BusinessForm> InsertBusinessAsync(BusinessModel model)
        {
            if (model.BusinessTypeId < 1) throw new ArgumentException("Please provide a valid business type");
            var obj = DataMapper.Map<Business, BusinessModel>(model);
            var item = await Task.Run(() => repo.Insert(obj));
            await searchService.InsertBusinessNamesAsync(item);
            return DataMapper.Map<BusinessForm, Business>(item);
        }

        public async Task<BaseDataModel<BusinessForm>> SearchForBusinessAsync(string name, string contact, string token, int skip, int max)
        {
            var query = repo.Search(name, contact, token);
            max = Math.Min(max, 30);
            return new BaseDataModel<BusinessForm>
            {
                Items = query.OrderByDescending(x=>x.Id).Skip(skip).Take(max).Select(x=> DataMapper.Map<BusinessForm,Business>(x)).ToList(),
                Total = query.LongCount()
            };
        }

        private string GetRandomString()
        {
            string path = System.IO.Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public async Task<BusinessForm> UpdateBusinessAsync(BusinessModel model)
        {
            if ((await GetById(model.Id)) is null) throw new ArgumentException("Cannot locate business");
            var item = DataMapper.Map<Business, BusinessModel>(model);
            if(item.Status != RegistrationStatus.Start && string.IsNullOrEmpty(item.Code)) item.Code = GetRandomString();
            await Task.Run(() => repo.Update(item));
            return DataMapper.Map<BusinessForm, Business>(item);
        }
    }
}
