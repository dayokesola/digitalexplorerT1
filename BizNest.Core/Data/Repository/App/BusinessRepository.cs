using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizNest.Core.Data.Repository.App
{
    public class BusinessRepository : BaseRepository<Business,BusinessModel,long>
    {
        public BusinessRepository(AppDbContext context) : base(context)
        {
            
        }

        public IQueryable<Business> Search(string name,string contact,string token)
        {
            name = name.ToLower();
            contact = contact.ToLower();
            var table = Query();
            if(!string.IsNullOrEmpty(name))
            {
                table = table.Where(x=>x.Name.ToLower().Contains(name));
            }
            if (!string.IsNullOrEmpty(contact))
            {
                table = table.Where(x=>x.Contact1Email.ToLower().Contains(contact) || x.Contact1Name.ToLower().Contains(contact) || x.Contact2Email.ToLower().Contains(contact) || x.Contact2Name.Contains(contact) );
            }
            if(!string.IsNullOrEmpty(token))
            {
                table = table.Where(x=>x.Code == token);
            }

            return table;
        }


        public Business GetSingleById(long id)
        {
            var item = Query().Where(x=>x.Id == id).FirstOrDefault();
            return item;
        }

        public Business GetSingleByToken(string token)
        {
            var item = Query().Where(x=>x.Code == token).FirstOrDefault();
            return item;
        }
    }
}