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
    public partial class MasterService : IMasterService
    {
        #region Branch Related
        public List<BranchInfo> GetList_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            return wscalls.GetList_Branch(BranchID, OrganizationCode, IsMainBranch, IsActive);
        }

        public BranchInfo GetDetails_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            return wscalls.GetDetails_Branch(BranchID, OrganizationCode, IsMainBranch, IsActive);
        }

        public bool Save_Branch(bool isOnlyDelete, BranchInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Branch(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region Bank Related
        public List<BankInfo> GetList_Bank(string BankID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetList_Bank(BankID, OrganizationCode, IsActive);
        }

        public BankInfo GetDetails_Bank(string BankID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetDetails_Bank(BankID, OrganizationCode, IsActive);
        }

        public bool Save_Bank(bool isOnlyDelete, BankInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Bank(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }




        public List<BankTransactionInfo> GetList_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetList_BankTransaction(BankTransactionID, OrganizationCode, IsActive);
        }

        public BankTransactionInfo GetDetails_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetDetails_BankTransaction(BankTransactionID, OrganizationCode, IsActive);
        }

        public bool Save_BankTransaction(bool isOnlyDelete, BankTransactionInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_BankTransaction(isOnlyDelete, objBankInfo, objUserInfo, out errormsg);
        }

        public bool Upload_BankTransaction(bool isOvereWrite, bool isOnlyDelete, DataSet ds, string BankName, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            return wscalls.Upload_BankTransaction(isOvereWrite, isOnlyDelete, ds, BankName, OrganizationCode, objUserInfo, out bReturn, out errormsg);
        }
        #endregion

        #region Category Related
        public List<CategoryInfo> GetList_CategoryForDropdown(string IsExpenseType, int CountryID, string CategoryID, string Option, string LanguageId)
        {
            return wscalls.GetList_CategoryForDropdown(IsExpenseType, CountryID, CategoryID, Option, LanguageId);
        }

        //Moderate User : Category list view
        public List<CategoryInfo> GetList_Category(string IsExpenseType, string CategoryID, string CountryId, string ProductId, bool IsActive, string Option, string LanguageId)
        {
            return wscalls.GetList_Category(IsExpenseType, CategoryID, CountryId, ProductId, IsActive, Option, LanguageId);
        }

        public CategoryInfo GetDetails_Category(string CategoryID, string CountryId, string ProductId, bool IsActive, string LanguageId)
        {
            return wscalls.GetDetails_Category(CategoryID, CountryId, ProductId, IsActive, LanguageId);
        }

        //Moderate User : Product details save
        public bool Save_Category(bool isOnlyDelete, CategoryInfo obj, string UserCode, out string errormsg)
        {
            return wscalls.Save_Category(isOnlyDelete, obj, UserCode, out errormsg);
        }

        public bool Upload_Category(bool isOvereWrite, DataSet ds, string UserCode, out bool bReturn, out string errormsg)
        {
            return wscalls.Upload_Category(isOvereWrite, ds, UserCode, out bReturn, out errormsg);
        }
        #endregion

        #region Coutry & State List
        public List<CountryInfo> GetList_Country()
        {
            return wscalls.GetList_Country();
        }

        public List<StateInfo> GetList_State(string CountryID)
        {
            return wscalls.GetList_State(CountryID);
        }
        #endregion

        #region Function Related
        public List<FunctionInfo> GetList_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetList_Function(FunctionID, OrganizationCode, IsActive);
        }

        public FunctionInfo GetDetails_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            return wscalls.GetDetails_Function(FunctionID, OrganizationCode, IsActive);
        }

        public bool Save_Function(bool isOnlyDelete, FunctionInfo objFunctionInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Function(isOnlyDelete, objFunctionInfo, objUserInfo, out errormsg);
        }
        #endregion

        #region Language Related
        public List<LanguageInfo> GetList_Language(string LanguageID, string LanguageName, bool IsActive)
        {
            return wscalls.GetList_Language(LanguageID, LanguageName, IsActive);
        }

        public LanguageInfo GetDetails_Language(string LanguageID, string LanguageName, bool IsActive)
        {
            return wscalls.GetDetails_Language(LanguageID, LanguageName, IsActive);
        }

        public bool Save_Language(bool isOnlyDelete, LanguageInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Language(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public List<LanguageValueInfo> GetList_DataValueLanguageWise(string MasterTablePrefixs, string LanguageIds, bool IsActive)
        {
            return wscalls.GetList_DataValueLanguageWise(MasterTablePrefixs, LanguageIds, IsActive);
        }

        public bool Save_DataValueLanguageWise(bool isOnlyDelete, LanguageValueInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_DataValueLanguageWise(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public List<LanguageCountryInfo> GetList_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
        {
            return wscalls.GetList_LanguageCountry(ID, LanguageID, CountryID, Visibility, IsActive);
        }

        public LanguageCountryInfo GetDetails_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
        {
            return wscalls.GetDetails_LanguageCountry(ID, LanguageID, CountryID, Visibility, IsActive);
        }

        public bool Save_LanguageCountry(bool isOnlyDelete, LanguageCountryInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_LanguageCountry(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region ServiceClass Related
        public List<ServiceClassInfo> GetList_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            return wscalls.GetList_ServiceClass(ServiceClassID, ServiceClassName, IsActive);
        }

        public ServiceClassInfo GetDetails_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            return wscalls.GetDetails_ServiceClass(ServiceClassID, ServiceClassName, IsActive);
        }

        public bool Save_ServiceClass(bool isOnlyDelete, ServiceClassInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_ServiceClass(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region ServiceType Related
        public List<ServiceTypeInfo> GetList_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            return wscalls.GetList_ServiceType(ServiceTypeID, ServiceTypeName, IsActive);
        }

        public ServiceTypeInfo GetDetails_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            return wscalls.GetDetails_ServiceType(ServiceTypeID, ServiceTypeName, IsActive);
        }

        public bool Save_ServiceType(bool isOnlyDelete, ServiceTypeInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_ServiceType(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region ServiceUnit Related
        public List<ServiceUnitInfo> GetList_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            return wscalls.GetList_ServiceUnit(ServiceUnitID, ServiceUnitName, IsActive);
        }

        public ServiceUnitInfo GetDetails_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            return wscalls.GetDetails_ServiceUnit(ServiceUnitID, ServiceUnitName, IsActive);
        }

        public bool Save_ServiceUnit(bool isOnlyDelete, ServiceUnitInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_ServiceUnit(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region Settings Related
        public List<SettingsInfo> GetList_Settings(string OrganizationCode, bool IsActive)
        {
            return wscalls.GetList_Settings(OrganizationCode, IsActive);
        }

        public SettingsInfo GetDetails_Settings(string OrganizationCode, bool IsActive)
        {
            return wscalls.GetDetails_Settings(OrganizationCode, IsActive);
        }

        public bool Save_Settings(bool isOnlyDelete, SettingsInfo objSettingsInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Settings(isOnlyDelete, objSettingsInfo, objUserInfo, out errormsg);
        }
        #endregion

        #region Static Value Related
        public List<StaticValuInfo> GetList_StaticValue(string Key) { return wscalls.GetList_StaticValue(Key); }
        #endregion

        #region Terms Related
        public List<TermsInfo> GetList_Terms(string TermsID, string OrganisationCode) { return wscalls.GetList_Terms(TermsID, OrganisationCode); }

        public TermsInfo GetDetails_Terms(string TermsID, string OrganisationCode) { return wscalls.GetDetails_Terms(TermsID, OrganisationCode); }

        public bool Save_Terms(bool isOnlyDelete, TermsInfo TermsInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Terms(isOnlyDelete, TermsInfo, objUserInfo, out errormsg);
        }
        #endregion
    }
}
