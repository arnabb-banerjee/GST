using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class CustomerInfo
    {
        public CustomerInfo()
        {
            this.CusID = "0";
            this.OrganizationCode = "";
            this.DisplayName = "";
            this.First_Name = "";
            this.Last_Name = "";
            this.ActivityType = "";
            this.AsOfDate = "";
            //this.BillingAddress = "";
            this.BillingWith = "";
            this.Billing_AddressID = "";
            this.Billing_City = "";
            this.Safix = "";
            this.State = "";
            this.Country = "";
            this.CSTRegNo = "";
            this.CusID = "";
            this.DatauniqueID = "";
            this.DOB = "";
            this.EmailID = "";
            this.GSTIN = "";
            this.GSTRegistrationType = "";
            this.GSTRegistrationTypeName = "";
            this.IsActive = false;
            this.IsSubCustomer= false;
            this.LastModifiedBy = "";
            this.Middle_Name = "";
            this.Mobile = "";
            this.Notes = "";
            this.OpeningBalance = "";
            this.OrganizationCode = "";
            this.PANNo = "";
            this.ParentCusID = "";
            this.PrefferedDeliveryMethod = "";
            this.PrefferedPaymentMethod = "";
            this.Sex = "";
            this.Shipping_AddressID = "";
            this.Shipping_City = "";
            this.Shipping_Country = "";
            this.Shipping_State = "";
            this.Shipping_Street1 = "";
            this.Shipping_Street2 = "";
            this.State = "";
            this.Street1 = "";
            this.Street2 = "";
            this.TaxRegNo = "";
            this.Terms = "";
            this.TermsName = "";
            this.Title = "";
            this.Website = "";
        }

        public string UserType { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string CusID { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Company_Name { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Safix { get; set; }
        public string ParentCusID { get; set; }
        public string ParentCusName { get; set; }
        public string BillingWith { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }

        //Contact related
        public string EmailID { get; set; }
        public string Mobile { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Website { get; set; }

        //Tax Related
        public string GSTRegistrationType { get; set; }
        public string GSTRegistrationTypeName { get; set; }
        public string GSTIN { get; set; }
        public bool IsSubCustomer { get; set; }
        public string TaxRegNo { get; set; }
        public string CSTRegNo { get; set; }
        public string PANNo { get; set; }
        public string Terms { get; set; }
        public string TermsName { get; set; }

        //Prefered Methods
        public string PrefferedPaymentMethod { get; set; }
        public string PrefferedPaymentMethodName { get; set; }
        public string PrefferedDeliveryMethod { get; set; }
        public string PrefferedDeliveryMethodName { get; set; }


        //Shipping Address
        public string Shipping_AddressID { get; set; }
        public string Shipping_Street1 { get; set; }
        public string Shipping_Street2 { get; set; }
        public string Shipping_City { get; set; }
        public string Shipping_State { get; set; }
        public string Shipping_Country { get; set; }
        public string Shipping_CityName { get; set; }
        public string Shipping_StateName { get; set; }
        public string Shipping_CountryName { get; set; }

        //Billing Address
        public string Billing_AddressID { get; set; }
        public string Billing_Street1 { get; set; }
        public string Billing_Street2 { get; set; }
        public string Billing_City { get; set; }
        public string Billing_State { get; set; }
        public string Billing_Country { get; set; }
        public string Billing_CityName { get; set; }
        public string Billing_StateName { get; set; }
        public string Billing_CountryName { get; set; }

        //Openning balance
        public string OpeningBalance { get; set; }
        public string AsOfDate { get; set; }

        //Notes
        public string Notes { get; set; }

        public List<TermsInfo> ListTermsAll { get; set; }
        public List<StaticValuInfo> ListStaticValuesAll { get; set; }
        public List<CustomerBillingAddressInfo> BillingAddress { get; set; }
        public List<CustomerShippingAddressInfo> ShippingAddress { get; set; }
        public List<BranchInfo> BranchList { get; set; }

    }

    [Serializable]
    public class CustomerBillingAddressInfo
    {
        public string AddressID { get; set; }
        public string CusID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }

        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
    }

    [Serializable]
    public class CustomerShippingAddressInfo
    {
        public string AddressID { get; set; }
        public string CusID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }

        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
    }

    [Serializable]
    public class Customer_OpeningBalanceInfo
    {
        public string CusID { get; set; }
        public string OpeningBalance { get; set; }
        public System.DateTime AsOfDate { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
    }

    [Serializable]
    public class CustomerImageInfo
    {
        public int ImageId { get; set; }
        public int CustomerId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Seq { get; set; }
        public string Ismain { get; set; }
        public string IsActive { get; set; }
    }
}
