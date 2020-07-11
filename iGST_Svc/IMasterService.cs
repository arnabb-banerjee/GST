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
    public interface IMasterService
    {
        #region Branch Related
        [OperationContract]
        List<BranchInfo> GetList_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive);

        [OperationContract]
        BranchInfo GetDetails_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive);

        [OperationContract]
        bool Save_Branch(bool isOnlyDelete, BranchInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Bank Related
        [OperationContract]
        List<BankInfo> GetList_Bank(string BankID, string OrganizationCode, bool IsActive);

        [OperationContract]
        BankInfo GetDetails_Bank(string BankID, string OrganizationCode, bool IsActive);

        [OperationContract]
        bool Save_Bank(bool isOnlyDelete, BankInfo objBankInfo, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        List<BankTransactionInfo> GetList_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive);

        [OperationContract]
        BankTransactionInfo GetDetails_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive);

        [OperationContract]
        bool Save_BankTransaction(bool isOnlyDelete, BankTransactionInfo objBankInfo, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        bool Upload_BankTransaction(bool isOvereWrite, bool isOnlyDelete, DataSet ds, string BankName, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg);
        #endregion

        #region Category Related
        //Moderate User : Category list view
        [OperationContract]
        List<CategoryInfo> GetList_CategoryForDropdown(string IsExpenseType, int CountryID, string CategoryID, string Option, string LanguageId);

        [OperationContract]
        List<CategoryInfo> GetList_Category(string IsExpenseType, string CategoryID, string CountryId, string ProductId, bool IsActive, string Option, string LanguageId);

        //Moderate User : Product details view
        [OperationContract]
        CategoryInfo GetDetails_Category(string CategoryID, string CountryId, string ProductId, bool IsActive, string LanguageId);

        //Moderate User : Product details save
        [OperationContract]
        bool Save_Category(bool isOnlyDelete, CategoryInfo obj, string UserCode, out string errormsg);

        [OperationContract]
        bool Upload_Category(bool isOvereWrite, DataSet ds, string UserCode, out bool bReturn, out string errormsg);
        #endregion

        #region Country State
        [OperationContract]
        List<CountryInfo> GetList_Country();

        [OperationContract]
        List<StateInfo> GetList_State(string CountryID);
        #endregion

        #region Function Related
        [OperationContract]
        List<FunctionInfo> GetList_Function(string FunctionID, string OrganizationCode, bool IsActive);

        [OperationContract]
        FunctionInfo GetDetails_Function(string FunctionID, string OrganizationCode, bool IsActive);

        [OperationContract]
        bool Save_Function(bool isOnlyDelete, FunctionInfo objFunctionInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Language Related
        [OperationContract]
        List<LanguageInfo> GetList_Language(string LanguageID, string LanguageName, bool IsActive);

        [OperationContract]
        LanguageInfo GetDetails_Language(string LanguageID, string LanguageName, bool IsActive);

        [OperationContract]
        bool Save_Language(bool isOnlyDelete, LanguageInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        List<LanguageValueInfo> GetList_DataValueLanguageWise(string MasterTablePrefixs, string LanguageIds, bool IsActive);

        [OperationContract]
        bool Save_DataValueLanguageWise(bool isOnlyDelete, LanguageValueInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        List<LanguageCountryInfo> GetList_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive);

        [OperationContract]
        LanguageCountryInfo GetDetails_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive);

        [OperationContract]
        bool Save_LanguageCountry(bool isOnlyDelete, LanguageCountryInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region ServiceClass Related
        [OperationContract]
        List<ServiceClassInfo> GetList_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive);

        [OperationContract]
        ServiceClassInfo GetDetails_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive);

        [OperationContract]
        bool Save_ServiceClass(bool isOnlyDelete, ServiceClassInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region ServiceType Related
        [OperationContract]
        List<ServiceTypeInfo> GetList_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive);

        [OperationContract]
        ServiceTypeInfo GetDetails_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive);

        [OperationContract]
        bool Save_ServiceType(bool isOnlyDelete, ServiceTypeInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region ServiceUnit Related
        [OperationContract]
        List<ServiceUnitInfo> GetList_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive);

        [OperationContract]
        ServiceUnitInfo GetDetails_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive);

        [OperationContract]
        bool Save_ServiceUnit(bool isOnlyDelete, ServiceUnitInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Settings Related
        [OperationContract]
        List<SettingsInfo> GetList_Settings(string OrganizationCode, bool IsActive);

        [OperationContract]
        SettingsInfo GetDetails_Settings(string OrganizationCode, bool IsActive);

        [OperationContract]
        bool Save_Settings(bool isOnlyDelete, SettingsInfo objSettingsInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Static Value Related
        [OperationContract]
        List<StaticValuInfo> GetList_StaticValue(string Key);
        #endregion

        #region Terms Related
        [OperationContract]
        List<TermsInfo> GetList_Terms(string TermsID, string OrganisationCode);

        [OperationContract]
        TermsInfo GetDetails_Terms(string TermsID, string OrganisationCode);

        [OperationContract]
        bool Save_Terms(bool isOnlyDelete, TermsInfo TermsInfo, UserInfo objUserInfo, out string errormsg);
        #endregion
    }
}
