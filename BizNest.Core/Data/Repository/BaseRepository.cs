using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Enum;
using BizNest.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizNest.Core.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseRepository<TEntity, TModel, TKey>
where TEntity : BaseEntity<TKey>
where TModel : BaseModel<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public DbContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual TEntity Update(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.UtcNow;
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
                return obj;
            }
            catch (Exception e)
            {
                //var newException = new FormattedDbEntityValidationException(e);
                throw e;
            }
        }

        public virtual int Delete(TEntity obj)
        {
            obj.UpdatedAt = DateTime.UtcNow;
            obj.RecordStatus = RecordStatus.Deleted;
            _context.Entry(obj).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public virtual TEntity Insert(TEntity obj)
        {
            try
            {
                obj.UpdatedAt = DateTime.UtcNow;
                obj.CreatedAt = DateTime.UtcNow;
                _context.Set<TEntity>().Add(obj);
                _context.SaveChanges();
                return obj;
            }
            catch (Exception e)
            {
                //var newException = new FormattedDbEntityValidationException(e);
                throw e;
            }
        }

        public virtual int DeleteFinally(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            return _context.SaveChanges();
        }

        public virtual int Count()
        {
            return Query().Count();
        }

        public virtual IQueryable<TEntity> Query(string includeProperties = "")
        {
            return All(includeProperties)
                .Where(x => x.RecordStatus != RecordStatus.Deleted && x.RecordStatus != RecordStatus.Archive);
        }

        public virtual IQueryable<TEntity> All(string includeProperties = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public bool Exists(TKey id)
        {
            var i = _context.Set<TEntity>().Find(id);
            if (i != null)
            {
                if (i.RecordStatus == RecordStatus.Deleted || i.RecordStatus == RecordStatus.Archive)
                {
                    i = null;
                }
            }
            return i != null;
        }

        public TEntity Get(TKey id)
        {
            var i = _context.Set<TEntity>().Find(id);
            if (i != null)
            {
                if (i.RecordStatus == RecordStatus.Deleted || i.RecordStatus == RecordStatus.Archive)
                {
                    i = null;
                }
                else
                {
                    _context.Entry<TEntity>(i).Reload();
                }
            }
            return i;
        }

        public virtual TEntity DeleteUndo(TEntity obj)
        {
            obj.UpdatedAt = DateTime.Now;
            obj.RecordStatus = RecordStatus.Active;
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return obj;
        }

         
         


        
    }


}
