using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Common;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Data.OleDb;
using BusinessObjects;
using Databaselayer;

namespace iGST_Svc
{
    public sealed partial class wscalls
    {
        #region Settings Related
        public static List<SettingsInfo> GetList_Settings(string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spSettingsGet]"))
            {
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<SettingsInfo> list = new List<SettingsInfo>();
                        SettingsInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new SettingsInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.an_AlertText_GSTDate = dr["an_AlertText_GSTDate"].ToString();
                            obj.an_AlertText_PaidMembership = dr["an_AlertText_PaidMembership"].ToString();
                            obj.an_isAllowedAlert_GSTDate = dr["an_isAllowedAlert_GSTDate"].ToString().Trim().ToUpper() == "Y";
                            obj.an_isAllowedAlert_PaidMembership = dr["an_isAllowedAlert_PaidMembership"].ToString().Trim().ToUpper() == "Y";
                            obj.c_isAllowedMultyLanguage = dr["c_isAllowedMultyLanguage"].ToString().Trim().ToUpper() == "Y";
                            obj.mc_CurrencyList = dr["mc_CurrencyList"].ToString();
                            obj.mc_isAllowedMutyCurrency = dr["mc_isAllowedMutyCurrency"].ToString().Trim().ToUpper() == "Y";
                            obj.p_BankAccountHolder = dr["p_BankAccountHolder"].ToString();
                            obj.p_BankAccountIBankName = dr["p_BankAccountIBankName"].ToString();
                            obj.p_BankAccountIBranchName = dr["p_BankAccountIBranchName"].ToString();
                            obj.p_BankAccountIFSCCode = dr["p_BankAccountIFSCCode"].ToString();
                            obj.p_BankAccountIMCRCode = dr["p_BankAccountIMCRCode"].ToString();
                            obj.p_BankAccountNumber = dr["p_BankAccountNumber"].ToString();
                            obj.p_isAllowedOnlinePayment = dr["p_isAllowedOnlinePayment"].ToString().Trim().ToUpper() == "Y";
                            obj.p_PaypalAccountID = dr["p_PaypalAccountID"].ToString();

                            obj.c_CompanyName = dr["CompanyName"].ToString();
                            obj.c_Email = dr["Email"].ToString();
                            obj.c_Mobile = dr["Mobile"].ToString();
                            obj.c_Address = dr["Address"].ToString();
                            obj.c_City = dr["City"].ToString();
                            obj.c_State = dr["State"].ToString();
                            obj.c_Country = dr["Country"].ToString();
                            obj.c_Website = dr["Website"].ToString();
                            obj.c_CIN = dr["CIN"].ToString();
                            obj.c_PAN = dr["PAN"].ToString();
                            obj.c_DefaultEmail = dr["DefaultEmail"].ToString();
                            obj.c_SMTP = dr["SMTP"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static SettingsInfo GetDetails_Settings(string OrganizationCode, bool IsActive)
        {
            if (!string.IsNullOrEmpty(OrganizationCode))
            {
                List<SettingsInfo> list = GetList_Settings(OrganizationCode, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new SettingsInfo();
        }

        public static bool Save_Settings(bool isOnlyDelete, SettingsInfo objSettingsInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objSettingsInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountHolder, Validations.ValueType.AlphaNumericSpecialChar, true, "Account Holder", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountIBankName, Validations.ValueType.AlphaNumericSpecialChar, true, "bank Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountIBranchName, Validations.ValueType.AlphaNumericSpecialChar, true, "Branch Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountIFSCCode, Validations.ValueType.AlphaNumericSpecialChar, true, "IFSC Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountIMCRCode, Validations.ValueType.AlphaNumericSpecialChar, true, "MCR Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_BankAccountNumber, Validations.ValueType.AlphaNumericSpecialChar, true, "Account Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.p_PaypalAccountID, Validations.ValueType.AlphaNumericSpecialChar, true, "Account ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.mc_CurrencyList, Validations.ValueType.AlphaNumericSpecialChar, true, "Currency List", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSettingsInfo.c_CompanyName, Validations.ValueType.AlphaNumericSpecialChar, true, "Company name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_Email, Validations.ValueType.AlphaNumericSpecialChar, true, "Email", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_DefaultEmail, Validations.ValueType.AlphaNumericSpecialChar, true, "Default email", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_Mobile, Validations.ValueType.AlphaNumericSpecialChar, true, "Mobile", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_Address, Validations.ValueType.AlphaNumericSpecialChar, true, "Address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_State, Validations.ValueType.Numeric, true, "State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_Country, Validations.ValueType.Numeric, true, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_CIN, Validations.ValueType.AlphaNumericSpecialChar, true, "CIN", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_PAN, Validations.ValueType.AlphaNumericSpecialChar, true, "PAN", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSettingsInfo.c_Website, Validations.ValueType.AlphaNumericSpecialChar, true, "Website", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spSettingsSave]", true))
            {
                DBHelper.AddPparameter("@OrganizationCode", objSettingsInfo.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@infotype", objSettingsInfo.InfoType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@mc_isAllowedMutyCurrency", objSettingsInfo.mc_isAllowedMutyCurrency ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@mc_CurrencyList", objSettingsInfo.mc_CurrencyList, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_isAllowedOnlinePayment", objSettingsInfo.p_isAllowedOnlinePayment ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountNumber", objSettingsInfo.p_BankAccountNumber, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountHolder", objSettingsInfo.p_BankAccountHolder, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountIFSCCode", objSettingsInfo.p_BankAccountIFSCCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountIMCRCode", objSettingsInfo.p_BankAccountIMCRCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountIBranchName", objSettingsInfo.p_BankAccountIBranchName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_BankAccountIBankName", objSettingsInfo.p_BankAccountIBankName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@p_PaypalAccountID", objSettingsInfo.p_PaypalAccountID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@c_isAllowedMultyLanguage", objSettingsInfo.c_isAllowedMultyLanguage ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@an_isAllowedAlert_GSTDate", objSettingsInfo.an_isAllowedAlert_GSTDate ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@an_isAllowedAlert_PaidMembership", objSettingsInfo.an_isAllowedAlert_PaidMembership, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@an_AlertText_GSTDate", objSettingsInfo.an_AlertText_GSTDate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@an_AlertText_PaidMembership", objSettingsInfo.an_AlertText_PaidMembership, DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@CompanyName", objSettingsInfo.c_CompanyName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Email", objSettingsInfo.c_Email, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Mobile", objSettingsInfo.c_Mobile, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Address", objSettingsInfo.c_Address, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", objSettingsInfo.c_City, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", objSettingsInfo.c_State, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Country", objSettingsInfo.c_Country, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Website", objSettingsInfo.c_Website, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CIN", objSettingsInfo.c_CIN, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PAN", objSettingsInfo.c_PAN, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DefaultEmail", objSettingsInfo.c_DefaultEmail, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@SMTP", objSettingsInfo.an_AlertText_PaidMembership, DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion
    }
}