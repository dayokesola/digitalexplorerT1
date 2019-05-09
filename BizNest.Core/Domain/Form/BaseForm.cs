using BizNest.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Form
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseForm<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecordStatus RecordStatus { get; set; }
    }
}
