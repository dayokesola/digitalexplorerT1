using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Entity.App
{

    /// <summary>
    /// BusinessType Class
    /// </summary>
    public class BusinessType : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MinStakeHolder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxStakeHolder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal MinCapital { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Info { get; set; }

    }
}
