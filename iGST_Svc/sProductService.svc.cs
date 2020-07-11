using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessObjects;

namespace iGST_Svc
{
    [KnownType(typeof(UserInfo))]
    public partial class ProductService : IProductService
    {
        #region Product Related
        //Moderate User : Product list view
        public List<ProductInfo> GetList_ProductDropdownlist(string ProductID, string ProductName, bool IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            return wscalls.GetList_ProductDropdownlist(ProductID, ProductName, IsExpense, OrganizationCode, CategoryId, IsActive, LanguageId);
        }

        public List<ProductInfo> GetList_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            return wscalls.GetList_Product(ProductID, ProductName, IsExpense, OrganizationCode, CategoryId, IsActive, LanguageId);
        }

        //Moderate User : Product details view
        public ProductInfo GetDetails_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            return wscalls.GetDetails_Product(ProductID, ProductName, IsExpense, OrganizationCode, CategoryId, IsActive, LanguageId);
        }

        //Moderate User : Product list save
        public bool Save_Product(bool isOnlyDelete, ProductInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Product(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public bool Upload_Product(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            return wscalls.Upload_Product(isOvereWrite, ds, objUserInfo, out bReturn, out errormsg);
        }

        // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
        public List<ProductOrganiztionInfo> GetList_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            return wscalls.GetList_ProductOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        // Modified on [30th August 2019] by [Partha]

        public List<ProductOrganiztionInfo> GetDetails_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            return wscalls.GetDetails_ProductOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }

        public bool Save_ProductOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_ProductOrganization(isOnlyDelete, obj, objUserInfo, out errormsg);
        }


        public bool Save_ProductOrganizationImage(bool isOnlyDelete, ProductOrganiztionImageInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_ProductOrganizationImage(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public List<ProductOrganiztionImageInfo> GetList_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            return wscalls.GetList_ProductOrganizationImage(ImageId, OrganizationproductId, ProductID, IsActive, IsMain);
        }

        public ProductOrganiztionImageInfo GetDetails_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            return wscalls.GetDetails_ProductOrganizationImage(ImageId, OrganizationproductId, ProductID, IsActive, IsMain);
        }
        #endregion
    }
}
