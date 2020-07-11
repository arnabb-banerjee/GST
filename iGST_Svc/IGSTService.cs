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
    public interface IGSTService
    {
        #region Role Related
        [OperationContract]
        List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive);

        [OperationContract]
        RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive);

        [OperationContract]
        bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID);
        #endregion

        #region Tax Related
        #region Tax master
        [OperationContract]
        List<TaxMasterInfo> GetList_TaxMaster(int mode, string TaxDefinationID);

        [OperationContract]
        TaxMasterInfo GetDetails_TaxMaster(int mode, string TaxDefinationID);

        [OperationContract]
        bool Save_TaxMaster(bool isOnlyDelete, TaxMasterInfo objBankInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Tax Country mapping
        [OperationContract]
        List<TaxCountryMapInfo> GetList_TaxCountryMap(int mode, string TaxDefinationID, string CountryId);

        [OperationContract]
        TaxCountryMapInfo GetDetails_TaxCountryMap(int mode, string TaxDefinationID, string CountryId);

        [OperationContract]
        bool Save_TaxCountryMap(bool isOnlyDelete, TaxCountryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Tax Country Categrory mapping
        [OperationContract]
        List<TaxCountryCategoryMapInfo> GetList_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId);

        [OperationContract]
        TaxCountryCategoryMapInfo GetDetails_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId);

        [OperationContract]
        bool Save_TaxCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        // Added by Partha on 10/07/2019
        #region Tax for expense Country Categrory mapping
        [OperationContract]
        List<TaxCountryCategoryMapInfo> GetList_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId);

        [OperationContract]
        TaxCountryCategoryMapInfo GetDetails_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId);

        [OperationContract]
        bool Save_TaxExpenseCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        // End: 10/07/2019

        [OperationContract]
        GSTInfo Get_Gst(string ProductId, string ShipStateId, string OrganizationCode);

        [OperationContract]
        GSTInfo Get_GstCategory(string CategorytId, string ShipStateId, string OrganizationCode);

        #endregion
    }
}
