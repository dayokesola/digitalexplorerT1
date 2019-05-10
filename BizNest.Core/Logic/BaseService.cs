using BizNest.Core.Data.DB;
using BizNest.Core.Data.Module;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Module;
using BizNest.Core.Logic.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Logic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TForm"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseService<TEntity, TModel, TForm, TKey> : BaseSingleService
    where TEntity : BaseEntity<TKey>
    where TModel : BaseModel<TKey>
    where TForm : BaseForm<TKey>
    {

        public BaseService(AppDbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseSingleService
    {

        public BaseSingleService(AppDbContext context)
        {
            _context = context;
        }

        private AppDbContext _context;
        private DataModule _data;

        /// <summary>
        /// DB Service Wrapper for repositories
        /// </summary>
        public DataModule DataModule
        {
            get
            {
                if (_data == null)
                {
                    _data = new DataModule(_context);
                }
                return _data;
            }
        }

        private FactoryModule _factory;
        /// <summary>
        /// 
        /// </summary>
        public FactoryModule FactoryModule
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new FactoryModule();
                }
                return _factory;
            }
        }



        private LogicModule _logic;

        /// <summary>
        /// 
        /// </summary>
        public LogicModule Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new LogicModule(_context);
                }
                return _logic;
            }
        }
    }
}
