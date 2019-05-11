using System;

namespace BizNest.Core.Domain.Model.App
{
    public class StakeHolderModel : BaseModel<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public long BusinessId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SSN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BirthDate { get; set; }
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
        public decimal SeedCapital { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountNumber { get; set; }
    }
}