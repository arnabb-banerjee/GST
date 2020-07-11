using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class StaticValuInfo
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

    [Serializable]
    public class TermsInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationCode { get; set; }
        public string DueInFixedNumberDays { get; set; }
        public string DueInCertainDayOfMonth { get; set; }
        public string DueInNextMonth { get; set; }
        public string Discount { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

    [Serializable]
    public class CountryInfo
    {
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public string PhoneCode { get; set; }
    }

    [Serializable]
    public class StateInfo
    {
        public string StateID { get; set; }
        public string StateName { get; set; }
        public string Countryid { get; set; }
    }

    [Serializable]
    public class GSTInfo
    {
        public string ShipStateID { get; set; }
        public string ProductId { get; set; }
        public string CountryId { get; set; }
        public string Percentage { get; set; }
        public string SalePrice { get; set; }
    }

    [Serializable]
    public class ViewBagImageUploadParameterPass
    {
        public string PageType { get; set; }
        public string ProductId { get; set; }
        public string OrganizationProductId { get; set; }
        public string ImageId { get; set; }
    }
}
