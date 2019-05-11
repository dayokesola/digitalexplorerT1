namespace BizNest.Core.Domain.Entity.App
{
    /// <summary>
    /// Business Class
    /// </summary>
    public class Business : BaseEntity<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


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

        public RegistrationStatus Status { get; set; }

    }



    public enum RegistrationStatus
    {
        Start,AwaitingPayment,Submitted,Review,Approved,Rejected
    }

}
