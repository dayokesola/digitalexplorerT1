using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Domain.Model.App
{ 

    public class BusinessCreateModel
    {
        public string BusinessName { get; set; }
        public string AddressStreet { get; set; }
        public List<StakeholderModel> StakeHolders { get; set; }
        public string BusinessType { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCountry { get; set; }
        public string Contact1Name { get; set; }
        public string Contact1Email { get; set; }
        public string Contact1Mobile { get; set; }
        public string Contact2Name { get; set; }
        public string Contact2Email { get; set; }
        public string Contact2Mobile { get; set; }
    }

    public class StakeholderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostCode { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string SeedAmount { get; set; }
        public string Mobile { get; set; }
        public string AddressCountry { get; set; }
    }

}
