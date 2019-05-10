using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "varchar(64)")]
        [MaxLength(64)]
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
        [Column(TypeName = "varchar(256)")]
        [MaxLength(256)]
        public string Info { get; set; }

    }

}
