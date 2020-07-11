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
    public partial class GSTService : IGSTService
    {
        
        #region Role Related
        public List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            return wscalls.GetList_Role(RoleID, BranchId, UserID, IsActive);
        }

        public RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            return wscalls.GetDetails_Role(RoleID, BranchId, UserID, IsActive);
        }

        public bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Role(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID)
        {
            return wscalls.Get_Effective_Role_ForAUser(BranchId, UserID);
        }
        #endregion
        
        #region Tax Related
        #region Tax master
        public List<TaxMasterInfo> GetList_TaxMaster(int mode, string TaxDefinationID)
        {
            return wscalls.GetList_TaxMaster(mode, TaxDefinationID);
        }

        public TaxMasterInfo GetDetails_TaxMaster(int mode, string TaxDefinationID)
        {
            return wscalls.GetDetails_TaxMaster(mode, TaxDefinationID);
        }

        public bool Save_TaxMaster(bool isOnlyDelete, TaxMasterInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_TaxMaster(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }
        #endregion

        #region Tax Country mapping
        public List<TaxCountryMapInfo> GetList_TaxCountryMap(int mode, string TaxDefinationID, string CountryId)
        {
            return wscalls.GetList_TaxCountryMap(mode, TaxDefinationID, CountryId);
        }

        public TaxCountryMapInfo GetDetails_TaxCountryMap(int mode, string TaxDefinationID, string CountryId)
        {
            return wscalls.GetDetails_TaxCountryMap(mode, TaxDefinationID, CountryId);
        }

        public bool Save_TaxCountryMap(bool isOnlyDelete, TaxCountryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_TaxCountryMap(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }
        #endregion

        #region Tax Country Categrory mapping
        public List<TaxCountryCategoryMapInfo> GetList_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            return wscalls.GetList_TaxCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);
        }

        public TaxCountryCategoryMapInfo GetDetails_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            return wscalls.GetDetails_TaxCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);
        }

        public bool Save_TaxCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_TaxCountryCategoryMap(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }
        #endregion

        // Added by Partha on 10/07/2019
        #region Tax for expense Country Categrory mapping
        public List<TaxCountryCategoryMapInfo> GetList_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            return wscalls.GetList_TaxExpenseCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);
        }

        public TaxCountryCategoryMapInfo GetDetails_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            return wscalls.GetDetails_TaxExpenseCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);
        }

        public bool Save_TaxExpenseCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_TaxExpenseCountryCategoryMap(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }
        #endregion


        public GSTInfo Get_Gst(string ProductId, string ShipStateId, string OrganizationCode)
        {
            return wscalls.Get_Gst(ProductId, ShipStateId, OrganizationCode);
        }

        public GSTInfo Get_GstCategory(string CategorytId, string ShipStateId, string OrganizationCode)
        {
            return wscalls.Get_GstCategory(CategorytId, ShipStateId, OrganizationCode);
        }

        // End: 10/07/2019
        #endregion
    }
}
