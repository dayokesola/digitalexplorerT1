using BizNest.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizNest.Core.Domain.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseModel<T>
    {

        /// <summary>
        /// 
        /// </summary> 
        public T Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecordStatus RecordStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual object LogMe()
        {
            return Id;
        }
        /// <summary>
        /// 
        /// </summary>
        //[Ignore]
        public string RecordStatusText
        {
            get
            {
                return RecordStatus.ToString();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        //[Ignore]
        public string CreatedAtText
        {
            get
            {
                return CreatedAt.ToString("dd-MMM-yy HH:mm");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        //[Ignore]
        public string UpdatedAtText
        {
            get
            {
                return UpdatedAt.ToString("dd-MMM-yy HH:mm");
            }
        }
    }
}
