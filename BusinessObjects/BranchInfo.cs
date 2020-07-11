using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class BranchInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string IsMainBranch { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string PIN { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
        //public List<BranchProductMappingInfo> ProductList { get; set; }
        public List<UserBranchRoleMappingInfo> UserRoleList { get; set; }
    }


    //[Serializable]
    //public class BranchProductMappingInfo
    //{
    //    public string ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public string CategoryId { get; set; }
    //    public string ParentCategoryId { get; set; }
    //    public string CategoryName { get; set; }
    //    public string ParentCategoryName { get; set; }
    //    public string ServiceOrGoods { get; set; }
    //    public string HSNCode { get; set; }
    //    public string SACCode { get; set; }
    //    public string CGST { get; set; }
    //    public string SGST { get; set; }
    //    public string IGST { get; set; }
    //    public string OrganizationID { get; set; }
    //    public string BranchID { get; set; }
    //    public string OrganizationName { get; set; }
    //    public string BranchName { get; set; }
    //    public string DataUniqueID { get; set; }
    //    public string AllreadyExists { get; set; }
    //}

}
