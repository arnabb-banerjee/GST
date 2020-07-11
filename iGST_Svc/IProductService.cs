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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        #region Product Related
        //Moderate User : Product list view
        [OperationContract]
        List<ProductInfo> GetList_ProductDropdownlist(string ProductID, string ProductName, bool IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId);

        [OperationContract]
        List<ProductInfo> GetList_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId);

        //Moderate User : Product details view
        [OperationContract]
        ProductInfo GetDetails_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId);

        //Moderate User : Product list save
        [OperationContract]
        bool Save_Product(bool isOnlyDelete, ProductInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        bool Upload_Product(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg);

        // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
        [OperationContract]
        List<ProductOrganiztionInfo> GetList_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        // END: Modified on [30th August 2019] by [Partha]

        [OperationContract]
        List<ProductOrganiztionInfo> GetDetails_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);

        [OperationContract]
        bool Save_ProductOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg);

        
        [OperationContract]
        bool Save_ProductOrganizationImage(bool isOnlyDelete, ProductOrganiztionImageInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        List<ProductOrganiztionImageInfo> GetList_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain);

        [OperationContract]
        ProductOrganiztionImageInfo GetDetails_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain);
        #endregion
    }
}
