using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Enum;
using BizNest.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using NPoco;
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
        public BaseRepository(AppDbContext context)
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




        protected List<object> prms;

        public IDatabase Connection
        {
            get
            {
                return AppPoco.DbFactory.GetDatabase();
            }
        }

        public string ApplySort(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return "";
            }
            var lst = new List<string>();
            var lstSort = sort.Split(',');
            foreach (var sortOption in lstSort)
            {
                if (sortOption.StartsWith("-"))
                {
                    lst.Add(sortOption.Remove(0, 1) + " desc");
                }
                else
                {
                    lst.Add(sortOption);
                }
            }
            return " Order by " + string.Join(",", lst);
        }

        public void AddParam(string key, object obj)
        {
            if (prms == null)
            {
                prms = new List<object>();

            }

            prms.Add(obj);
        }


        public NPoco.Page<TModel> SearchView(string sql, long page, long pagesize)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Page<TModel>(page, pagesize, sql);
                    }
                    var t = db.Page<TModel>(page, pagesize, sql, prms.ToArray());
                    prms = null;
                    return t;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public NPoco.Page<TModel> SearchViewSQL(string sql, long page, long pagesize)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Page<TModel>(page, pagesize, sql);
                    }
                    var t = db.Page<TModel>(page, pagesize, sql, prms.ToArray());
                    prms = null;
                    return t;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        public T GetRecordAnon<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<T>(sql).FirstOrDefault();
                    }
                    var t = db.Fetch<T>(sql, prms.ToArray()).FirstOrDefault();
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetListAnon<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<T>(sql);
                    }
                    var t = db.Fetch<T>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }



        public NPoco.Page<T> SearchViewAnon<T>(string sql, long page, long pagesize)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Page<T>(page, pagesize, sql);
                    }
                    var t = db.Page<T>(page, pagesize, sql, prms.ToArray());
                    prms = null;
                    return t;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public TModel GetRecord(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<TModel>(sql).FirstOrDefault();
                    }
                    var t = db.Fetch<TModel>(sql, prms.ToArray()).FirstOrDefault();
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<TModel> GetList(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.Fetch<TModel>(sql);
                    }
                    var t = db.Fetch<TModel>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        public T GetScalar<T>(string sql)
        {
            using (IDatabase db = Connection)
            {
                try
                {
                    if (prms == null)
                    {
                        return db.ExecuteScalar<T>(sql);
                    }
                    var t = db.ExecuteScalar<T>(sql, prms.ToArray());
                    prms = null;
                    return t;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


    }


}
