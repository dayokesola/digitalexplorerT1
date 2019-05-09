using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Form.App
{
    public class BusinessTypeForm : BaseForm<int>
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
