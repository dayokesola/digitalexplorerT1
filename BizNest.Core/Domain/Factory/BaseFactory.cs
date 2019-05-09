using BizNest.Core.Common;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BizNest.Core.Domain.Factory
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TForm"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BaseFactory<TEntity, TModel, TForm, TKey>
        where TEntity : BaseEntity<TKey>
        where TModel : BaseModel<TKey>
        where TForm : BaseForm<TKey>

    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private object Shape<T>(T obj, string fields)
        {
            List<string> lstOfFields = new List<string>();
            if (string.IsNullOrEmpty(fields))
            {
                return obj;
            }
            lstOfFields = fields.Split(',').ToList();
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);
            if (!lstOfFieldsToWorkWith.Any())
            {
                return obj;
            }
            else
            {
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    try
                    {
                        var fieldValue = obj.GetType()
                        .GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(obj, null);
                        ((IDictionary<String, Object>)objectToReturn).Add(field.Trim(), fieldValue);
                    }
                    catch
                    {
                        //Log.Error(ex);
                    }

                }
                return objectToReturn;
            }
        }
  

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="src"></param>
        /// <param name="obj"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public T1 Patch<T1, T2>(T1 src, T2 obj, string fields)
        {
            List<string> lstOfFields = new List<string>();
            if (string.IsNullOrEmpty(fields))
            {
                return src;
            }
            lstOfFields = fields.Split(',').ToList();
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);
            if (!lstOfFieldsToWorkWith.Any())
            {
                return src;
            }
            else
            {
                foreach (var field in lstOfFieldsToWorkWith)
                {

                    var fieldProp = obj.GetType()
                        .GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    var fieldValue = fieldProp.GetValue(obj, null);
                    var property = src.GetType()
                        .GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    object safeValue = (fieldValue == null) ? null : Convert.ChangeType(fieldValue, t);
                    property.SetValue(src, fieldValue, null);




                }
                return src;
            }

        }


        /// <summary>
        /// Create an Entity Type from a Model Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TEntity CreateEntity(TModel obj)
        {
            return DataMapper.Map<TEntity, TModel>(obj);
        }

        /// <summary>
        /// Create a Form Type from a Model Type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TForm CreateFrom(TModel obj)
        {
            return DataMapper.Map<TForm, TModel>(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TModel CreateModel(TEntity obj)
        {
            return DataMapper.Map<TModel, TEntity>(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TModel CreateModel(TForm obj)
        {
            return DataMapper.Map<TModel, TForm>(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TModel> ListOfModel()
        {
            return new List<TModel>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TEntity DefaultEntity()
        {
            return (TEntity)Activator.CreateInstance(typeof(TEntity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> ListOfEntity()
        {
            return new List<TEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TForm DefaultForm()
        {
            return (TForm)Activator.CreateInstance(typeof(TForm));
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual TModel DefaultModel()
        {
            return (TModel)Activator.CreateInstance(typeof(TModel));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TForm> ListOfForm()
        {
            return new List<TForm>();
        }



    }

}
