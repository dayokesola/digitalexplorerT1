using System.Collections.Generic;

namespace BizNest.Core.Domain.Model
{
    public class AppSettingsModel
    {
        public string SmtpHost { get; set; }
        public string EmailRecipients { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public Page()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public long CurrentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long TotalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long TotalItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ItemsPerPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<T> Items { get; set; }

    }
}
