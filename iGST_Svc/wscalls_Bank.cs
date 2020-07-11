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
        #region Bank Related
        public static List<BankInfo> GetList_Bank(string BankID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spMSTBanksGet"))
            {
                DBHelper.AddPparameter("@ID", BankID);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BankInfo> list = new List<BankInfo>();
                        BankInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new BankInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.BankID = dr["ID"].ToString();
                            obj.Name = dr["Name"].ToString();
                            obj.Address = dr["Address"].ToString();
                            obj.IFSCCode = dr["IFSCCode"].ToString();
                            obj.MCRCode = dr["MCRCode"].ToString();
                            obj.CorpID = dr["CorpID"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static BankInfo GetDetails_Bank(string BankID, string OrganizationCode, bool IsActive)
        {
            if (!string.IsNullOrEmpty(BankID))
            {
                List<BankInfo> list = GetList_Bank(BankID, OrganizationCode, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new BankInfo();
        }

        public static bool Save_Bank(bool isOnlyDelete, BankInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.BankID, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CorpID.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Corporate ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Address.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.IFSCCode.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "IFSC Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.MCRCode.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "MCR Code", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTBanksSave]", true))
            {
                DBHelper.AddPparameter("@BankID", !string.IsNullOrEmpty(objBankInfo.BankID) ? Convert.ToInt32(objBankInfo.BankID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@Name", objBankInfo.Name, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", objBankInfo.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CorpID", objBankInfo.CorpID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Address", objBankInfo.Address, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IFSCCode", objBankInfo.IFSCCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@MCRCode", objBankInfo.MCRCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", objBankInfo.IsActive, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<BankTransactionInfo> GetList_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spBankTransactionsGet]"))
            {
                DBHelper.AddPparameter("@ID", BankTransactionID);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BankTransactionInfo> list = new List<BankTransactionInfo>();
                        BankTransactionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new BankTransactionInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();

                            obj.TransactionID = dr["TransactionID"].ToString().Trim();
                            obj.TransactionDate = Convert.ToDateTime(dr["TransactionDate"].ToString()).ToString("dd MMM yy");

                            obj.Id = dr["Id"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.Debit = dr["Debit"].ToString();
                            obj.Credit = dr["Credit"].ToString();
                            obj.ChqNo = dr["ChqNo"].ToString();
                            obj.Balance = dr["Balance"].ToString();
                            obj.Particulars = dr["Particulars"].ToString();
                            obj.IsSellExpense = dr["IsSellExpense"].ToString();
                            obj.InitBr = dr["InitBr"].ToString();
                            obj.CustomerId = dr["CustomerId"].ToString();
                            obj.CustomerName = dr["CustomerName"].ToString();
                            obj.ProductIds = dr["ProductIds"].ToString();
                            obj.Products = dr["Products"].ToString();
                            obj.Tax = dr["Tax"].ToString();

                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static BankTransactionInfo GetDetails_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
        {
            if (!string.IsNullOrEmpty(BankTransactionID))
            {
                List<BankTransactionInfo> list = GetList_BankTransaction(BankTransactionID, OrganizationCode, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new BankTransactionInfo();
        }

        public static bool Save_BankTransaction(bool isOnlyDelete, BankTransactionInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.Products, Validations.ValueType.AlphaNumericSpecialChar, true, "Products", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CustomerId, Validations.ValueType.Integer, true, "Customer/Supplier", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Tax, Validations.ValueType.Numeric, true, "Tax", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spBankTransactionsSave]", true))
            {
                DBHelper.AddPparameter("@BankTransactionId", objBankInfo.Id.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.Id) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@Products", objBankInfo.Products, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CustomerId", objBankInfo.CustomerId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsSellExpense", objBankInfo.IsSellExpense, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Tax", objBankInfo.Tax, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static bool Upload_BankTransaction(bool isOvereWrite, bool isOnlyDelete, DataSet ds, string BankName, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";
            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { errormsg = "Organization name not found"; return false; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                int TrasactionID = -1;
                int TrasactionDate = -1;
                int Debit = -1;
                int Credit = -1;
                int Balance = -1;
                int Particular = -1;
                int IntBr = -1;
                int ChqNo = -1;

                #region Column Validation
                int index = ds.Tables[0].Columns.IndexOf("Transaction ID");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TransactionID"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Tran ID"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TranID"); }
                if (index < 0) { errormsg = "Column [Transaction ID] is not available"; return true; }
                TrasactionID = index;

                index = ds.Tables[0].Columns.IndexOf("Transaction Date");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TransactionDate"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Tran Date"); }
                if (index < 0) { errormsg = "Column [Transaction Date] is not available"; return true; }
                TrasactionDate = index;

                index = ds.Tables[0].Columns.IndexOf("Particulars");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Particular"); }
                if (index < 0) { errormsg = "Column [Particulars] is not available"; return true; }
                Particular = index;

                index = ds.Tables[0].Columns.IndexOf("Balance");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("balance"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("blnce"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("bl"); }
                if (index < 0) { errormsg = "Column [Balance] is not available"; return true; }
                Balance = index;

                index = ds.Tables[0].Columns.IndexOf("InitBr");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init Br"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init# Br"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init. Br"); }
                if (index < 0) { errormsg = "Column [InitBr] is not available"; return true; }
                IntBr = index;

                index = ds.Tables[0].Columns.IndexOf("Debit");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Db"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("debit"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("db"); }
                if (index < 0) { errormsg = "Column [Debit] is not available"; return true; }
                Debit = index;

                index = ds.Tables[0].Columns.IndexOf("Credit");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("credit"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Crt"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Cr"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("cr"); }
                if (index < 0) { errormsg = "Column [Credit] is not available"; return true; }
                Credit = index;

                index = ds.Tables[0].Columns.IndexOf("Chq No");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("ChqNo"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Cheque no"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Chq number"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Cheque number"); }
                if (index < 0) { errormsg = "Column [Chq No] is not available"; return true; }
                ChqNo = index;
                #endregion

                using (DBHelper dbhlper = new DBHelper("[spBankTransactionsSave]", true))
                {
                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr[TrasactionID].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Transaction ID", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[ChqNo].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Chq No", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(Convert.ToDateTime(dr[TrasactionDate].ToString()).ToString(), Validations.ValueType.DateTime, false, "Transaction date", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Particular].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Particulars", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Balance].ToString(), Validations.ValueType.Numeric, false, "Balance", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[IntBr].ToString(), Validations.ValueType.Numeric, false, "Init Br", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Debit].ToString(), Validations.ValueType.Numeric, true, "Debit", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Credit].ToString(), Validations.ValueType.Numeric, true, "Credit", out errormsg)) { return false; }
                        #endregion

                        DBHelper.ClearParameters();

                        #region Parameter Add
                        DBHelper.AddPparameter("@isOvereWrite", isOvereWrite ? "Y" : "N", DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@TransactionID", dr[TrasactionID].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@TransactionDate", dr[TrasactionDate].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ChqNo", dr[ChqNo].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@Particulars", dr[Particular].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@Debit", dr[Debit].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@Credit", dr[Credit].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@Balance", dr[Balance].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@InitBr", dr[IntBr].ToString(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@BankName", BankName, DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                        DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
                        #endregion

                        if (DBHelper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg) == false)
                        {
                            return false;
                        }
                    }

                    return DBHelper.CloseConnection(true);
                }
            }

            if (errormsg.Trim().Length == 0)
                errormsg = "No data is available to be uploaded";

            return false;
        }

        public static bool Upload_BankTransaction1(bool isOvereWrite, DataSet ds, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";
            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Validation - Column existance
                if (ds.Tables[0].Columns.IndexOf("Transaction Date") < 0 && ds.Tables[0].Columns.IndexOf("TransactionDate") < 0 && ds.Tables[0].Columns.IndexOf("Tran Date") < 0 && ds.Tables[0].Columns.IndexOf("TranDate") < 0) { errormsg = "[TransactionDate] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Transaction ID") < 0 && ds.Tables[0].Columns.IndexOf("TransactionID") < 0) { errormsg = "[Transaction ID] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("ChqNo") < 0 && ds.Tables[0].Columns.IndexOf("Chq No") < 0) { errormsg = "[ChqNo] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Particulars") < 0 && ds.Tables[0].Columns.IndexOf("Particular") < 0) { errormsg = "[Particulars] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Debit") < 0) { errormsg = "[Debit] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Credit") < 0) { errormsg = "[Credit] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Balance") < 0) { errormsg = "[Balance] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("InitBr") < 0 && ds.Tables[0].Columns.IndexOf("Init Br") < 0 && ds.Tables[0].Columns.IndexOf("Init# Br") < 0 && ds.Tables[0].Columns.IndexOf("Init. Br") < 0) { errormsg = "[InitBr] column is not available"; }
                else
                {
                    ds.Tables[0].Columns.Add("Row Number");
                    ds.Tables[0].Columns.Add("Error Message");

                    bool DisplayErroeMessage = false;

                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        int index = ds.Tables[0].Columns.IndexOf("Transaction ID");
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TransactionID"); }
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Tran ID"); }
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TranID"); }

                        if (!Validations.ValidateDataType(dr[index].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Transaction ID", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }

                        index = ds.Tables[0].Columns.IndexOf("Transaction Date");
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("TransactionDate"); }
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Tran Date"); }

                        if (!Validations.ValidateDataType(Convert.ToDateTime(dr[index].ToString()).ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Transaction date", out errormsg)) { return false; }

                        index = ds.Tables[0].Columns.IndexOf("Particulars");
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Particular"); }

                        if (!Validations.ValidateDataType(dr[index].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Particulars", out errormsg)) { return false; }

                        if (!Validations.ValidateDataType(dr["Balance"].ToString(), Validations.ValueType.Numeric, false, "Balance", out errormsg)) { return false; }

                        index = ds.Tables[0].Columns.IndexOf("InitBr");
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init Br"); }
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init# Br"); }
                        if (index < 0) { index = ds.Tables[0].Columns.IndexOf("Init. Br"); }

                        if (!Validations.ValidateDataType(dr[index].ToString(), Validations.ValueType.Numeric, false, "Init Br", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Debit"].ToString(), Validations.ValueType.Numeric, true, "Debit", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Credit"].ToString(), Validations.ValueType.Numeric, true, "Credit", out errormsg)) { return false; }
                        #endregion
                    }
                    #endregion

                    if (!DisplayErroeMessage)
                    {
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Error Message"));
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Row Number"));

                        using (DBHelper dbhlper = new DBHelper("[spBankTransactionsUpload]", true))
                        {
                            DBHelper.AddPparameter("@typeBankTransactions", ds.Tables[0], DBHelper.param_types.Structured);
                            DBHelper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            if (DBHelper.Execute_NonQuery(out errormsg))
                            {
                                return true;
                            }
                            else
                            {
                                if (errormsg.Trim().Length == 0)
                                    errormsg = "Data hos not been saved and no reason has been determined";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        errormsg = "Problem in excel data. Please rectify before upload";
                        return false;
                    }
                }
            }
            else
            {
                errormsg = "No data is available to be uploaded";
            }

            return false;
        }
        #endregion
   }
}