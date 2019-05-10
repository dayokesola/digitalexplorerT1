using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Form.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizNest.Core.Logic.App
{


    /// <summary>
    /// BusinessType Service
    /// </summary>
    public partial class BusinessTypeService : BaseService<BusinessType, BusinessTypeModel, BusinessTypeForm, int>
    {

    }


    /// <summary>
    /// BusinessType Service
    /// </summary>
    public partial class BusinessTypeService : BaseService<BusinessType, BusinessTypeModel, BusinessTypeForm, int>
    {
        /// <summary>
        /// IQueryable BusinessType Entity Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public IQueryable<BusinessType> Search(string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "")
        {
            return DataModule.BusinessTypes.Search(name, minStakeHolder, maxStakeHolder, minCapital, info);
        }


        /// <summary>
        /// IEnumerable BusinessType Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public IEnumerable<BusinessTypeModel> SearchModel(string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "")
        {
            return DataModule.BusinessTypes.Search(name, minStakeHolder, maxStakeHolder, minCapital, info)
                .Select(FactoryModule.BusinessTypes.CreateModel);
        }


        /// <summary>
        /// Paged BusinessType Model Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// <param name="page"></param>
        ///<param name="pageSize"></param>
        ///<param name="sort"></param>
        /// <returns></returns>
        public Page<BusinessTypeModel> SearchView(string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "",
            long page = 1, long pageSize = 10, string sort = "")
        {
            return DataModule.BusinessTypes.SearchView(name, minStakeHolder, maxStakeHolder, minCapital, info, page, pageSize, sort);
        }

        /// <summary>
        /// Create BusinessType Model from BusinessType Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BusinessTypeModel Create(BusinessType entity)
        {
            return FactoryModule.BusinessTypes.CreateModel(entity);
        }

        /// <summary>
        /// Create BusinessType Model from BusinessType Form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public BusinessTypeModel Create(BusinessTypeForm form)
        {
            return FactoryModule.BusinessTypes.CreateModel(form);
        }

        /// <summary>
        /// Create BusinessType Entity from BusinessType Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessType Create(BusinessTypeModel model)
        {
            return FactoryModule.BusinessTypes.CreateEntity(model);
        }

        /// <summary>
        /// Check Uniqueness of BusinessType before creation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateExists(BusinessTypeModel model)
        {
            return DataModule.BusinessTypes.ItemExists(model);
        }

        /// <summary>
        /// Delete BusinessType
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(BusinessType entity)
        {
            return DataModule.BusinessTypes.Delete(entity);
        }

        /// <summary>
        /// Get BusinessType Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessType Get(int id)
        {
            return DataModule.BusinessTypes.Get(id);
        }



        /// <summary>
        /// Get BusinessType Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessTypeModel GetModel(int id)
        {
            return DataModule.BusinessTypes.GetModel(id);
        }

        /// <summary>
        /// Insert new BusinessType to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public BusinessTypeModel Insert(BusinessTypeModel model, bool check = true)
        {
            if (check)
            {
                var routeSearch = DataModule.BusinessTypes.ItemExists(model);
                if (routeSearch)
                {
                    throw new Exception("BusinessType Name already exists");
                }
            }
            var entity = FactoryModule.BusinessTypes.CreateEntity(model);
            entity.RecordStatus = Core.Domain.Enum.RecordStatus.Active;
            DataModule.BusinessTypes.Insert(entity);
            return FactoryModule.BusinessTypes.CreateModel(entity);
        }

        /// <summary>
        /// Update a BusinessType Entity with a BusinessType Model with selected fields
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BusinessType Patch(BusinessType entity, BusinessTypeModel model, string fields)
        {
            return FactoryModule.BusinessTypes.Patch(entity, model, fields);
        }

        /// <summary>
        /// Update BusinessType, with Patch Options Optional
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BusinessTypeModel Update(BusinessType entity, BusinessTypeModel model = null, string fields = "")
        {
            if (model != null)
            {
                entity = Patch(entity, model, fields);
            }
            return FactoryModule.BusinessTypes.CreateModel(DataModule.BusinessTypes.Update(entity));
        }

        /// <summary>
        /// Check Uniqueness of BusinessType before update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateExists(BusinessTypeModel model)
        {
            return DataModule.BusinessTypes.ItemExists(model, model.Id);
        }

    }
}
