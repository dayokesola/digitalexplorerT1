using System;
using System.Collections.Generic;
using System.Text;
using BizNest.Core.Domain.Entity.App;

namespace BizNest.Core.Domain.Form.App
{

    /// <summary>
    /// Business Form
    /// </summary>
    public class BusinessForm : BaseForm<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public RegistrationStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BusinessTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddressStreet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddressCity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AddressPostCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AddressCountryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact1Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact1Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact1Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact2Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact2Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Contact2Mobile { get; set; }

        public List<StakeHolderForm> StakeHolders { get; set; }
    }

}
