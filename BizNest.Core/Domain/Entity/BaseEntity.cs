using BizNest.Core.Common;
using BizNest.Core.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizNest.Core.Data.Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseEntity()
        {
            CreatedAt = Util.CurrentDateTime();
            UpdatedAt = Util.CurrentDateTime();
        }
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public T Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecordStatus RecordStatus { get; set; }


        /// <summary>
        /// Date Record was created
        /// </summary> 
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last updated date
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
