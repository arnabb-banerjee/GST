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

namespace iGST_Svc1
{
    public sealed class wscalls1
    {
        string _ConnectionString = "";
        string _ConnectionStringOleDB = "";
        string NewDatauniqueID = "";
        bool flag = false;

        public wscalls1()
        {
            _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ToString();
            _ConnectionStringOleDB = System.Configuration.ConfigurationManager.ConnectionStrings["strConnOleDB"].ToString();
        }

        #region Bill Related
        public List<InvoiceInfo> GetList_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            using (DBHelper dbhlper = new DBHelper("[GetInvoiceListMasters]"))
            {
                dbhlper.AddPparameter("@InvoiceID", InvoiceID);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@BranchID", BranchID);
                dbhlper.AddPparameter("@CusID", CusID);
                dbhlper.AddPparameter("@InvoiceDateFrom", InvoiceDateFrom);
                dbhlper.AddPparameter("@InvoiceDateTo", InvoiceDateTo);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<InvoiceInfo> list = new List<InvoiceInfo>();
                        InvoiceInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new InvoiceInfo();

                            obj.InvoiceID = dr["InvoiceID"].ToString();
                            obj.InvoiceNumber = dr["InvoiceNumber"].ToString();
                            obj.AmountPayable = dr["AmountPayable"].ToString();
                            obj.InvoiceDate = dr["InvoiceDate"] != null ? Convert.ToDateTime(dr["InvoiceDate"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                            obj.InvoiceDueDate = dr["Duedate"] != null ? Convert.ToDateTime(dr["Duedate"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                            obj.BranchID = dr["BranchID"].ToString();
                            obj.CusID = dr["CusID"].ToString();
                            obj.CustomerName = dr["CustomerName"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.BranchName = dr["BranchName"].ToString();
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();

                            if (dr["LastModifiedOn"] != null && dr["LastModifiedOn"].ToString().Trim().Length > 0)
                            {
                                obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            }

                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsCancelled = dr["IsCancelled"].ToString().Trim() == "Y";
                            obj.IsReturned = dr["IsReturned"].ToString().Trim() == "Y";
                            obj.AmountAfterDiscount = dr["AmountAfterDiscount"].ToString();
                            obj.SumAmount = dr["SumAmount"].ToString();
                            obj.AmountPayable = dr["AmountPayable"].ToString();
                            obj.TotalAmount = dr["TotalAmount"].ToString();
                            obj.AmountDue = dr["AmountDue"].ToString();
                            obj.ReturnedOn = dr["ReturnedOn"].ToString();
                            obj.BranchAddress = dr["BranchAddress"].ToString();
                            obj.BranchCountry = dr["BranchCountry"].ToString();
                            obj.BranchState = dr["BranchState"].ToString();
                            obj.ShipAddress = dr["ShipAddess"].ToString();
                            obj.ShipCity = dr["ShipCity"].ToString();
                            obj.ShipState = dr["ShipState"].ToString();
                            obj.ShipCountry = dr["ShipCountry"].ToString();
                            obj.CusEmail = dr["CusEmail"].ToString();
                            obj.CustomerName = dr["CustomerName"].ToString();
                            obj.ChangedCurrency = dr["ChangedCurrency"].ToString();
                            obj.ConversionRate = dr["ConversionRate"].ToString();
                            obj.PrevConversionRate = dr["PrevConversionRate"].ToString();
                            obj.DatauniqueID = dr["DataUniqueID"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim() == "Y";

                            if (InvoiceID.Trim().ToString().Length > 0 &&
                                (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0))
                            {
                                obj.InvoiceProductList = new List<InvoiceProductInfo>();
                                InvoiceProductInfo objInvoiceProductInfo = null;

                                foreach (DataRow cdr in ds.Tables[1].Rows)
                                {
                                    objInvoiceProductInfo = new InvoiceProductInfo();

                                    objInvoiceProductInfo.DataUniqueID = cdr["DataUniqueID"].ToString();
                                    objInvoiceProductInfo.PricePerUnit = cdr["PricePerUnit"].ToString();
                                    objInvoiceProductInfo.ProductId = cdr["ProductId"].ToString();
                                    objInvoiceProductInfo.Quantity = cdr["Quantity"].ToString();
                                    objInvoiceProductInfo.TaxOnProduct = cdr["TaxOnProduct"].ToString();
                                    objInvoiceProductInfo.TotalAmount = cdr["TotalAmount"].ToString();
                                    objInvoiceProductInfo.TotalAmountIncludeTax = cdr["AmountIncludeTax"].ToString();

                                    obj.InvoiceProductList.Add(objInvoiceProductInfo);
                                }
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public InvoiceInfo GetDetails_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            if (!string.IsNullOrEmpty(InvoiceID))
            {
                List<InvoiceInfo> list = GetList_Bill(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new InvoiceInfo();
        }

        public bool Save_Bill(bool isOnlyDelete, InvoiceInfo objBillInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBillInfo.InvoiceDate, Validations.ValueType.DateTime, false, "Invoice Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.InvoiceDueDate, Validations.ValueType.DateTime, true, "Due Date", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.InvoiceID, Validations.ValueType.Integer, true, "Invoice Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusID, Validations.ValueType.Integer, false, "Customer", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.BranchID, Validations.ValueType.Integer, true, "Branch Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusAddress, Validations.ValueType.AlphaNumericSpecialChar, true, "Customer Address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusCountry, Validations.ValueType.Integer, true, "Customer Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusState, Validations.ValueType.Integer, true, "Customer State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization Code", out errormsg)) { return false; }
            if (!Validations.CheckIntegerArray(objBillInfo.ProductIDs, Validations.ValueType.AlphaNumericSpecialChar, false, "Products", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTBillsSave]", true))
            {
                dbhlper.AddPparameter("@InvoiceType", "B", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceDate", objBillInfo.InvoiceDate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceDueDate", objBillInfo.InvoiceDueDate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceID", objBillInfo.InvoiceID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.InvoiceID) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@InvoiceNumber", objBillInfo.InvoiceNumber, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BranchID", objBillInfo.BranchID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.BranchID) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@OrganizationCode", objBillInfo.OrganizationCode.Trim().Length > 0 ? objBillInfo.OrganizationCode : "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CusID", Convert.ToInt32(objBillInfo.CusID));
                dbhlper.AddPparameter("@ShipAddress", objBillInfo.ShipAddress, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShipCity", objBillInfo.ShipCity, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShipState", Convert.ToInt32(objBillInfo.ShipState), DBHelper.param_types.Int);
                dbhlper.AddPparameter("@ShipCountry", Convert.ToInt32(objBillInfo.ShipCountry), DBHelper.param_types.Int);
                dbhlper.AddPparameter("@XMLProductData", objBillInfo.ProductIDs, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Prices", objBillInfo.Prices, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Taxes", objBillInfo.Taxes, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ChangedCurrency", objBillInfo.ChangedCurrency, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PrevConversionRate", objBillInfo.PrevConversionRate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ConversionRate", objBillInfo.ConversionRate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@SumAmount", objBillInfo.SumAmount, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AmountPayable", objBillInfo.AmountPayable, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AmountDue", objBillInfo.AmountDue, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsReturned", (objBillInfo.IsReturned ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsCancelled", (objBillInfo.IsCancelled ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Expence Related
        public List<InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            using (DBHelper dbhlper = new DBHelper("[GetExpenceListMasters]"))
            {
                dbhlper.AddPparameter("@InvoiceID", InvoiceID);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@BranchID", BranchID);
                dbhlper.AddPparameter("@CusID", CusID);
                dbhlper.AddPparameter("@InvoiceDateFrom", InvoiceDateFrom);
                dbhlper.AddPparameter("@InvoiceDateTo", InvoiceDateTo);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<InvoiceInfo> list = new List<InvoiceInfo>();
                        InvoiceInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new InvoiceInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.InvoiceID = dr["InvoiceID"].ToString();
                            obj.CustomerName = dr["CustomerName"].ToString();
                            obj.BranchName = dr["BranchName"].ToString();
                            obj.InvoiceDate = dr["InvoiceDate"] != null ? Convert.ToDateTime(dr["InvoiceDate"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                            obj.InvoiceDueDate = dr["Duedate"] != null ? Convert.ToDateTime(dr["Duedate"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";
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

        public InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            if (!string.IsNullOrEmpty(InvoiceID))
            {
                List<InvoiceInfo> list = GetList_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new InvoiceInfo();
        }

        public bool Save_Expense(bool isOnlyDelete, InvoiceInfo objBillInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBillInfo.InvoiceDate, Validations.ValueType.DateTime, false, "Invoice Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.InvoiceDueDate, Validations.ValueType.DateTime, true, "Due Date", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.InvoiceID, Validations.ValueType.Integer, true, "Invoice Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.InvoiceID, Validations.ValueType.Integer, true, "Invoice Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusID, Validations.ValueType.Integer, true, "Customer Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.BranchID, Validations.ValueType.Integer, true, "Branch Unique Number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusAddress, Validations.ValueType.AlphaNumericSpecialChar, true, "Customer Address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusCountry, Validations.ValueType.Integer, true, "Customer Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBillInfo.CusState, Validations.ValueType.Integer, true, "Customer State", out errormsg)) { return false; }
            //if (!Validations.ValidateDataType(objBillInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, true, "Organization Code", out errormsg)) { return false; }
            if (!Validations.CheckIntegerArray(objBillInfo.ProductIDs, Validations.ValueType.AlphaNumericSpecialChar, true, "Products", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMstEpenseSave]", true))
            {
                dbhlper.AddPparameter("@InvoiceType", "E", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceDate", objBillInfo.InvoiceDate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceDueDate", objBillInfo.InvoiceDueDate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InvoiceID", objBillInfo.InvoiceID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.InvoiceID) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@InvoiceNumber", objBillInfo.InvoiceNumber, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BranchID", objBillInfo.BranchID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.BranchID) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@OrganizationCode", objBillInfo.OrganizationCode.Trim().Length > 0 ? objBillInfo.OrganizationCode : "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CusID", Convert.ToInt32(objBillInfo.CusID));
                dbhlper.AddPparameter("@CusAddress", objBillInfo.CusAddress, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CusState", Convert.ToInt32(objBillInfo.CusState), DBHelper.param_types.Int);
                dbhlper.AddPparameter("@CusCountry", Convert.ToInt32(objBillInfo.CusCountry), DBHelper.param_types.Int);
                dbhlper.AddPparameter("@XMLProductData", objBillInfo.ProductIDs, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Prices", objBillInfo.Prices, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Taxes", objBillInfo.Taxes, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsReturned", (objBillInfo.IsReturned ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsCancelled", (objBillInfo.IsCancelled ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserID", objUserInfo.UserID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Coutry & State List
        public List<CountryInfo> GetList_Country()
        {
            using (DBHelper dbhlper = new DBHelper("GetCountryStateList"))
            {
                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CountryInfo> list = new List<CountryInfo>();
                        CountryInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CountryInfo();
                            obj.CountryID = dr["ID"].ToString();
                            obj.CountryName = dr["Name"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }

                return null;
            }
        }

        public List<StateInfo> GetList_State(string CountryID)
        {
            using (DBHelper dbhlper = new DBHelper("GetCountryStateList"))
            {
                dbhlper.AddPparameter("@CountryID", CountryID);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<StateInfo> list = new List<StateInfo>();
                        StateInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new StateInfo();
                            obj.StateID = dr["ID"].ToString();
                            obj.StateName = dr["Name"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Branch Related
        public List<BranchInfo> GetList_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spMSTBranch_Get"))
            {
                dbhlper.AddPparameter("@ID", BranchID);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BranchInfo> list = new List<BranchInfo>();
                        BranchInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new BranchInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.BranchID = dr["BranchID"].ToString();
                            obj.BranchName = dr["BranchName"].ToString();
                            obj.IsMainBranch = dr["IsMainBranch"].ToString();
                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
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

        public BranchInfo GetDetails_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            if (!string.IsNullOrEmpty(BranchID))
            {
                List<BranchInfo> list = GetList_Branch(BranchID, OrganizationCode, IsMainBranch, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new BranchInfo();
        }

        public bool Save_Branch(bool isOnlyDelete, BranchInfo objBranchInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBranchInfo.BranchID, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.BranchName, Validations.ValueType.AlphaNumericSpecialChar, false, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.City.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "City", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTBranchSave]", true))
            {
                dbhlper.AddPparameter("@BranchID", objBranchInfo.BranchID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BranchName", objBranchInfo.BranchName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationCode", objBranchInfo.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsMainBranch", objBranchInfo.IsMainBranch, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street1", objBranchInfo.Street1, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street2", objBranchInfo.Street2, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", objBranchInfo.City, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", objBranchInfo.State, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Country", objBranchInfo.Country, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PIN", objBranchInfo.PIN, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", objBranchInfo.IsActive, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        //public List<BranchProductMappingInfo> GetList_BranchProductMapping(string OrganizationCode, string BranchID, string LanguageId)
        //{
        //    using (DBHelper dbhlper = new DBHelper("spBranchProductMappingGet"))
        //    {
        //        dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
        //        dbhlper.AddPparameter("@BranchID", BranchID);
        //        dbhlper.AddPparameter("@LanguageId", LanguageId);

        //        using (DataSet ds = dbhlper.Execute_Query())
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                List<BranchProductMappingInfo> list = new List<BranchProductMappingInfo>();
        //                BranchProductMappingInfo obj = null;

        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    obj = new BranchProductMappingInfo();
        //                    obj.ProductId = dr["ProductID"].ToString();
        //                    obj.ProductName = dr["ProductName"].ToString();
        //                    obj.AllreadyExists = dr["AllreadyExists"].ToString();

        //                    list.Add(obj);
        //                }

        //                return list;
        //            }
        //        }
        //    }

        //    return null;
        //}

        public bool Save_BranchProductMapping(bool isOnlyDelete, string OrganizationCode, string ProductIds, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(ProductIds, Validations.ValueType.AlphaNumericSpecialChar, false, "Products", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("spMasterDataMultiLanguageSave", true))
            {
                dbhlper.AddPparameter("@ProductIds", ProductIds, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BranchID", "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Bank Related
        public List<BankInfo> GetList_Bank(string BankID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spMSTBanksGet"))
            {
                dbhlper.AddPparameter("@ID", BankID);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
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

        public BankInfo GetDetails_Bank(string BankID, string OrganizationCode, bool IsActive)
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

        public bool Save_Bank(bool isOnlyDelete, BankInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
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
                dbhlper.AddPparameter("@BankID", objBankInfo.BankID.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.BankID) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@Name", objBankInfo.Name, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationCode", objBankInfo.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CorpID", objBankInfo.CorpID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Address", objBankInfo.Address, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IFSCCode", objBankInfo.IFSCCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@MCRCode", objBankInfo.MCRCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", objBankInfo.IsActive, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public List<BankTransactionInfo> GetList_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spBankTransactionsGet]"))
            {
                dbhlper.AddPparameter("@ID", BankTransactionID);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
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

        public BankTransactionInfo GetDetails_BankTransaction(string BankTransactionID, string OrganizationCode, bool IsActive)
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

        public bool Save_BankTransaction(bool isOnlyDelete, BankTransactionInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.Products, Validations.ValueType.AlphaNumericSpecialChar, true, "Products", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CustomerId, Validations.ValueType.Integer, true, "Customer/Supplier", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Tax, Validations.ValueType.Numeric, true, "Tax", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spBankTransactionsSave]", true))
            {
                dbhlper.AddPparameter("@BankTransactionId", objBankInfo.Id.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.Id) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@Products", objBankInfo.Products, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CustomerId", objBankInfo.CustomerId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsSellExpense", objBankInfo.IsSellExpense, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Tax", objBankInfo.Tax, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public bool Upload_BankTransaction(bool isOvereWrite, DataSet ds, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
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
                            dbhlper.AddPparameter("@typeBankTransactions", ds.Tables[0], DBHelper.param_types.Structured);
                            dbhlper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            if (dbhlper.Execute_NonQuery(out errormsg))
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

        #region Category Related
        public List<CategoryInfo> GetList_CategoryForDropdown(string CategoryID, string Option, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spPRMSTCategoryGet]"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@CategoryId", CategoryID.Trim().Length > 0 ? Convert.ToInt32(CategoryID) : 0);

                if (Option == "P")
                {
                    dbhlper.AddPparameter("@WillCarryContent", "N");
                }
                else if (Option == "C")
                {
                    dbhlper.AddPparameter("@WillCarryContent", "Y");
                }
                else
                {
                    dbhlper.AddPparameter("@WillCarryContent", "");
                }

                dbhlper.AddPparameter("@CountryId", 0);
                dbhlper.AddPparameter("@ProductId", 0);
                dbhlper.AddPparameter("@IsActive", "Y");

                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0, DBHelper.param_types.BigInt);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CategoryInfo> list = new List<CategoryInfo>();
                        CategoryInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CategoryInfo();
                            obj.CategoryId = dr["CategoryID"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public List<CategoryInfo> GetList_Category(string CategoryID, string CountryId, string ProductId, bool IsActive, string Option, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spPRMSTCategoryGet]"))
            {
                dbhlper.AddPparameter("@Mode", 0);
                dbhlper.AddPparameter("@CategoryId", CategoryID.Trim().Length > 0 ? Convert.ToInt32(CategoryID) : 0);

                if (Option == "P")
                {
                    dbhlper.AddPparameter("@WillCarryContent", "N");
                }
                else if (Option == "C")
                {
                    dbhlper.AddPparameter("@WillCarryContent", "Y");
                }
                else
                {
                    dbhlper.AddPparameter("@WillCarryContent", "");
                }

                dbhlper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@ProductId", ProductId.Trim().Length > 0 ? Convert.ToInt32(ProductId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@IsActive", "");
                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0, DBHelper.param_types.BigInt);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CategoryInfo> list = new List<CategoryInfo>();
                        CategoryInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CategoryInfo();
                            obj.CategoryId = dr["CategoryID"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.WillCarryContent = dr["WillCarryContent"].ToString();
                            obj.ParentCategoryId = dr["ParentCategoryId"].ToString();
                            obj.ParentCategoryName = dr["ParentCategoryName"].ToString();
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNCode = dr["HSNCode"].ToString();
                            obj.SACCode = dr["SACCode"].ToString();
                            /*obj.CGST = dr["CGST"].ToString();
                            obj.SGST = dr["SGST"].ToString();
                            obj.IGST = dr["IGST"].ToString();*/
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

            return new List<CategoryInfo>();
        }

        public CategoryInfo GetDetails_Category(string CategoryID, string CountryId, string ProductId, bool IsActive, string LanguageId)
        {
            if (!string.IsNullOrEmpty(CategoryID))
            {
                List<CategoryInfo> list = GetList_Category(CategoryID, CountryId, ProductId, IsActive, "", LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new CategoryInfo();
        }

        public bool Save_Category(bool isOnlyDelete, CategoryInfo objCategory, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(objCategory.CategoryId, Validations.ValueType.Integer, true, "Category ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCategory.CategoryName, Validations.ValueType.AlphaNumericSpecialChar, true, "category Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spPrMstCategorySave]", true))
            {
                dbhlper.AddPparameter("@CategoryId", objCategory.CategoryId == "" ? 0 : Convert.ToInt32(objCategory.CategoryId), DBHelper.param_types.Int);
                dbhlper.AddPparameter("@ParentCategoryId", objCategory.ParentCategoryId == "" ? 0 : Convert.ToInt32(objCategory.ParentCategoryId), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CategoryName", HttpUtility.HtmlEncode(objCategory.CategoryName), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ServiceOrGoods", objCategory.ServiceOrGoods, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@HSNSACCode", objCategory.ServiceOrGoods == "G" ? objCategory.HSNCode : objCategory.SACCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@WillCarryContent", objCategory.WillCarryContent, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", objCategory.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public bool Upload_Category(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";
            System.Text.StringBuilder sbErroeMessage = new System.Text.StringBuilder();

            sbErroeMessage.AppendLine("<table class='table'>");
            sbErroeMessage.AppendLine("   <thead>");
            sbErroeMessage.AppendLine("     <tr>");
            sbErroeMessage.AppendLine("       <td>Row number</td>");
            sbErroeMessage.AppendLine("       <td>Message</td>");
            sbErroeMessage.AppendLine("     </tr>");
            sbErroeMessage.AppendLine("   </thead>");
            sbErroeMessage.AppendLine("   <tbody>");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Validation - Column existance
                if (ds.Tables[0].Columns.IndexOf("Category") < 0) { sbErroeMessage.AppendLine("<tr><td>Column related</td><td>[Category] column is not available</td></tr>"); }
                else if (ds.Tables[0].Columns.IndexOf("Parent Category") < 0) { sbErroeMessage.AppendLine("<tr><td>Column related</td><td>[Parent Category] column is not available</td></tr>"); }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { sbErroeMessage.AppendLine("<tr><td>Column related</td><td>[Country] column is not available</td></tr>"); }
                else if (ds.Tables[0].Columns.IndexOf("CGST") < 0 && ds.Tables[0].Columns.IndexOf("SGST") < 0 && ds.Tables[0].Columns.IndexOf("IGST") < 0 && ds.Tables[0].Columns.IndexOf("VAT") < 0)
                {
                    sbErroeMessage.AppendLine("<tr><td>Column related</td><td>[Tax related] column is not available</td></tr>");
                }
                else
                {
                    ds.Tables[0].Columns.Add("Row Number");
                    ds.Tables[0].Columns.Add("Error Message");

                    bool DisplayErroeMessage = false;

                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr["Category"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Category", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Parent Category"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Category", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Country", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["CGST"].ToString(), Validations.ValueType.Percentage, true, "CGST", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["SGST"].ToString(), Validations.ValueType.Percentage, true, "SGST", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["IGST"].ToString(), Validations.ValueType.Percentage, true, "IGST", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["VAT"].ToString(), Validations.ValueType.Percentage, true, "VAT", out errormsg)) { sbErroeMessage.AppendLine("<tr><td>" + (ds.Tables[0].Rows.IndexOf(dr) + 1) + "</td><td>" + errormsg + "</td></tr>"); DisplayErroeMessage = true; }
                        #endregion
                    }
                    #endregion

                    if (!DisplayErroeMessage)
                    {
                        sbErroeMessage = null;

                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Error Message"));
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Row Number"));

                        using (DBHelper dbhlper = new DBHelper("[spPRMstCategoryUpload]", true))
                        {
                            dbhlper.AddPparameter("@CategoryData", ds.Tables[0], DBHelper.param_types.Structured);

                            dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            return dbhlper.Execute_NonQuery(out errormsg);
                        }
                    }
                    else
                    {
                        sbErroeMessage.AppendLine("   </tbody>");
                        sbErroeMessage.AppendLine("</table>");
                        errormsg = sbErroeMessage.ToString();
                        return false;
                    }
                }
            }

            errormsg = "No data is available to be uploaded";
            return false;
        }

        #endregion

        #region Function Related
        public List<FunctionInfo> GetList_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spURMSTFunctionsGet"))
            {
                dbhlper.AddPparameter("@ID", FunctionID);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<FunctionInfo> list = new List<FunctionInfo>();
                        FunctionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new FunctionInfo();
                            obj.FunctionId = dr["FunctionId"].ToString();
                            obj.FunctionName = dr["FunctionName"].ToString();
                            obj.IsForModerate = dr["IsForModerate"].ToString();
                            obj.IsForMembership = dr["IsForMembership"].ToString();
                            obj.IsDesignation = dr["IsDesignation"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.IsDefaultForModerateUser = dr["IsDefaultForModerateUser"].ToString().Trim().ToUpper() == "Y";
                            obj.IsDefaultForRegisteredUser = dr["IsDefaultForRegisteredUser"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            if (!string.IsNullOrEmpty(FunctionID) && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
                            {
                                obj.RoleList = new List<RoleInfo>();
                                RoleInfo objRole = null;

                                foreach (DataRow drc in ds.Tables[1].Rows)
                                {
                                    objRole = new RoleInfo();

                                    objRole.RoleID = drc["RoleId"].ToString();
                                    objRole.RoleName = drc["RoleName"].ToString();
                                    objRole.isSelected = drc["IsSelected"].ToString().Trim().ToUpper() == "Y";
                                    objRole.IsOnlyForModerateUsers = drc["isOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";

                                    obj.RoleList.Add(objRole);
                                }
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                    else if (FunctionID.Trim() == "-1")
                    {
                        List<FunctionInfo> list = new List<FunctionInfo>();
                        FunctionInfo obj = null;

                        obj = new FunctionInfo();
                        obj.FunctionId = "0";
                        obj.FunctionName = "";
                        obj.IsForModerate = "";
                        obj.IsForMembership = "";
                        obj.IsDesignation = "";

                        if (!string.IsNullOrEmpty(FunctionID) && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
                        {
                            obj.RoleList = new List<RoleInfo>();
                            RoleInfo objRole = null;

                            foreach (DataRow drc in ds.Tables[1].Rows)
                            {
                                objRole = new RoleInfo();

                                objRole.RoleID = drc["RoleId"].ToString();
                                objRole.RoleName = drc["RoleName"].ToString();
                                objRole.isSelected = false;
                                objRole.IsOnlyForModerateUsers = drc["isOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";

                                obj.RoleList.Add(objRole);
                            }
                        }

                        list.Add(obj);

                        return list;
                    }
                }

                return null;
            }
        }

        public FunctionInfo GetDetails_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            if (!string.IsNullOrEmpty(FunctionID))
            {
                List<FunctionInfo> list = GetList_Function(FunctionID, OrganizationCode, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new FunctionInfo();
        }

        public bool Save_Function(bool isOnlyDelete, FunctionInfo objFunctionInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (objFunctionInfo.FunctionId.Trim() != "-1")
            {
                if (!Validations.ValidateDataType(objFunctionInfo.FunctionId, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            }

            if (!Validations.ValidateDataType(objFunctionInfo.FunctionName, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spURMSTFunctionsSave]", true))
            {
                dbhlper.AddPparameter("@FunctionId", objFunctionInfo.FunctionId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@FunctionName", objFunctionInfo.FunctionName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsForModerate", objFunctionInfo.IsForModerate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsForMembership", objFunctionInfo.IsForMembership, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsDesignation", objFunctionInfo.IsDesignation, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsDefaultForModerateUser", objFunctionInfo.IsDefaultForModerateUser ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsDefaultForRegisteredUser", objFunctionInfo.IsDefaultForRegisteredUser ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Roles", objFunctionInfo.Roles, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", objFunctionInfo.IsActive, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);

            }

        }
        #endregion

        #region Role Related
        public List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("SPURMSTROLEGET"))
            {
                dbhlper.AddPparameter("@RoleID", !string.IsNullOrEmpty(RoleID) ? Convert.ToInt64(RoleID) : 0);
                dbhlper.AddPparameter("@BranchId", BranchId);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@UserID", UserID);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<RoleInfo> list = new List<RoleInfo>();
                        RoleInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            #region Read Object
                            obj = new RoleInfo();
                            obj.RoleID = dr["RoleID"].ToString();
                            obj.RoleName = HttpUtility.HtmlEncode(dr["RoleName"].ToString());

                            obj.IsOnlyForModerateUsers = dr["IsOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";

                            obj.Category_Audit = dr["CanManageCategory"].ToString().Contains("5");
                            obj.Category_View = dr["CanManageCategory"].ToString().Contains("1");
                            obj.Category_Add = dr["CanManageCategory"].ToString().Contains("2");
                            obj.Category_Edit = dr["CanManageCategory"].ToString().Contains("3");
                            obj.Category_Delete = dr["CanManageCategory"].ToString().Contains("4");

                            obj.Product_Audit = dr["CanManageProduct"].ToString().Contains("5");
                            obj.Product_View = dr["CanManageProduct"].ToString().Contains("1");
                            obj.Product_Add = dr["CanManageProduct"].ToString().Contains("2");
                            obj.Product_Edit = dr["CanManageProduct"].ToString().Contains("3");
                            obj.Product_Delete = dr["CanManageProduct"].ToString().Contains("4");

                            obj.Role_Audit = dr["CanManageRole"].ToString().Contains("5");
                            obj.Role_View = dr["CanManageRole"].ToString().Contains("1");
                            obj.Role_Add = dr["CanManageRole"].ToString().Contains("2");
                            obj.Role_Edit = dr["CanManageRole"].ToString().Contains("3");
                            obj.Role_Delete = dr["CanManageRole"].ToString().Contains("4");

                            obj.Function_Audit = dr["CanManageFunction"].ToString().Contains("5");
                            obj.Function_View = dr["CanManageFunction"].ToString().Contains("1");
                            obj.Function_Add = dr["CanManageFunction"].ToString().Contains("2");
                            obj.Function_Edit = dr["CanManageFunction"].ToString().Contains("3");
                            obj.Function_Delete = dr["CanManageFunction"].ToString().Contains("4");

                            obj.UserRegistered_Audit = dr["CanManageUserRegistered"].ToString().Contains("5");
                            obj.UserRegistered_View = dr["CanManageUserRegistered"].ToString().Contains("1");
                            obj.UserRegistered_Add = dr["CanManageUserRegistered"].ToString().Contains("2");
                            obj.UserRegistered_Edit = dr["CanManageUserRegistered"].ToString().Contains("3");
                            obj.UserRegistered_Delete = dr["CanManageUserRegistered"].ToString().Contains("4");

                            obj.UserModerate_Audit = dr["CanManageUserModerate"].ToString().Contains("5");
                            obj.UserModerate_View = dr["CanManageUserModerate"].ToString().Contains("1");
                            obj.UserModerate_Add = dr["CanManageUserModerate"].ToString().Contains("2");
                            obj.UserModerate_Edit = dr["CanManageUserModerate"].ToString().Contains("3");
                            obj.UserModerate_Delete = dr["CanManageUserModerate"].ToString().Contains("4");

                            obj.Organization_Audit = dr["CanManageOrganization"].ToString().Contains("5");
                            obj.Organization_View = dr["CanManageOrganization"].ToString().Contains("1");
                            obj.Organization_Add = dr["CanManageOrganization"].ToString().Contains("2");
                            obj.Organization_Edit = dr["CanManageOrganization"].ToString().Contains("3");
                            obj.Organization_Delete = dr["CanManageOrganization"].ToString().Contains("4");

                            obj.Branch_Audit = dr["CanManageBranch"].ToString().Contains("5");
                            obj.Branch_View = dr["CanManageBranch"].ToString().Contains("1");
                            obj.Branch_Add = dr["CanManageBranch"].ToString().Contains("2");
                            obj.Branch_Edit = dr["CanManageBranch"].ToString().Contains("3");
                            obj.Branch_Delete = dr["CanManageBranch"].ToString().Contains("4");

                            obj.Customer_Audit = dr["CanManageCustomer"].ToString().Contains("5");
                            obj.Customer_View = dr["CanManageCustomer"].ToString().Contains("1");
                            obj.Customer_Add = dr["CanManageCustomer"].ToString().Contains("2");
                            obj.Customer_Edit = dr["CanManageCustomer"].ToString().Contains("3");
                            obj.Customer_Delete = dr["CanManageCustomer"].ToString().Contains("4");

                            obj.Bill_Audit = dr["CanManageBill"].ToString().Contains("5");
                            obj.Bill_View = dr["CanManageBill"].ToString().Contains("1");
                            obj.Bill_Add = dr["CanManageBill"].ToString().Contains("2");
                            obj.Bill_Edit = dr["CanManageBill"].ToString().Contains("3");
                            obj.Bill_Delete = dr["CanManageBill"].ToString().Contains("4");

                            obj.Language_Audit = dr["CanManageLanguage"].ToString().Contains("5");
                            obj.Language_View = dr["CanManageLanguage"].ToString().Contains("1");
                            obj.Language_Add = dr["CanManageLanguage"].ToString().Contains("2");
                            obj.Language_Edit = dr["CanManageLanguage"].ToString().Contains("3");
                            obj.Language_Delete = dr["CanManageLanguage"].ToString().Contains("4");

                            obj.DefineLanguage_Audit = dr["CanManageDefineLanguage"].ToString().Contains("5");
                            obj.DefineLanguage_View = dr["CanManageDefineLanguage"].ToString().Contains("1");
                            obj.DefineLanguage_Add = dr["CanManageDefineLanguage"].ToString().Contains("2");
                            obj.DefineLanguage_Edit = dr["CanManageDefineLanguage"].ToString().Contains("3");
                            obj.DefineLanguage_Delete = dr["CanManageDefineLanguage"].ToString().Contains("4");

                            obj.StaticValue_Audit = dr["CanManageStaticValue"].ToString().Contains("5");
                            obj.StaticValue_View = dr["CanManageStaticValue"].ToString().Contains("1");
                            obj.StaticValue_Add = dr["CanManageStaticValue"].ToString().Contains("2");
                            obj.StaticValue_Edit = dr["CanManageStaticValue"].ToString().Contains("3");
                            obj.StaticValue_Delete = dr["CanManageStaticValue"].ToString().Contains("4");

                            obj.Terms_Audit = dr["CanManageTerms"].ToString().Contains("5");
                            obj.Terms_View = dr["CanManageTerms"].ToString().Contains("1");
                            obj.Terms_Add = dr["CanManageTerms"].ToString().Contains("2");
                            obj.Terms_Edit = dr["CanManageTerms"].ToString().Contains("3");
                            obj.Terms_Delete = dr["CanManageTerms"].ToString().Contains("4");

                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            list.Add(obj);
                            #endregion
                        }

                        return list;
                    }
                }

                return null;
            }
        }

        public RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            List<RoleInfo> list = GetList_Role(RoleID, BranchId, UserID, IsActive);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new RoleInfo();
        }

        public bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.RoleID, Validations.ValueType.Integer, true, "Unique Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.RoleName, Validations.ValueType.AlphaNumericSpecialChar, false, "Role Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("spUrMstRoleSave", true))
            {
                dbhlper.AddPparameter("@RoleID", !string.IsNullOrEmpty(obj.RoleID) ? Convert.ToInt64(obj.RoleID) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@RoleName", HttpUtility.HtmlEncode(obj.RoleName), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageCategory", (obj.Category_View ? "1" : "") + (obj.Category_Add ? "2" : "") + (obj.Category_Edit ? "3" : "") + (obj.Category_Delete ? "4" : "") + (obj.Category_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageProduct", (obj.Product_View ? "1" : "") + (obj.Product_Add ? "2" : "") + (obj.Product_Edit ? "3" : "") + (obj.Product_Delete ? "4" : "") + (obj.Product_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageRole", (obj.Role_View ? "1" : "") + (obj.Role_Add ? "2" : "") + (obj.Role_Edit ? "3" : "") + (obj.Role_Delete ? "4" : "") + (obj.Role_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageFunction", (obj.Function_View ? "1" : "") + (obj.Function_Add ? "2" : "") + (obj.Function_Edit ? "3" : "") + (obj.Function_Delete ? "4" : "") + (obj.Function_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageUserRegistered", (obj.UserRegistered_View ? "1" : "") + (obj.UserRegistered_Add ? "2" : "") + (obj.UserRegistered_Edit ? "3" : "") + (obj.UserRegistered_Delete ? "4" : "") + (obj.UserRegistered_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageUserModerate", (obj.UserModerate_View ? "1" : "") + (obj.UserModerate_Add ? "2" : "") + (obj.UserModerate_Edit ? "3" : "") + (obj.UserModerate_Delete ? "4" : "") + (obj.UserModerate_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageOrganization", (obj.Organization_View ? "1" : "") + (obj.Organization_Add ? "2" : "") + (obj.Organization_Edit ? "3" : "") + (obj.Organization_Delete ? "4" : "") + (obj.Organization_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageBranch", (obj.Branch_View ? "1" : "") + (obj.Branch_Add ? "2" : "") + (obj.Branch_Edit ? "3" : "") + (obj.Branch_Delete ? "4" : "") + (obj.Branch_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageCustomer", (obj.Customer_View ? "1" : "") + (obj.Customer_Add ? "2" : "") + (obj.Customer_Edit ? "3" : "") + (obj.Customer_Delete ? "4" : "") + (obj.Customer_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageBill", (obj.Bill_View ? "1" : "") + (obj.Bill_Add ? "2" : "") + (obj.Bill_Edit ? "3" : "") + (obj.Bill_Delete ? "4" : "") + (obj.Bill_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageLanguage", (obj.Language_View ? "1" : "") + (obj.Language_Add ? "2" : "") + (obj.Language_Edit ? "3" : "") + (obj.Language_Delete ? "4" : "") + (obj.Language_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageDefineLanguage", (obj.DefineLanguage_View ? "1" : "") + (obj.DefineLanguage_Add ? "2" : "") + (obj.DefineLanguage_Edit ? "3" : "") + (obj.DefineLanguage_Delete ? "4" : "") + (obj.DefineLanguage_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageStaticvalue", (obj.StaticValue_View ? "1" : "") + (obj.StaticValue_Add ? "2" : "") + (obj.StaticValue_Edit ? "3" : "") + (obj.StaticValue_Edit ? "4" : "") + (obj.StaticValue_Edit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CanManageTerms", (obj.Terms_View ? "1" : "") + (obj.Terms_Add ? "2" : "") + (obj.Terms_Edit ? "3" : "") + (obj.Terms_Delete ? "4" : "") + (obj.Terms_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyForModerateUsers", obj.IsOnlyForModerateUsers ? 'Y' : 'N', DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isForMembershipView", (obj.isForMembershipView ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);

            }
        }

        public RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID)
        {
            using (DBHelper dbhlper = new DBHelper("GetEffectiveRoleForAUser"))
            {
                dbhlper.AddPparameter("@UserCode", UserID);
                dbhlper.AddPparameter("@BranchId", BranchId);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        RoleInfo obj = new RoleInfo();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            #region Read Object
                            if (!obj.Category_Audit) obj.Category_Audit = dr["CanManageCategory"].ToString().Contains("5");
                            if (!obj.Category_View) obj.Category_View = dr["CanManageCategory"].ToString().Contains("1");
                            if (!obj.Category_Add) obj.Category_Add = dr["CanManageCategory"].ToString().Contains("2");
                            if (!obj.Category_Edit) obj.Category_Edit = dr["CanManageCategory"].ToString().Contains("3");
                            if (!obj.Category_Delete) obj.Category_Delete = dr["CanManageCategory"].ToString().Contains("4");

                            if (!obj.Product_Audit) obj.Product_Audit = dr["CanManageProduct"].ToString().Contains("5");
                            if (!obj.Product_View) obj.Product_View = dr["CanManageProduct"].ToString().Contains("1");
                            if (!obj.Product_Add) obj.Product_Add = dr["CanManageProduct"].ToString().Contains("2");
                            if (!obj.Product_Edit) obj.Product_Edit = dr["CanManageProduct"].ToString().Contains("3");
                            if (!obj.Product_Delete) obj.Product_Delete = dr["CanManageProduct"].ToString().Contains("4");

                            if (!obj.Role_Audit) obj.Role_Audit = dr["CanManageRole"].ToString().Contains("5");
                            if (!obj.Role_View) obj.Role_View = dr["CanManageRole"].ToString().Contains("1");
                            if (!obj.Role_Add) obj.Role_Add = dr["CanManageRole"].ToString().Contains("2");
                            if (!obj.Role_Edit) obj.Role_Edit = dr["CanManageRole"].ToString().Contains("3");
                            if (!obj.Role_Delete) obj.Role_Delete = dr["CanManageRole"].ToString().Contains("4");

                            if (!obj.Function_Audit) obj.Function_Audit = dr["CanManageFunction"].ToString().Contains("5");
                            if (!obj.Function_View) obj.Function_View = dr["CanManageFunction"].ToString().Contains("1");
                            if (!obj.Function_Add) obj.Function_Add = dr["CanManageFunction"].ToString().Contains("2");
                            if (!obj.Function_Edit) obj.Function_Edit = dr["CanManageFunction"].ToString().Contains("3");
                            if (!obj.Function_Delete) obj.Function_Delete = dr["CanManageFunction"].ToString().Contains("4");

                            if (!obj.UserRegistered_Audit) obj.UserRegistered_Audit = dr["CanManageUserRegistered"].ToString().Contains("5");
                            if (!obj.UserRegistered_View) obj.UserRegistered_View = dr["CanManageUserRegistered"].ToString().Contains("1");
                            if (!obj.UserRegistered_Add) obj.UserRegistered_Add = dr["CanManageUserRegistered"].ToString().Contains("2");
                            if (!obj.UserRegistered_Edit) obj.UserRegistered_Edit = dr["CanManageUserRegistered"].ToString().Contains("3");
                            if (!obj.UserRegistered_Delete) obj.UserRegistered_Delete = dr["CanManageUserRegistered"].ToString().Contains("4");

                            if (!obj.UserModerate_Audit) obj.UserModerate_Audit = dr["CanManageUserModerate"].ToString().Contains("5");
                            if (!obj.UserModerate_View) obj.UserModerate_View = dr["CanManageUserModerate"].ToString().Contains("1");
                            if (!obj.UserModerate_Add) obj.UserModerate_Add = dr["CanManageUserModerate"].ToString().Contains("2");
                            if (!obj.UserModerate_Edit) obj.UserModerate_Edit = dr["CanManageUserModerate"].ToString().Contains("3");
                            if (!obj.UserModerate_Delete) obj.UserModerate_Delete = dr["CanManageUserModerate"].ToString().Contains("4");

                            if (!obj.Organization_Audit) obj.Organization_Audit = dr["CanManageOrganization"].ToString().Contains("5");
                            if (!obj.Organization_View) obj.Organization_View = dr["CanManageOrganization"].ToString().Contains("1");
                            if (!obj.Organization_Add) obj.Organization_Add = dr["CanManageOrganization"].ToString().Contains("2");
                            if (!obj.Organization_Edit) obj.Organization_Edit = dr["CanManageOrganization"].ToString().Contains("3");
                            if (!obj.Organization_Delete) obj.Organization_Delete = dr["CanManageOrganization"].ToString().Contains("4");

                            if (!obj.Branch_Audit) obj.Branch_Audit = dr["CanManageBranch"].ToString().Contains("5");
                            if (!obj.Branch_View) obj.Branch_View = dr["CanManageBranch"].ToString().Contains("1");
                            if (!obj.Branch_Add) obj.Branch_Add = dr["CanManageBranch"].ToString().Contains("2");
                            if (!obj.Branch_Edit) obj.Branch_Edit = dr["CanManageBranch"].ToString().Contains("3");
                            if (!obj.Branch_Delete) obj.Branch_Delete = dr["CanManageBranch"].ToString().Contains("4");

                            if (!obj.Customer_Audit) obj.Customer_Audit = dr["CanManageCustomer"].ToString().Contains("5");
                            if (!obj.Customer_View) obj.Customer_View = dr["CanManageCustomer"].ToString().Contains("1");
                            if (!obj.Customer_Add) obj.Customer_Add = dr["CanManageCustomer"].ToString().Contains("2");
                            if (!obj.Customer_Edit) obj.Customer_Edit = dr["CanManageCustomer"].ToString().Contains("3");
                            if (!obj.Customer_Delete) obj.Customer_Delete = dr["CanManageCustomer"].ToString().Contains("4");

                            if (!obj.Bill_Audit) obj.Bill_Audit = dr["CanManageBill"].ToString().Contains("5");
                            if (!obj.Bill_View) obj.Bill_View = dr["CanManageBill"].ToString().Contains("1");
                            if (!obj.Bill_Add) obj.Bill_Add = dr["CanManageBill"].ToString().Contains("2");
                            if (!obj.Bill_Edit) obj.Bill_Edit = dr["CanManageBill"].ToString().Contains("3");
                            if (!obj.Bill_Delete) obj.Bill_Delete = dr["CanManageBill"].ToString().Contains("4");

                            if (!obj.Language_Audit) obj.Language_Audit = dr["CanManageLanguage"].ToString().Contains("5");
                            if (!obj.Language_View) obj.Language_View = dr["CanManageLanguage"].ToString().Contains("1");
                            if (!obj.Language_Add) obj.Language_Add = dr["CanManageLanguage"].ToString().Contains("2");
                            if (!obj.Language_Edit) obj.Language_Edit = dr["CanManageLanguage"].ToString().Contains("3");
                            if (!obj.Language_Delete) obj.Language_Delete = dr["CanManageLanguage"].ToString().Contains("4");

                            if (!obj.DefineLanguage_Audit) obj.DefineLanguage_Audit = dr["CanManageDefineLanguage"].ToString().Contains("5");
                            if (!obj.DefineLanguage_View) obj.DefineLanguage_View = dr["CanManageDefineLanguage"].ToString().Contains("1");
                            if (!obj.DefineLanguage_Add) obj.DefineLanguage_Add = dr["CanManageDefineLanguage"].ToString().Contains("2");
                            if (!obj.DefineLanguage_Edit) obj.DefineLanguage_Edit = dr["CanManageDefineLanguage"].ToString().Contains("3");
                            if (!obj.DefineLanguage_Delete) obj.DefineLanguage_Delete = dr["CanManageDefineLanguage"].ToString().Contains("4");

                            if (!obj.StaticValue_Audit) obj.StaticValue_Audit = dr["CanManageStaticValue"].ToString().Contains("5");
                            if (!obj.StaticValue_View) obj.StaticValue_View = dr["CanManageStaticValue"].ToString().Contains("1");
                            if (!obj.StaticValue_Add) obj.StaticValue_Add = dr["CanManageStaticValue"].ToString().Contains("2");
                            if (!obj.StaticValue_Edit) obj.StaticValue_Edit = dr["CanManageStaticValue"].ToString().Contains("3");
                            if (!obj.StaticValue_Delete) obj.StaticValue_Delete = dr["CanManageStaticValue"].ToString().Contains("4");

                            if (!obj.Terms_Audit) obj.Terms_Audit = dr["CanManageTerms"].ToString().Contains("5");
                            if (!obj.Terms_View) obj.Terms_View = dr["CanManageTerms"].ToString().Contains("1");
                            if (!obj.Terms_Add) obj.Terms_Add = dr["CanManageTerms"].ToString().Contains("2");
                            if (!obj.Terms_Edit) obj.Terms_Edit = dr["CanManageTerms"].ToString().Contains("3");
                            if (!obj.Terms_Delete) obj.Terms_Delete = dr["CanManageTerms"].ToString().Contains("4");
                            #endregion
                        }

                        Common.ErrorLog.LogSQLErrors_Comments(null, "Get_Effective_Role_ForAUser:=> Role Read complete");


                        return obj;
                    }
                }

                return new RoleInfo();
            }
        }
        #endregion

        #region Language Related
        public List<LanguageInfo> GetList_Language(string LanguageID, string LanguageName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[splangMSTLanguageGet]"))
            {
                dbhlper.AddPparameter("@LanguageId", LanguageID);
                dbhlper.AddPparameter("@LanguageName", LanguageName);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<LanguageInfo> list = new List<LanguageInfo>();
                        LanguageInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new LanguageInfo();
                            obj.LanguageId = dr["LanguageID"].ToString();
                            obj.LanguageName = dr["LanguageName"].ToString();
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

        public LanguageInfo GetDetails_Language(string LanguageID, string LanguageName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(LanguageID))
            {
                List<LanguageInfo> list = GetList_Language(LanguageID, LanguageName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new LanguageInfo();
        }

        public bool Save_Language(bool isOnlyDelete, LanguageInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.LanguageName, Validations.ValueType.AlphaNumericSpecialChar, true, "Language Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spLangMSTLanguageSave]", true))
            {
                dbhlper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LanguageName", obj.LanguageName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public List<LanguageValueInfo> GetList_DataValueLanguageWise(string MasterTablePrefixs, string LanguageIds, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[splangValuGet]"))
            {
                dbhlper.AddPparameter("@MasterIDField", "");
                dbhlper.AddPparameter("@MasterTablePrefixs", MasterTablePrefixs);
                dbhlper.AddPparameter("@LanguageIds", LanguageIds);
                dbhlper.AddPparameter("@IsActive", "");

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<LanguageValueInfo> list = new List<LanguageValueInfo>();
                        LanguageValueInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new LanguageValueInfo();
                            obj.LanguageId = dr["LanguageID"].ToString();
                            obj.LanguageName = dr["LanguageName"].ToString();
                            obj.MasterFieldValue = dr["MasterFieldValue"].ToString();
                            obj.MasterIDField = dr["MasterIDField"].ToString();
                            obj.MasterTablePrefix = dr["MasterTablePrefix"].ToString();

                            if (obj.MasterTablePrefix == "CA")
                            {
                                obj.MasterTableName = "Category";
                            }
                            else if (obj.MasterTablePrefix == "P")
                            {
                                obj.MasterTableName = "Product";
                            }

                            obj.value = dr["value"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }

                }
            }

            return null;
        }

        public bool Save_DataValueLanguageWise(bool isOnlyDelete, LanguageValueInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[splangValuSave]", true))
            {
                dbhlper.AddPparameter("@MasterIDField", obj.MasterIDField, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@MasterTablePrefix", obj.MasterTablePrefix, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Value", obj.value, DBHelper.param_types.NVarchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public List<LanguageCountryInfo> GetList_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spLanguageCountryGet]"))
            {
                dbhlper.AddPparameter("@Id", ID.Trim().Length > 0 ? ID : "0");
                dbhlper.AddPparameter("@LanguageId", LanguageID);
                dbhlper.AddPparameter("@CountryId", CountryID);
                dbhlper.AddPparameter("@Visibility", Visibility);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<LanguageCountryInfo> list = new List<LanguageCountryInfo>();
                        LanguageCountryInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new LanguageCountryInfo();
                            obj.Id = Convert.ToInt32(dr["Id"]);
                            obj.LanguageId = dr["LanguageId"].ToString();
                            obj.LanguageName = dr["LanguageName"].ToString();
                            obj.CountryId = dr["CountryId"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.Visibility = dr["Visibility"].ToString() == "Y";
                            obj.Proirity = Convert.ToInt32(dr["Priority"].ToString());

                            list.Add(obj);
                        }

                        return list;
                    }

                }
            }

            return null;
        }

        public LanguageCountryInfo GetDetails_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
        {
            if (!string.IsNullOrEmpty(LanguageID))
            {
                List<LanguageCountryInfo> list = GetList_LanguageCountry(ID, LanguageID, CountryID, Visibility, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new LanguageCountryInfo();
        }

        public bool Save_LanguageCountry(bool isOnlyDelete, LanguageCountryInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CountryId, Validations.ValueType.Integer, true, "Countrry Id", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spLanguageCountrySave]", true))
            {
                dbhlper.AddPparameter("@Id", obj.Id, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@CountryId", obj.CountryId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Visibility", (obj.Visibility ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Priority", obj.Proirity, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        #endregion

        #region Organization Related
        public List<OrganizationInfo> GetList_OrganizationDropdownList(string OrganizationCode, string isActive, string UserCode)
        {
            using (DBHelper dbhlper = new DBHelper("[spURMSTOrganizationGet]"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@IsActive", isActive.Trim());
                dbhlper.AddPparameter("@UserCode", UserCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<OrganizationInfo> list = new List<OrganizationInfo>();
                        OrganizationInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new OrganizationInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            //obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            //obj.IsActive = dr["IsActive"].ToString().Trim().ToLower() == "y";
                            //obj.ActivityType = dr["ActivityType"].ToString();
                            //obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);

                            list.Add(obj);
                        }

                        return list;
                    }
                }

                return new List<OrganizationInfo>();
            }
        }

        public List<OrganizationInfo> GetList_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("sporganizationget"))
            {
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@OrganizationName", OrganizationName);
                dbhlper.AddPparameter("@City", City);
                dbhlper.AddPparameter("@State", State);
                dbhlper.AddPparameter("@Country", Country);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@UserID", UserID);
                dbhlper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<OrganizationInfo> list = new List<OrganizationInfo>();
                        OrganizationInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new OrganizationInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }

                return null;
            }
        }

        public OrganizationInfo GetDetails_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId)
        {
            List<OrganizationInfo> list = GetList_Organization(OrganizationCode, OrganizationName, City, State, Country, UserID, IsActive, LanguageId);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new OrganizationInfo();
        }

        public bool Save_Organization(bool isOnlyDelete, OrganizationInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.OrganizationName, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization Name", out errormsg)) { return false; }

            using (DBHelper dbhlper = new DBHelper("spOrganizationsave", true))
            {
                dbhlper.AddPparameter("@OrganizationCode", obj.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationName", obj.OrganizationName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Organization Accountant Related
        public List<OrganizationAccountantInfo> GetList_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            using (DBHelper dbhlper = new DBHelper("[spMapOrganizationAccountantGet]"))
            {
                dbhlper.AddPparameter("@Mode", 0);
                dbhlper.AddPparameter("@ID", ID.Trim().Length > 0 ? ID.Trim() : "0");
                dbhlper.AddPparameter("@AccountantCode", AccountantCode);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<OrganizationAccountantInfo> list = new List<OrganizationAccountantInfo>();
                        OrganizationAccountantInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new OrganizationAccountantInfo();
                            obj.ID = dr["ID"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.AccountantCode = dr["AccountantCode"].ToString();
                            obj.AccountantName = dr["AccountantName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            var list1 = new List<OrganizationAccountantInfo>();
            list1.Add(new OrganizationAccountantInfo());
            return list1;
        }

        public List<OrganizationAccountantInfo> GetDetails_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            using (DBHelper dbhlper = new DBHelper("[spMapOrganizationAccountantGet]"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@ID", ID.Trim().Length > 0 ? ID.Trim() : "0");
                dbhlper.AddPparameter("@AccountantCode", AccountantCode);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<OrganizationAccountantInfo> list = new List<OrganizationAccountantInfo>();
                        OrganizationAccountantInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new OrganizationAccountantInfo();
                            obj.ID = dr["ID"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.AccountantCode = dr["AccountantCode"].ToString();
                            obj.AccountantName = dr["AccountantName"].ToString();
                            obj.isSelected = dr["isSelected"].ToString().Trim().ToUpper();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }


        public bool Save_OrganizationAccountant(bool isOnlyDelete, OrganizationAccountantInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.AccountantCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Accountant", out errormsg)) { return false; }

            using (DBHelper dbhlper = new DBHelper("spMapOrganizationAccountantSave", true))
            {
                dbhlper.AddPparameter("@ID", obj.ID.Trim().Length > 0 ? obj.ID : "0", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AccountantCode", obj.AccountantCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationCode", obj.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Product Related
        public List<ProductInfo> GetList_ProductDropdownlist(string ProductID, string ProductName, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spPrMstProductGet"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@ProductID", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                dbhlper.AddPparameter("@ProductName", ProductName);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@CategoryId", CategoryId.Trim().Length > 0 ? Convert.ToInt32(CategoryId) : 0);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ProductInfo> list = new List<ProductInfo>();
                        ProductInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ProductInfo();
                            obj.ProductId = dr["ProductID"].ToString();
                            obj.ProductName = dr["ProductName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public List<ProductInfo> GetList_Product(string ProductID, string ProductName, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spPrMstProductGet"))
            {
                dbhlper.AddPparameter("@Mode", 0);
                dbhlper.AddPparameter("@ProductID", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                dbhlper.AddPparameter("@ProductName", ProductName);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@CategoryId", CategoryId.Trim().Length > 0 ? Convert.ToInt32(CategoryId) : 0);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ProductInfo> list = new List<ProductInfo>();
                        ProductInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ProductInfo();
                            obj.ProductId = dr["ProductID"].ToString();
                            obj.ProductName = dr["ProductName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            //obj.ParentCategoryId = dr["ParentCategoryId"].ToString();
                            //obj.ParentCategoryName = dr["ParentCategoryName"].ToString();
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNCode = dr["HSNCode"].ToString();
                            obj.SACCode = dr["SACCode"].ToString();
                            //obj.CGST = dr["CGST"].ToString();
                            //obj.SGST = dr["SGST"].ToString();
                            //obj.IGST = dr["IGST"].ToString();
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

        public ProductInfo GetDetails_Product(string ProductID, string ProductName, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            if (!string.IsNullOrEmpty(ProductID))
            {
                List<ProductInfo> list = GetList_Product(ProductID, ProductName, OrganizationCode, CategoryId, IsActive, LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ProductInfo();
        }

        public bool Save_Product(bool isOnlyDelete, ProductInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.ProductId, Validations.ValueType.Integer, true, "Product ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ProductName, Validations.ValueType.AlphaNumericSpecialChar, false, "Product Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spPRMSTProductSave]", true))
            {
                dbhlper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.Int);
                dbhlper.AddPparameter("@ProductName", HttpUtility.HtmlEncode(obj.ProductName), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CategoryId", obj.CategoryId.Trim().Length > 0 ? Convert.ToInt32(obj.CategoryId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@CountryID", obj.CountryId.Trim().Length > 0 ? Convert.ToInt32(obj.CountryId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public bool Upload_Product(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Validation - Column existance
                if (ds.Tables[0].Columns.IndexOf("Category") < 0) { errormsg = "[Category] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Product") < 0) { errormsg = "[Product] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; }
                else
                {
                    ds.Tables[0].Columns.Add("Row Number");
                    ds.Tables[0].Columns.Add("Error Message");

                    bool DisplayErroeMessage = false;

                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr["Category"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Category", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Product"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Category", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        #endregion
                    }
                    #endregion

                    if (!DisplayErroeMessage)
                    {
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Error Message"));
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Row Number"));

                        using (DBHelper dbhlper = new DBHelper("[spPRMstProductUpload]", true))
                        {
                            dbhlper.AddPparameter("@ProductData", ds.Tables[0], DBHelper.param_types.Structured);

                            dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            if (dbhlper.Execute_NonQuery(out errormsg))
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

            errormsg = "No data is available to be uploaded";
            return false;
        }

        public List<ProductOrganiztionInfo> GetList_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductGet]"))
            {
                dbhlper.AddPparameter("@Mode", 0);
                dbhlper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                dbhlper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0);
                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ProductOrganiztionInfo> list = new List<ProductOrganiztionInfo>();
                        ProductOrganiztionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ProductOrganiztionInfo();
                            obj.OrganizationproductId = dr["OrganizationproductId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganizationproductId"]);
                            obj.ProductId = dr["ProductID"].ToString();
                            obj.ProductName = dr["ProductName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNSACCode = dr["HSNSACCode"].ToString();
                            obj.Name = dr["name"].ToString();
                            obj.Description = dr["Description"].ToString();
                            obj.SKU = dr["SKU"].ToString();
                            obj.Unit = dr["AbatementPercentage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Unit"]);
                            obj.Class = dr["Class"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Class"]);
                            obj.AbatementPercentage = dr["AbatementPercentage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AbatementPercentage"]);
                            obj.ServiceType = dr["ServiceType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ServiceType"]);
                            obj.SalePrice = dr["SalePrice"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SalePrice"]);
                            obj.isInclusiveTax = dr["isInclusiveTax"].ToString().Trim().ToUpper() == "Y";
                            obj.AvailableQty = dr["AvailableQty"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AvailableQty"]);
                            obj.IncomeAccount = dr["IncomeAccount"].ToString();
                            obj.SupplierId = dr["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SupplierId"]);
                            obj.PreferredSupplierId = dr["PreferredSupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PreferredSupplierId"]);
                            obj.ReverseCharge = dr["ReverseCharge"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ReverseCharge"]);
                            obj.PurchaseTax = dr["PurchaseTax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PurchaseTax"]);
                            obj.SaleTax = dr["SaleTax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SaleTax"]);

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            var list1 = new List<ProductOrganiztionInfo>();
            list1.Add(new ProductOrganiztionInfo());
            return list1;
        }

        public List<ProductOrganiztionInfo> GetDetails_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductGet]"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                dbhlper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0);
                dbhlper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ProductOrganiztionInfo> list = new List<ProductOrganiztionInfo>();
                        ProductOrganiztionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ProductOrganiztionInfo();
                            obj.OrganizationproductId = dr["OrganizationproductId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganizationproductId"]);
                            obj.ProductId = dr["ProductID"].ToString();
                            obj.ProductName = dr["ProductName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNSACCode = dr["HSNSACCode"].ToString();
                            obj.Name = dr["name"].ToString();
                            obj.Description = dr["Description"].ToString();
                            obj.SKU = dr["SKU"].ToString();
                            obj.Unit = dr["AbatementPercentage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Unit"]);
                            obj.Class = dr["Class"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Class"]);
                            obj.AbatementPercentage = dr["AbatementPercentage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AbatementPercentage"]);
                            obj.ServiceType = dr["ServiceType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ServiceType"]);
                            obj.SalePrice = dr["SalePrice"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SalePrice"]);
                            obj.isInclusiveTax = dr["isInclusiveTax"].ToString().Trim().ToUpper() == "Y";
                            obj.AvailableQty = dr["AvailableQty"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AvailableQty"]);
                            obj.IncomeAccount = dr["IncomeAccount"].ToString();
                            obj.SupplierId = dr["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SupplierId"]);
                            obj.PreferredSupplierId = dr["PreferredSupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PreferredSupplierId"]);
                            obj.ReverseCharge = dr["ReverseCharge"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ReverseCharge"]);
                            obj.PurchaseTax = dr["PurchaseTax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PurchaseTax"]);
                            obj.SaleTax = dr["SaleTax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SaleTax"]);
                            obj.isSelected = dr["isExist"].ToString().Trim().ToUpper() == "Y";
                            obj.FileData = dr["FileData"] == DBNull.Value ? null : (Byte[])dr["FileData"];
                            obj.FileName = dr["FileName"].ToString();
                            obj.FileType = dr["FileType"].ToString();
                            obj.ImageId = dr["ImageId"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public bool Save_ProductOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ProductId, Validations.ValueType.Integer, true, "Product Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CategoryId, Validations.ValueType.Integer, true, "Category Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.SKU, Validations.ValueType.AlphaNumericSpecialChar, true, "SKU", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Description, Validations.ValueType.AlphaNumericSpecialChar, true, "Descripition", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductSave]", true))
            {
                dbhlper.AddPparameter("@OrganizationproductId", obj.OrganizationproductId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@CategoryId", obj.CategoryId.Trim().Length > 0 ? Convert.ToInt32(obj.CategoryId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@OrganizationCode", obj.OrganizationCode.Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Name", obj.Name, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Description", obj.Description, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@SKU", obj.SKU, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Unit", obj.Unit, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Class", obj.Class, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@AbatementPercentage", obj.AbatementPercentage, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@ServiceType", obj.ServiceType, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@SalePrice", obj.SalePrice, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@isInclusiveTax", obj.isInclusiveTax ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AvailableQty", obj.AvailableQty, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@IncomeAccount", obj.IncomeAccount, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@SupplierId", obj.SupplierId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@PreferredSupplierId", obj.PreferredSupplierId, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@ReverseCharge", obj.ReverseCharge, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@PurchaseTax", obj.PurchaseTax, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@SaleTax", obj.SaleTax, DBHelper.param_types.Numeric);
                dbhlper.AddPparameter("@IsActive", obj.IsActive.Trim().ToUpper(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public GSTInfo Get_Gst(string ProductId, string ShipStateId, string OrganizationCode)
        {
            using (DBHelper dbhlper = new DBHelper("spGet_Gst"))
            {
                GSTInfo obj = new GSTInfo();
                obj.Percentage = "0";
                obj.SalePrice = "0";

                dbhlper.AddPparameter("@ProductId", ProductId.Trim().Length > 0 ? Convert.ToInt32(ProductId) : 0);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@ShipStateID", ShipStateId.Trim().Length > 0 ? Convert.ToInt32(ShipStateId) : 0);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Percentage = ds.Tables[0].Rows[0]["Percentage"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["Percentage"].ToString();
                        obj.SalePrice = ds.Tables[0].Rows[0]["SalePrice"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["SalePrice"].ToString();
                    }
                }

                return obj;
            }
        }

        public bool Save_ProductOrganizationImage(bool isOnlyDelete, ProductOrganiztionImageInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (isOnlyDelete)
            {
                if (obj.ImageId < 1) { errormsg = "Image identity should come"; return false; }
            }
            else
            {
                if (!Validations.ValidateDataType(obj.FileName, Validations.ValueType.AlphaNumericSpecialChar, false, "File Name", out errormsg)) { return false; }
                if (!Validations.ValidateDataType(obj.FileType, Validations.ValueType.AlphaNumericSpecialChar, true, "File Type", out errormsg)) { return false; }
            }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductImageSave]", true))
            {
                dbhlper.AddPparameter("@ImageId", obj.ImageId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@OrganizationproductId", obj.OrganizationproductId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@FileData", obj.FileData == null ? new byte[] { } : obj.FileData, DBHelper.param_types.Image);
                dbhlper.AddPparameter("@FileName", obj.FileName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@FileType", obj.FileType, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Seq", obj.Seq, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsMain", obj.Ismain.Trim().ToUpper(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", obj.IsActive.Trim().ToUpper(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public List<ProductOrganiztionImageInfo> GetList_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductImageGet]"))
            {
                dbhlper.AddPparameter("@ImageId", ImageId);
                dbhlper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                dbhlper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                dbhlper.AddPparameter("@IsActive", IsActive);
                dbhlper.AddPparameter("@IsMain", IsMain);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ProductOrganiztionImageInfo> list = new List<ProductOrganiztionImageInfo>();
                        ProductOrganiztionImageInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ProductOrganiztionImageInfo();
                            obj.ImageId = dr["ImageId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ImageId"]);
                            obj.OrganizationproductId = dr["OrganizationproductId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganizationproductId"]);
                            obj.ProductId = dr["ProductID"].ToString();
                            obj.FileData = dr["FileData"] == DBNull.Value ? new Byte[] { } : (byte[])dr["FileData"];
                            obj.FileName = dr["FileName"].ToString();
                            obj.FileType = dr["FileType"].ToString();
                            obj.Seq = dr["SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SEQ"]);
                            obj.Ismain = dr["isMain"].ToString().Trim().ToUpper();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public ProductOrganiztionImageInfo GetDetails_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            var list = GetList_ProductOrganizationImage(ImageId, OrganizationproductId, ProductID, IsActive, IsMain);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            ProductOrganiztionImageInfo objProductOrganiztionImageInfo = new ProductOrganiztionImageInfo();
            objProductOrganiztionImageInfo.ImageId = 0;
            objProductOrganiztionImageInfo.IsActive = "";
            objProductOrganiztionImageInfo.Ismain = "";
            objProductOrganiztionImageInfo.OrganizationproductId = 0;
            return objProductOrganiztionImageInfo;
        }
        #endregion

        #region ServiceClass Related
        public List<ServiceClassInfo> GetList_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceClassGet]"))
            {
                dbhlper.AddPparameter("@ServiceClassId", ServiceClassID);
                dbhlper.AddPparameter("@ServiceClassName", ServiceClassName);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceClassInfo> list = new List<ServiceClassInfo>();
                        ServiceClassInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceClassInfo();
                            obj.ServiceClassId = dr["ServiceClassID"].ToString();
                            obj.ServiceClassName = dr["ServiceClassName"].ToString();
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

        public ServiceClassInfo GetDetails_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceClassID))
            {
                List<ServiceClassInfo> list = GetList_ServiceClass(ServiceClassID, ServiceClassName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceClassInfo();
        }

        public bool Save_ServiceClass(bool isOnlyDelete, ServiceClassInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceClassId, Validations.ValueType.Integer, true, "ServiceClass Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceClassName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceClass Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceClassSave]", true))
            {
                dbhlper.AddPparameter("@ServiceClassId", obj.ServiceClassId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ServiceClassName", obj.ServiceClassName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region ServiceType Related
        public List<ServiceTypeInfo> GetList_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceTypeGet]"))
            {
                dbhlper.AddPparameter("@ServiceTypeId", ServiceTypeID);
                dbhlper.AddPparameter("@ServiceTypeName", ServiceTypeName);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceTypeInfo> list = new List<ServiceTypeInfo>();
                        ServiceTypeInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceTypeInfo();
                            obj.ServiceTypeId = dr["ServiceTypeID"].ToString();
                            obj.ServiceTypeName = dr["ServiceTypeName"].ToString();
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

        public ServiceTypeInfo GetDetails_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceTypeID))
            {
                List<ServiceTypeInfo> list = GetList_ServiceType(ServiceTypeID, ServiceTypeName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceTypeInfo();
        }

        public bool Save_ServiceType(bool isOnlyDelete, ServiceTypeInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceTypeId, Validations.ValueType.Integer, true, "ServiceType Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceTypeName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceType Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceTypeSave]", true))
            {
                dbhlper.AddPparameter("@ServiceTypeId", obj.ServiceTypeId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ServiceTypeName", obj.ServiceTypeName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region ServiceUnit Related
        public List<ServiceUnitInfo> GetList_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceUnitGet]"))
            {
                dbhlper.AddPparameter("@ServiceUnitId", ServiceUnitID);
                dbhlper.AddPparameter("@ServiceUnitName", ServiceUnitName);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceUnitInfo> list = new List<ServiceUnitInfo>();
                        ServiceUnitInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceUnitInfo();
                            obj.ServiceUnitId = dr["ServiceUnitID"].ToString();
                            obj.ServiceUnitName = dr["ServiceUnitName"].ToString();
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

        public ServiceUnitInfo GetDetails_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceUnitID))
            {
                List<ServiceUnitInfo> list = GetList_ServiceUnit(ServiceUnitID, ServiceUnitName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceUnitInfo();
        }

        public bool Save_ServiceUnit(bool isOnlyDelete, ServiceUnitInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceUnitId, Validations.ValueType.Integer, true, "ServiceUnit Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceUnitName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceUnit Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceUnitSave]", true))
            {
                dbhlper.AddPparameter("@ServiceUnitId", obj.ServiceUnitId, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ServiceUnitName", obj.ServiceUnitName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Settings Related
        public List<SettingsInfo> GetList_Settings(string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spSettingsGet]"))
            {
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
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

        public SettingsInfo GetDetails_Settings(string OrganizationCode, bool IsActive)
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

        public bool Save_Settings(bool isOnlyDelete, SettingsInfo objSettingsInfo, UserInfo objUserInfo, out string errormsg)
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
                dbhlper.AddPparameter("@OrganizationCode", objSettingsInfo.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@infotype", objSettingsInfo.InfoType, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@mc_isAllowedMutyCurrency", objSettingsInfo.mc_isAllowedMutyCurrency ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@mc_CurrencyList", objSettingsInfo.mc_CurrencyList, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_isAllowedOnlinePayment", objSettingsInfo.p_isAllowedOnlinePayment ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountNumber", objSettingsInfo.p_BankAccountNumber, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountHolder", objSettingsInfo.p_BankAccountHolder, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountIFSCCode", objSettingsInfo.p_BankAccountIFSCCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountIMCRCode", objSettingsInfo.p_BankAccountIMCRCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountIBranchName", objSettingsInfo.p_BankAccountIBranchName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_BankAccountIBankName", objSettingsInfo.p_BankAccountIBankName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@p_PaypalAccountID", objSettingsInfo.p_PaypalAccountID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@c_isAllowedMultyLanguage", objSettingsInfo.c_isAllowedMultyLanguage ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@an_isAllowedAlert_GSTDate", objSettingsInfo.an_isAllowedAlert_GSTDate ? "Y" : "N", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@an_isAllowedAlert_PaidMembership", objSettingsInfo.an_isAllowedAlert_PaidMembership, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@an_AlertText_GSTDate", objSettingsInfo.an_AlertText_GSTDate, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@an_AlertText_PaidMembership", objSettingsInfo.an_AlertText_PaidMembership, DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@CompanyName", objSettingsInfo.c_CompanyName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Email", objSettingsInfo.c_Email, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Mobile", objSettingsInfo.c_Mobile, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Address", objSettingsInfo.c_Address, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", objSettingsInfo.c_City, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", objSettingsInfo.c_State, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Country", objSettingsInfo.c_Country, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Website", objSettingsInfo.c_Website, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CIN", objSettingsInfo.c_CIN, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PAN", objSettingsInfo.c_PAN, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DefaultEmail", objSettingsInfo.c_DefaultEmail, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@SMTP", objSettingsInfo.an_AlertText_PaidMembership, DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Static Value Related
        public List<StaticValuInfo> GetList_StaticValue(string Key)
        {
            List<StaticValuInfo> list = new List<StaticValuInfo>();
            using (DBHelper dbhlper = new DBHelper("GetStaticValueList"))
            {
                dbhlper.AddPparameter("@Key", Key);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        StaticValuInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new StaticValuInfo();
                            obj.Id = dr["ID"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.Key = dr["Key"].ToString();
                            obj.Value = dr["Value"].ToString();
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);

                            list.Add(obj);
                        }
                    }
                }
            }

            return list;
        }
        #endregion

        #region Terms Related
        public List<TermsInfo> GetList_Terms(string TermsID, string OrganisationCode)
        {
            List<TermsInfo> list = new List<TermsInfo>();

            using (DBHelper dbhlper = new DBHelper("spMSTTermsGet"))
            {
                dbhlper.AddPparameter("@ID", string.IsNullOrEmpty(TermsID) ? "0" : TermsID);
                dbhlper.AddPparameter("@OrganizationCode", string.IsNullOrEmpty(OrganisationCode) ? "" : OrganisationCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        TermsInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new TermsInfo();
                            obj.Id = dr["ID"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.Name = dr["Name"].ToString();
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);

                            list.Add(obj);
                        }
                    }

                }
            }

            return list;
        }

        public TermsInfo GetDetails_Terms(string TermsID, string OrganisationCode)
        {
            if (!string.IsNullOrEmpty(TermsID))
            {
                List<TermsInfo> list = GetList_Terms(TermsID, OrganisationCode);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new TermsInfo();
        }

        public bool Save_Terms(bool isOnlyDelete, TermsInfo TermsInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(TermsInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.Id, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Title", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.DueInCertainDayOfMonth.ToString(), Validations.ValueType.Integer, true, "Due in certain day of month", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.DueInFixedNumberDays.ToString(), Validations.ValueType.Integer, true, "Due in fixed number days", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.DueInNextMonth.ToString(), Validations.ValueType.Integer, true, "Due in next month", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(TermsInfo.Discount.ToString(), Validations.ValueType.Numeric, true, "Discount", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTTermsSave]", true))
            {
                dbhlper.AddPparameter("@Id", (TermsInfo.Id == "" ? "0" : TermsInfo.Id), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Name", HttpUtility.HtmlEncode(TermsInfo.Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(TermsInfo.OrganizationCode), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DueInFixedNumberDays", HttpUtility.HtmlEncode(TermsInfo.DueInFixedNumberDays), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DueInCertainDayOfMonth", HttpUtility.HtmlEncode(TermsInfo.DueInCertainDayOfMonth), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DueInNextMonth", HttpUtility.HtmlEncode(TermsInfo.DueInNextMonth), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Discount", HttpUtility.HtmlEncode(TermsInfo.Discount), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@IsActive", TermsInfo.IsActive, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", NewDatauniqueID, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }

        }
        #endregion

        #region User Activity Related 
        //public bool UserRegistration(string Company, string EmailID, string Mobile, string Password, out string errormsg)
        //{
        //    errormsg = "";

        //    using (DBHelper dbhlper = new DBHelper("spUserRegisteredRegistration", true))
        //    {
        //        dbhlper.AddPparameter("@UserName", Company, DBHelper.param_types.Varchar);
        //        dbhlper.AddPparameter("@EmailID", EmailID, DBHelper.param_types.Varchar);
        //        dbhlper.AddPparameter("@Mobile", Mobile, DBHelper.param_types.Varchar);
        //        dbhlper.AddPparameter("@Password", Password, DBHelper.param_types.Varchar);
        //        dbhlper.AddPparameter("@OrganizationName", Company, DBHelper.param_types.Varchar);
        //        dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
        //        dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

        //        return dbhlper.Execute_NonQuery(out errormsg);
        //    }
        //}

        public UserInfo UserLogin(string UserID, string Password)
        {
            using (DBHelper dbhlper = new DBHelper("spLoginUser"))
            {
                dbhlper.AddPparameter("@UserID", UserID);
                dbhlper.AddPparameter("@Password", Password);

                using (DataSet ds = dbhlper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UserInfo obj = new UserInfo();
                        obj.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                        obj.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                        obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                        obj.ManagerID = ds.Tables[0].Rows[0]["ManagerID"].ToString();
                        obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                        obj.ManagerName = ds.Tables[0].Rows[0]["ManagerName"].ToString();
                        obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                        obj.AccessAllowed = ds.Tables[0].Rows[0]["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
                        obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                        obj.IsMainUser = ds.Tables[0].Rows[0]["IsMainUser"].ToString().Trim().ToUpper() == "Y";
                        obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().ToString().Trim().ToUpper() == "Y";

                        return obj;
                    }

                    return null;
                }
            }
        }

        public bool UserProfileUpdate(string UserID, string First_Name, string Last_Name, string Mobile, string EmailID, string Street1, string Street2, string City, string State, string Country, string BranchName, out string NewDatauniqueID, out string errormsg)
        {
            errormsg = "";
            NewDatauniqueID = "";
            flag = false;

            using (DBHelper dbhlper = new DBHelper("spUserRegisteredProfileUpdate", true))
            {
                dbhlper.AddPparameter("@UserID", UserID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@FirstName", First_Name, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LastName", Last_Name, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Mobile", Mobile, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@EmailID", EmailID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street1", Street1, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street2", Street2, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", City, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", State, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Country", Country, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BranchName", BranchName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        //#region UserRegistered Related
        //public List<UserInfo> GetList_UserRegistered(string UserID, string EmailID, string Mobile, string City, string State, string Country, string BranchId, string OrganizationCode, bool IsActive, string LanguageId)
        //{
        //    using (DBHelper dbhlper = new DBHelper("spUserRegisteredGet"))
        //    {
        //        dbhlper.AddPparameter("@UserID", UserID);
        //        dbhlper.AddPparameter("@EmailID", EmailID);
        //        dbhlper.AddPparameter("@Mobile", Mobile);
        //        dbhlper.AddPparameter("@City", City);
        //        dbhlper.AddPparameter("@State", State);
        //        dbhlper.AddPparameter("@Country", Country);
        //        dbhlper.AddPparameter("@BranchId", BranchId);
        //        dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
        //        dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
        //        dbhlper.AddPparameter("@LanguageId", LanguageId);

        //        using (DataSet ds = dbhlper.Execute_Query())
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                List<UserInfo> list = new List<UserInfo>();
        //                UserInfo obj = null;

        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    obj = new UserInfo();
        //                    obj.UserID = dr["UserID"].ToString();
        //                    obj.UserName = dr["UserName"].ToString();
        //                    obj.Title = dr["FirstName"].ToString();
        //                    obj.First_Name = dr["FirstName"].ToString();
        //                    obj.Middle_Name = dr["FirstName"].ToString();
        //                    obj.Last_Name = dr["LastName"].ToString();
        //                    obj.DOB = dr["DOB"].ToString();
        //                    obj.Sex = dr["Sex"].ToString();
        //                    obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
        //                    obj.IsApproved = dr["IsApproved"].ToString().Trim().ToUpper() == "Y";
        //                    obj.IsMainUser = dr["IsMainUser"].ToString().Trim().ToUpper() == "Y";
        //                    obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
        //                    obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
        //                    obj.ActivityType = dr["ActivityType"].ToString();
        //                    obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
        //                    obj.DatauniqueID = dr["DatauniqueID"].ToString();

        //                    obj.UserRegisteredMobile = new UserMobileInfo();
        //                    obj.UserRegisteredEmail = new UserEmailInfo();

        //                    obj.UserRegisteredMobile.Mobile = dr["Mobile"].ToString();
        //                    obj.UserRegisteredEmail.EmailID = dr["EmailID"].ToString();

        //                    list.Add(obj);
        //                }

        //                return list;
        //            }
        //        }

        //        return null;
        //    }
        //}

        ////public UserInfo GetDetails_UserRegistered(string UserID, string EmailID, string Mobile, string City, string State, string Country, string BranchId, string OrganizationCode, bool IsActive, string LanguageId)
        ////{
        ////    List<UserInfo> list = GetList_UserRegistered(UserID, EmailID, Mobile, City, State, Country, BranchId, OrganizationCode, IsActive, LanguageId);

        ////    if (list != null && list.Count() > 0)
        ////    {
        ////        return list[0];
        ////    }

        ////    return new UserInfo();
        ////}
        //#endregion

        #region UserModerate Related
        public List<UserInfo> GetList_UserModerate(string OrganizationCode, string UserCode, string UserType)
        {
            using (DBHelper dbhlper = new DBHelper("spUrMstUserGet"))
            {
                dbhlper.AddPparameter("@UserType", UserType);
                dbhlper.AddPparameter("@UserCode", UserCode);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new UserInfo();
                            obj.UserType = dr["UserType"].ToString();
                            obj.UserTypeName = dr["UserTypeName"].ToString();
                            obj.DisplayName = dr["DisplayName"].ToString();
                            obj.UserCode = dr["UserCode"].ToString();
                            obj.First_Name = dr["FirstName"].ToString();
                            obj.Middle_Name = dr["MiddleName"].ToString();
                            obj.Last_Name = dr["LastName"].ToString();
                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim() == "Y";
                            obj.Sex = dr["Sex"].ToString();
                            obj.Title = dr["Title"].ToString();
                            obj.Safix = dr["Safix"].ToString();
                            obj.DOB = dr["DOB"].ToString().Length > 0 ? Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";

                            obj.EmailID = dr["EmailID"].ToString();
                            obj.UserRegisteredEmail = new UserEmailInfo();
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredEmail.EmailConfirmationSentOn = Convert.ToDateTime(dr["EmailConfirmationSentOn"]).ToString().Replace("/", "-");
                            }
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredEmail.EmailVerifiedOn = Convert.ToDateTime(dr["EmailVerifiedOn"]).ToString().Replace("/", "-");
                            }
                            obj.UserRegisteredEmail.IsActive = dr["IsEmailActive"].ToString().Trim() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = dr["IsEmailConfirmationSend"].ToString() == "Y";
                            obj.UserRegisteredEmail.IsEmailVerified = dr["IsEmailVerified"].ToString() == "Y";

                            obj.MobileNumber = dr["Mobile"].ToString();
                            obj.UserRegisteredMobile = new UserMobileInfo();
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredMobile.MobileConfirmationSentOn = Convert.ToDateTime(dr["MobileConfirmationSentOn"]).ToString().Replace("/", "-");
                            }
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredMobile.MobileVerifiedOn = dr["MobileVerifiedOn"].ToString();
                            }
                            obj.UserRegisteredMobile.IsActive = dr["IsMobileActive"].ToString() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = dr["IsMobileConfirmationSent"].ToString() == "Y";
                            obj.UserRegisteredMobile.IsMobileVerified = dr["IsMobileVerified"].ToString() == "Y";

                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();

                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
                            obj.IsActive = dr["IsActive"].ToString().Trim() == "Y";
                            obj.IsApproved = dr["IsApproved"].ToString().Trim() == "Y";
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                                UserBranchRoleMappingInfo newChildObj = null;

                                foreach (DataRow dr1 in ds.Tables[1].Rows)
                                {
                                    newChildObj = new UserBranchRoleMappingInfo();

                                    newChildObj.BranchID = dr1["BranchID"].ToString();
                                    newChildObj.BranchName = dr1["BranchName"].ToString();
                                    newChildObj.FunctionID = dr1["FunctionId"].ToString();
                                    newChildObj.FunctionName = dr1["FunctionName"].ToString();
                                    newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                                    newChildObj.OrganizationCode = dr1["OrganizationCode"].ToString();
                                    newChildObj.OrganizationName = dr1["OrganizationName"].ToString();

                                    obj.UserBranchRoleList.Add(newChildObj);
                                }

                                obj.OrganizationCode = obj.UserBranchRoleList[0].OrganizationCode;
                                obj.OrganizationName = obj.UserBranchRoleList[0].OrganizationName;
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                    else if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = new UserInfo();

                        obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                        UserBranchRoleMappingInfo newChildObj = null;

                        foreach (DataRow dr1 in ds.Tables[1].Rows)
                        {
                            newChildObj = new UserBranchRoleMappingInfo();

                            //newChildObj.BranchID = dr1["BranchID"] != DBNull.Value ? dr1["BranchID"].ToString() : "0";
                            //newChildObj.BranchName = dr1["BranchName"] != DBNull.Value ? dr1["BranchName"].ToString() : "";
                            newChildObj.FunctionID = dr1["FunctionId"] != DBNull.Value ? dr1["FunctionId"].ToString() : "0";
                            newChildObj.FunctionName = dr1["FunctionName"] != DBNull.Value ? dr1["FunctionName"].ToString() : "";
                            newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                            //newChildObj.OrganizationCode = dr1["OrganizationCode"] != DBNull.Value ? dr1["OrganizationCode"].ToString() : "";
                            //newChildObj.OrganizationName = dr1["OrganizationName"] != DBNull.Value ? dr1["OrganizationName"].ToString() : "";

                            obj.UserBranchRoleList.Add(newChildObj);
                        }

                        list.Add(obj);

                        return list;
                    }
                }
            }

            return null;
        }

        public UserInfo GetDetails_UserModerate(string UserCode)
        {
            List<UserInfo> list = GetList_UserModerate("", string.IsNullOrEmpty(UserCode) ? "<NEW>" : UserCode.Trim(), "");

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new UserInfo();
        }

        /// <summary>
        /// Registered User Registration
        /// </summary>
        /// <param name="isOnlyDelete"></param>
        /// <param name="objUserInfoToBeSaved"></param>
        /// <param name="objUserInfo"></param>
        /// <param name="functions"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool Save_UserModerate(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, UserInfo objUserInfo, string functions, out string errormsg)
        {
            errormsg = "";
            flag = false;
            string accountkey = Guid.NewGuid().ToString() + System.DateTime.Now.ToString();

            #region Validations
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserID, Validations.ValueType.Integer, true, "User ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserType, Validations.ValueType.PairValueAEMR, false, "User Type", out errormsg)) { return false; }

            if (objUserInfoToBeSaved.UserType.Trim().ToUpper() == "R"
                || objUserInfoToBeSaved.UserType.Trim().ToUpper() == "E"
                || objUserInfoToBeSaved.UserType.Trim().ToUpper() == "A")
            {
                if (!Validations.ValidateDataType(objUserInfoToBeSaved.OrganizationName, Validations.ValueType.AlphaNumericSpecialChar, (objUserInfoToBeSaved.UserType.Trim().ToUpper() == "A"), "Organization name", out errormsg)) { return false; }
            }

            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Title, Validations.ValueType.Integer, true, "Title", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Safix, Validations.ValueType.Integer, true, "Safix", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.First_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "First name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Middle_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Last name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Last_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Middle name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Sex, Validations.ValueType.Alphabet, true, "Sex", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.DOB, Validations.ValueType.DateTime, true, "DOB", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objUserInfoToBeSaved.EmailID, Validations.ValueType.Email, false, "EmailID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.MobileNumber, Validations.ValueType.MobileNumber, false, "Mobile number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.State, Validations.ValueType.Integer, true, "State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Country, Validations.ValueType.Integer, true, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Password, Validations.ValueType.Password, true, "Password", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Functions, Validations.ValueType.AlphaNumericSpecialChar, true, "Functions", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Notes, Validations.ValueType.AlphaNumericSpecialChar, true, "Notes", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[SPURMSTUserRegistration]", true))
            {
                #region MyRegion
                dbhlper.AddPparameter("@IsOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfoToBeSaved.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserType", HttpUtility.HtmlEncode(objUserInfoToBeSaved.UserType), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Title", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Title), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Safix), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.First_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Middle_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Last_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objUserInfoToBeSaved.DOB), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Sex), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AccessAllowed", (objUserInfoToBeSaved.AccessAllowed ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsApproved", (objUserInfoToBeSaved.IsApproved ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DONEBY", objUserInfo != null && objUserInfo.UserCode != null ? objUserInfo.UserCode : "", DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Notes), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@Mobile", objUserInfoToBeSaved.MobileNumber, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsMobileActive", HttpUtility.HtmlEncode(objUserInfoToBeSaved.IsMobileActive ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@EmailID", objUserInfoToBeSaved.EmailID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@EmailVerificationKey", accountkey, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsEmailActive", objUserInfoToBeSaved.IsEmailActive ? "Y" : "", DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@Password", objUserInfoToBeSaved.Password, DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", HttpUtility.HtmlEncode(objUserInfoToBeSaved.City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", (objUserInfoToBeSaved.State == "" ? "0" : objUserInfoToBeSaved.State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Country", (objUserInfoToBeSaved.Country == "" ? "0" : objUserInfoToBeSaved.Country), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@PIN", objUserInfoToBeSaved.PIN, DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@OrganizationCode1", objUserInfoToBeSaved.OrganizationCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OrganizationName", objUserInfoToBeSaved.OrganizationName, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Functions", functions, DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@NewUserCode", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@BranchId", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@OrganizationCode", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
                #endregion

                //if (dbhlper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg))
                if (dbhlper.Execute_NonQuery(out errormsg))
                {
                    NameValueCollection appsettings = System.Configuration.ConfigurationManager.AppSettings;
                    System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();

                    sbHtml.AppendLine("<table>");
                    sbHtml.AppendLine("  <tr><td>Hi " + objUserInfoToBeSaved.First_Name + "</td></tr>");
                    sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                    sbHtml.AppendLine("  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>");
                    sbHtml.AppendLine("  <tr><td>Please click on the <a href='" + System.Configuration.ConfigurationManager.AppSettings["WebURL"].ToString() + "activateregaccount?accountkey=" + accountkey + "'>link</a> to activate your account</td></tr>");
                    sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                    sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                    sbHtml.AppendLine("  <tr><td>Regards</td></tr>");
                    sbHtml.AppendLine("  <tr><td><a href='" + appsettings["WebURL"].ToString() + "'>Big page</a></td></tr>");
                    sbHtml.AppendLine("</table>");

                    new MailHelper("EmailConfirmationOnRegistration", "Email verification for your BigPage account", sbHtml.ToString(), appsettings["WebSenderEmailID"].ToString(), appsettings["WebSenderEmailDisplayName"].ToString(), objUserInfoToBeSaved.EmailID, objUserInfoToBeSaved.First_Name + " " + objUserInfoToBeSaved.Last_Name, "", "");

                    //string NewUserCode = dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 5).ToString();
                    //int OrganizationId = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count-4));
                    //int BranchId = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 3));
                    //int DatauniqueID = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 2));

                    //dbhlper.SetSQLCommandText("[spUrMstUserRegisteredFunctionMappingSave]");
                    //foreach (UserBranchRoleMappingInfo obj in MappingList)
                    //{
                    //    dbhlper.ClearParameters();
                    //    dbhlper.AddPparameter("@UserId", NewUserCode, DBHelper.param_types.Varchar);
                    //    dbhlper.AddPparameter("@DatauniqueID", DatauniqueID, DBHelper.param_types.BigInt);
                    //    dbhlper.AddPparameter("@FunctionId", obj.FunctionID, DBHelper.param_types.BigInt);
                    //    dbhlper.AddPparameter("@BranchId", BranchId, DBHelper.param_types.BigInt);
                    //    dbhlper.AddPparameter("@OrganizationId", OrganizationId, DBHelper.param_types.BigInt);
                    //    dbhlper.AddPparameter("@DoneBy", objUserInfo.UserID, DBHelper.param_types.BigInt);
                    //    dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                    //    if (!dbhlper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg))
                    //    {
                    //        return false;
                    //    }
                    //}

                    //dbhlper.CloseConnection(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Email_Verification(string EmailVerifictionKey, out string errormsg)
        {
            errormsg = "";
            flag = false;

            using (DBHelper dbhlper = new DBHelper("[SPURMSTEmailVerification]", true))
            {
                #region MyRegion
                dbhlper.AddPparameter("@EmailVerification", HttpUtility.HtmlEncode(EmailVerifictionKey), DBHelper.param_types.Varchar);
                #endregion

                if (dbhlper.Execute_NonQuery(out errormsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public UserInfo ModerateUserLogin(string UserID, string Password)
        //{
        //    using (DBHelper dbhlper = new DBHelper("spLoginModerate"))
        //    {
        //        dbhlper.AddPparameter("@UserID", UserID);
        //        dbhlper.AddPparameter("@Password", Password);

        //        using (DataSet ds = dbhlper.Execute_Query())
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                UserInfo obj = new UserInfo();
        //                obj.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
        //                obj.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
        //                obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
        //                obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
        //                obj.AccessAllowed = ds.Tables[0].Rows[0]["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
        //                obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
        //                obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().ToString().Trim().ToUpper() == "Y";

        //                return obj;
        //            }

        //            return null;
        //        }
        //    }
        //}
        #endregion

        #region Customer Related
        public byte[] GetExcel_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            List<CustomerInfo> list = GetList_Customer(UserType, CusID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);

            if (list != null && list.Count() > 0)
            {
                ExcelPackage pck = new ExcelPackage();
                var ws = pck.Workbook.Worksheets.Add("CustomerList_AsOf_" + System.DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("-", "/"));

                int col = 1;
                int row = 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "First Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Middle Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Last Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Company"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "DOB"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Sex"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Parent Customer"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "EmailID"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Mobile"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Country"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "GST Registration Type"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "GSTIN"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Tax Reg No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "CST Reg No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "PAN No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Terms"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Preffered Payment Method"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Preffered Delivery Method"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Notes"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Opening Balance"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "As Of date"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Country"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Country"; col = col + 1;

                foreach (CustomerInfo obj in list)
                {
                    row = row + 1;
                    col = 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.First_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Middle_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Last_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Company_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.DOB; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Sex; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.ParentCusName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.EmailID; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Mobile; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.State; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Country; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.GSTRegistrationTypeName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.GSTIN; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.TaxRegNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.CSTRegNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PANNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Terms; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PrefferedPaymentMethodName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PrefferedDeliveryMethodName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Notes; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.OpeningBalance; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.AsOfDate; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_StateName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_CountryName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_StateName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_CountryName; col = col + 1;
                }

                return pck.GetAsByteArray();
            }

            return null;
        }

        public List<CustomerInfo> GetList_CustomerDropdownlist(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spCusMstCustomerGet"))
            {
                dbhlper.AddPparameter("@Mode", 1);
                dbhlper.AddPparameter("@UserType", UserType);
                dbhlper.AddPparameter("@CusID", CusID);
                dbhlper.AddPparameter("@BranchId", BranchId);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@UserID", UserID);
                dbhlper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CustomerInfo> list = new List<CustomerInfo>();
                        CustomerInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CustomerInfo();
                            obj.CusID = dr["CusID"].ToString();
                            obj.DisplayName = dr["DisplayName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public List<CustomerInfo> GetList_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spCusMstCustomerGet"))
            {
                dbhlper.AddPparameter("@Mode", 0);
                dbhlper.AddPparameter("@UserType", UserType);
                dbhlper.AddPparameter("@CusID", CusID);
                dbhlper.AddPparameter("@BranchId", BranchId);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@UserID", UserID);
                dbhlper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CustomerInfo> list = new List<CustomerInfo>();
                        CustomerInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CustomerInfo();
                            obj.CusID = dr["CusID"].ToString();
                            obj.DisplayName = dr["DisplayName"].ToString();
                            obj.First_Name = dr["FirstName"].ToString();
                            obj.Title = dr["Title"].ToString();
                            obj.Safix = dr["Safix"].ToString();
                            obj.Middle_Name = dr["MiddleName"].ToString();
                            obj.Last_Name = dr["LastName"].ToString();
                            obj.DOB = dr["DOB"].ToString().Length > 0 ? Convert.ToDateTime(dr["DOB"].ToString()).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                            obj.Sex = dr["Sex"].ToString();
                            obj.ParentCusID = dr["ParentCusID"].ToString();
                            obj.IsSubCustomer = dr["ParentCusID"].ToString().Trim().Length > 0;
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";

                            //Each table holds different valies for the same fields
                            //So, we are not collecting those data for now

                            //obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            //obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            //obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            //obj.ActivityType = dr["ActivityType"].ToString();

                            obj.EmailID = dr["EmailID"].ToString();
                            obj.Mobile = dr["Mobile"].ToString();
                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.Website = dr["Website"].ToString();

                            obj.GSTRegistrationType = dr["GSTRegistrationType"].ToString();
                            obj.GSTRegistrationTypeName = dr["GSTRegistrationTypeName"].ToString();
                            obj.GSTIN = dr["GSTIN"].ToString();
                            obj.TaxRegNo = dr["TaxRegNo"].ToString();
                            obj.CSTRegNo = dr["CSTRegNo"].ToString();
                            obj.PANNo = dr["PANNo"].ToString();
                            obj.Terms = dr["Terms"].ToString();
                            obj.TermsName = dr["TermsName"].ToString();

                            obj.PrefferedPaymentMethod = dr["PrefferedPaymentMethod"].ToString();
                            obj.PrefferedPaymentMethodName = dr["PrefferedPaymentMethodName"].ToString();
                            obj.PrefferedDeliveryMethod = dr["PrefferedDeliveryMethod"].ToString();
                            obj.PrefferedDeliveryMethodName = dr["PrefferedDeliveryMethodName"].ToString();

                            obj.AsOfDate = dr["AsOfDate"].ToString().Length > 0 ? Convert.ToDateTime(dr["AsOfDate"].ToString()).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                            obj.OpeningBalance = dr["OpeningBalance"].ToString();

                            obj.Shipping_Street1 = dr["ShippingStreet1"].ToString();
                            obj.Shipping_Street2 = dr["ShippingStreet2"].ToString();
                            obj.Shipping_City = dr["ShippingCity"].ToString();
                            obj.Shipping_State = dr["ShippingState"].ToString();
                            obj.Shipping_StateName = dr["ShippingStateName"].ToString();
                            obj.Shipping_Country = dr["ShippingCountry"].ToString();
                            obj.Shipping_CountryName = dr["ShippingCountryName"].ToString();

                            obj.Billing_Street1 = dr["BillingStreet1"].ToString();
                            obj.Billing_Street2 = dr["BillingStreet2"].ToString();
                            obj.Billing_City = dr["BillingCity"].ToString();
                            obj.Billing_State = dr["BillingState"].ToString();
                            obj.Billing_StateName = dr["BillingStateName"].ToString();
                            obj.Billing_Country = dr["BillingCountry"].ToString();
                            obj.Billing_CountryName = dr["BillingCountryName"].ToString();

                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public CustomerInfo GetDetails_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            if (!string.IsNullOrEmpty(CusID))
            {
                List<CustomerInfo> list = GetList_Customer(UserType, CusID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            //obj.ListStaticValuesAll = new wscalls().GetList_StaticValue("");
            //obj.ListTermsAll = new wscalls().GetList_Terms("", OrganizationCode);

            return new CustomerInfo();
        }

        public DataSet Upload_Customer(string UserType, bool isOvereWrite, DataSet ds, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";

            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return null; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Validation - Column existance
                if (ds.Tables[0].Columns.IndexOf("First Name") < 0) { errormsg = "[First Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Last Name") < 0) { errormsg = "[Last Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Company Name") < 0) { errormsg = "[Company Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Email") < 0) { errormsg = "[Email] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Mobile") < 0) { errormsg = "[Mobile] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("FAX") < 0) { errormsg = "[FAX] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Street") < 0) { errormsg = "[Street] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("City") < 0) { errormsg = "[City] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("State") < 0) { errormsg = "[State] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Opening Balance") < 0) { errormsg = "[Opening Balance] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("As Of Date") < 0) { errormsg = "[As Of Date] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("GST Registration Type") < 0) { errormsg = "[GST Registration Type] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("GSTIN Number") < 0) { errormsg = "[GSTIN Number] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Tax Reg No") < 0) { errormsg = "[Tax Reg.No.] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Cst Reg No") < 0) { errormsg = "[Cst Reg. No.] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("PAN Number") < 0) { errormsg = "[PAN Number] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Terms") < 0) { errormsg = "[Terms] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Effective Date") < 0) { errormsg = "[Effective Date] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Prefered Delivery Method") < 0) { errormsg = "[Prefered Delivery Method] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Prefered Paymant Method") < 0) { errormsg = "[Prefered Paymant Method] column is not available"; }

                else if (ds.Tables[0].Columns.IndexOf("Parent Customer") < 0) { errormsg = "[Parent Customer] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing Street1") < 0) { errormsg = "[Billing Street1] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing City") < 0) { errormsg = "[Billing City] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing State") < 0) { errormsg = "[Billing State] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing Country") < 0) { errormsg = "[Billing Country] column is not available"; }
                else
                {
                    ds.Tables[0].Columns.Add("Row Number");
                    ds.Tables[0].Columns.Add("Error Message");

                    bool DisplayErroeMessage = false;

                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr["First Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "First Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Middle Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Last Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Company Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Company Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Email"].ToString(), Validations.ValueType.Email, true, "Email", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Mobile"].ToString(), Validations.ValueType.MobileNumber, true, "Mobile", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["FAX"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "FAX", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Street"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Street", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Parent Customer"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Customer", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Opening Balance"].ToString(), Validations.ValueType.Numeric, true, "Opening Balance", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["As Of Date"].ToString(), Validations.ValueType.DateTime, true, "As Of Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["GST Registration Type"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GST Registration Type", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["GSTIN Number"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GSTIN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Tax Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Cst Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Cst Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["PAN Number"].ToString(), Validations.ValueType.AlphaNumeric, true, "PAN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Terms"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Terms", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Effective Date"].ToString(), Validations.ValueType.DateTime, true, "Effective Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Prefered Delivery Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Delivery Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Prefered Paymant Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Paymant Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing Street1"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Street1", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        #endregion
                    }
                    #endregion

                    if (!DisplayErroeMessage)
                    {
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Error Message"));
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Row Number"));

                        using (DBHelper dbhlper = new DBHelper("[spCUSMSTCustomerUpload]", true))
                        {
                            dbhlper.AddPparameter("@CustomerData", ds.Tables[0], DBHelper.param_types.Structured);
                            dbhlper.AddPparameter("@UserType", UserType.Trim().ToUpper(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            dbhlper.Execute_NonQuery(out errormsg);
                        }
                    }
                    else
                    {
                        errormsg = "Problem in excel data. Please rectify before upload";
                        return ds;
                    }
                }
            }

            return null;
        }

        public bool Save_Customer(string UserType, bool isOnlyDelete, CustomerInfo objCustomerInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;
            //string NewCusID = "";

            #region Validations
            if (!Validations.ValidateDataType(objCustomerInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.CusID, Validations.ValueType.Integer, true, "Customer ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Title, Validations.ValueType.Integer, true, "Title", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.First_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "First name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Middle_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Middle name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Last_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Last name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Safix, Validations.ValueType.AlphaNumericSpecialChar, true, "Safix", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Sex, Validations.ValueType.Alphabet, true, "Sex", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.DOB, Validations.ValueType.DateTime, true, "DOB", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objCustomerInfo.EmailID, Validations.ValueType.Email, true, "EmailID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Mobile, Validations.ValueType.MobileNumber, true, "Mobile number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.State, Validations.ValueType.Numeric, true, "State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Country, Validations.ValueType.Numeric, true, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Website, Validations.ValueType.AlphaNumericSpecialChar, true, "Website", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objCustomerInfo.GSTRegistrationType, Validations.ValueType.Numeric, true, "GST registration type", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.ParentCusID, Validations.ValueType.Numeric, true, "Parent customer id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.PrefferedPaymentMethod, Validations.ValueType.Numeric, true, "Preffered payment method", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.PrefferedDeliveryMethod, Validations.ValueType.Numeric, true, "Preffered delivery method", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.TaxRegNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Reg. No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.CSTRegNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Cst Reg. No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.PANNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Pan No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Terms, Validations.ValueType.Numeric, true, "Terms", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objCustomerInfo.OpeningBalance, Validations.ValueType.Numeric, true, "Opening balance", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.AsOfDate, Validations.ValueType.DateTime, true, "As of date ", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objCustomerInfo.Shipping_Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1 of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Shipping_Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2 of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Shipping_City, Validations.ValueType.AlphaNumericSpecialChar, true, "City of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Shipping_State, Validations.ValueType.Numeric, true, "State of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Shipping_Country, Validations.ValueType.Numeric, true, "Country of Shipping address", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objCustomerInfo.Billing_Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1 of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Billing_Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2 of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Billing_City, Validations.ValueType.AlphaNumericSpecialChar, true, "City of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Billing_State, Validations.ValueType.Numeric, true, "State of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCustomerInfo.Billing_Country, Validations.ValueType.Numeric, true, "Country of Billing address", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spCUSMSTCustomerSave]", true))
            {
                dbhlper.AddPparameter("@UserType", UserType, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CusID", (objCustomerInfo.CusID == "" ? "0" : objCustomerInfo.CusID), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Title", HttpUtility.HtmlEncode(objCustomerInfo.Title), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objCustomerInfo.First_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objCustomerInfo.Middle_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objCustomerInfo.Last_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objCustomerInfo.Safix), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objCustomerInfo.Sex), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objCustomerInfo.DOB), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(objCustomerInfo.OrganizationCode), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@EmailID", objCustomerInfo.EmailID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Mobile", objCustomerInfo.Mobile, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objCustomerInfo.Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objCustomerInfo.Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", HttpUtility.HtmlEncode(objCustomerInfo.City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", (objCustomerInfo.State == "" ? "0" : objCustomerInfo.State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Country", (objCustomerInfo.Country == "" ? "0" : objCustomerInfo.Country), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Website", HttpUtility.HtmlEncode(objCustomerInfo.Website), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@GSTRegistrationType", (objCustomerInfo.GSTRegistrationType == "" ? "0" : objCustomerInfo.GSTRegistrationType), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@GSTIN", HttpUtility.HtmlEncode(objCustomerInfo.GSTIN), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsSubCustomer", (objCustomerInfo.IsSubCustomer ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ParentCusID", (objCustomerInfo.ParentCusID == "" ? "0" : objCustomerInfo.ParentCusID), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@BillingWith", HttpUtility.HtmlEncode(objCustomerInfo.BillingWith), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objCustomerInfo.Notes), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PrefferedPaymentMethod", HttpUtility.HtmlEncode(objCustomerInfo.PrefferedPaymentMethod), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PrefferedDeliveryMethod", HttpUtility.HtmlEncode(objCustomerInfo.PrefferedDeliveryMethod), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@TaxRegNo", HttpUtility.HtmlEncode(objCustomerInfo.TaxRegNo), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CSTRegNo", objCustomerInfo.CSTRegNo, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PANNo", HttpUtility.HtmlEncode(objCustomerInfo.PANNo), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Terms", (objCustomerInfo.Terms == "" ? "0" : objCustomerInfo.ParentCusID), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@OpeningBalance", (objCustomerInfo.OpeningBalance == "" ? "0" : objCustomerInfo.OpeningBalance), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AsOfDate", Validations.ConvertToDateReturn_ddMMyyyy_blank(objCustomerInfo.AsOfDate), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@ShippingStreet1", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingStreet2", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingCity", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingState", (objCustomerInfo.Shipping_State == "" ? "0" : objCustomerInfo.Shipping_State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@ShippingCountry", (objCustomerInfo.Shipping_Country == "" ? "0" : objCustomerInfo.Shipping_Country), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@BillingStreet1", HttpUtility.HtmlEncode(objCustomerInfo.Billing_Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingStreet2", HttpUtility.HtmlEncode(objCustomerInfo.Billing_Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingCity", HttpUtility.HtmlEncode(objCustomerInfo.Billing_City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingState", (objCustomerInfo.Billing_State == "" ? "0" : objCustomerInfo.Billing_State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@BillingCountry", (objCustomerInfo.Billing_Country == "" ? "0" : objCustomerInfo.Billing_Country), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@IsActive", objCustomerInfo.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewCusID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Supplier Related
        public byte[] GetExcel_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            List<SupplierInfo> list = GetList_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);

            if (list != null && list.Count() > 0)
            {
                ExcelPackage pck = new ExcelPackage();
                var ws = pck.Workbook.Worksheets.Add("SupplierList_AsOf_" + System.DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("-", "/"));

                int col = 1;
                int row = 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "First Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Middle Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Last Name"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Company"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "DOB"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Sex"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Parent Supplier"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "EmailID"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Mobile"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Country"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "GST Registration Type"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "GSTIN"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Tax Reg No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "CST Reg No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "PAN No"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Terms"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Preffered Payment Method"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Preffered Delivery Method"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Notes"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Opening Balance"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "As Of date"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Billing Country"; col = col + 1;

                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Street1"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Street2"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping City"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping State"; col = col + 1;
                pck.Workbook.Worksheets[0].Cells[row, col].Value = "Shipping Country"; col = col + 1;

                foreach (SupplierInfo obj in list)
                {
                    row = row + 1;
                    col = 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.First_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Middle_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Last_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Company_Name; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.DOB; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Sex; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.ParentSupName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.EmailID; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Mobile; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.State; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Country; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.GSTRegistrationTypeName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.GSTIN; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.TaxRegNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.CSTRegNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PANNo; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Terms; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PrefferedPaymentMethodName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.PrefferedDeliveryMethodName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Notes; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.OpeningBalance; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.AsOfDate; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_StateName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Billing_CountryName; col = col + 1;

                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_Street1; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_Street2; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_City; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_StateName; col = col + 1;
                    pck.Workbook.Worksheets[0].Cells[row, col].Value = obj.Shipping_CountryName; col = col + 1;
                }

                return pck.GetAsByteArray();
            }

            return null;
        }

        public List<SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spSupMstSupplierGet"))
            {
                dbhlper.AddPparameter("@SupID", SupID);
                dbhlper.AddPparameter("@BranchId", BranchId);
                dbhlper.AddPparameter("@OrganizationCode", OrganizationCode);
                dbhlper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                dbhlper.AddPparameter("@UserID", UserID);
                dbhlper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = dbhlper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<SupplierInfo> list = new List<SupplierInfo>();
                        SupplierInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new SupplierInfo();
                            obj.SupID = dr["SupID"].ToString();
                            obj.Title = dr["Title"].ToString();
                            obj.Safix = dr["Safix"].ToString();
                            obj.First_Name = dr["FirstName"].ToString();
                            obj.Middle_Name = dr["MiddleName"].ToString();
                            obj.Last_Name = dr["LastName"].ToString();
                            obj.DOB = dr["DOB"].ToString();
                            obj.Sex = dr["Sex"].ToString();
                            obj.ParentSupID = dr["ParentSupID"].ToString();
                            obj.IsSubSupplier = dr["ParentSupID"].ToString().Trim().Length > 0;
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";

                            //Each table holds different valies for the same fields
                            //So, we are not collecting those data for now

                            //obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            //obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            //obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            //obj.ActivityType = dr["ActivityType"].ToString();

                            obj.EmailID = dr["EmailID"].ToString();
                            obj.Mobile = dr["Mobile"].ToString();
                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.Website = dr["Website"].ToString();

                            obj.GSTRegistrationType = dr["GSTRegistrationType"].ToString();
                            obj.GSTRegistrationTypeName = dr["GSTRegistrationTypeName"].ToString();
                            obj.GSTIN = dr["GSTIN"].ToString();
                            obj.TaxRegNo = dr["TaxRegNo"].ToString();
                            obj.CSTRegNo = dr["CSTRegNo"].ToString();
                            obj.PANNo = dr["PANNo"].ToString();
                            obj.Terms = dr["Terms"].ToString();
                            obj.TermsName = dr["TermsName"].ToString();

                            obj.PrefferedPaymentMethod = dr["PrefferedPaymentMethod"].ToString();
                            obj.PrefferedPaymentMethodName = dr["PrefferedPaymentMethodName"].ToString();
                            obj.PrefferedDeliveryMethod = dr["PrefferedDeliveryMethod"].ToString();
                            obj.PrefferedDeliveryMethodName = dr["PrefferedDeliveryMethodName"].ToString();

                            obj.OpeningBalance = dr["OpeningBalance"].ToString();
                            obj.AsOfDate = dr["AsOfDate"].ToString();

                            obj.Shipping_Street1 = dr["ShippingStreet1"].ToString();
                            obj.Shipping_Street2 = dr["ShippingStreet2"].ToString();
                            obj.Shipping_City = dr["ShippingCity"].ToString();
                            obj.Shipping_State = dr["ShippingState"].ToString();
                            obj.Shipping_StateName = dr["ShippingStateName"].ToString();
                            obj.Shipping_Country = dr["ShippingCountry"].ToString();
                            obj.Shipping_CountryName = dr["ShippingCountryName"].ToString();

                            obj.Billing_Street1 = dr["BillingStreet1"].ToString();
                            obj.Billing_Street2 = dr["BillingStreet2"].ToString();
                            obj.Billing_City = dr["BillingCity"].ToString();
                            obj.Billing_State = dr["BillingState"].ToString();
                            obj.Billing_StateName = dr["BillingStateName"].ToString();
                            obj.Billing_Country = dr["BillingCountry"].ToString();
                            obj.Billing_CountryName = dr["BillingCountryName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            if (!string.IsNullOrEmpty(SupID))
            {
                List<SupplierInfo> list = GetList_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            //obj.ListStaticValuesAll = new wscalls().GetList_StaticValue("");
            //obj.ListTermsAll = new wscalls().GetList_Terms("", OrganizationCode);

            return new SupplierInfo();
        }

        public DataSet Upload_Supplier(bool isOvereWrite, DataSet ds, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";

            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return null; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Validation - Column existance
                if (ds.Tables[0].Columns.IndexOf("First Name") < 0) { errormsg = "[First Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Last Name") < 0) { errormsg = "[Last Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Company Name") < 0) { errormsg = "[Company Name] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Email") < 0) { errormsg = "[Email] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Mobile") < 0) { errormsg = "[Mobile] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("FAX") < 0) { errormsg = "[FAX] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Street") < 0) { errormsg = "[Street] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("City") < 0) { errormsg = "[City] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("State") < 0) { errormsg = "[State] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Opening Balance") < 0) { errormsg = "[Opening Balance] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("As Of Date") < 0) { errormsg = "[As Of Date] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("GST Registration Type") < 0) { errormsg = "[GST Registration Type] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("GSTIN Number") < 0) { errormsg = "[GSTIN Number] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Tax Reg No") < 0) { errormsg = "[Tax Reg.No.] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Cst Reg No") < 0) { errormsg = "[Cst Reg. No.] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("PAN Number") < 0) { errormsg = "[PAN Number] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Terms") < 0) { errormsg = "[Terms] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Effective Date") < 0) { errormsg = "[Effective Date] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Prefered Delivery Method") < 0) { errormsg = "[Prefered Delivery Method] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Prefered Paymant Method") < 0) { errormsg = "[Prefered Paymant Method] column is not available"; }

                else if (ds.Tables[0].Columns.IndexOf("Parent Supplier") < 0) { errormsg = "[Parent Supplier] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing Street1") < 0) { errormsg = "[Billing Street1] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing City") < 0) { errormsg = "[Billing City] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing State") < 0) { errormsg = "[Billing State] column is not available"; }
                else if (ds.Tables[0].Columns.IndexOf("Billing Country") < 0) { errormsg = "[Billing Country] column is not available"; }
                else
                {
                    ds.Tables[0].Columns.Add("Row Number");
                    ds.Tables[0].Columns.Add("Error Message");

                    bool DisplayErroeMessage = false;

                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr["First Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "First Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Middle Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Last Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Company Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Company Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Email"].ToString(), Validations.ValueType.Email, true, "Email", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Mobile"].ToString(), Validations.ValueType.MobileNumber, true, "Mobile", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["FAX"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "FAX", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Street"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Street", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Parent Supplier"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Supplier", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Opening Balance"].ToString(), Validations.ValueType.Numeric, true, "Opening Balance", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["As Of Date"].ToString(), Validations.ValueType.DateTime, true, "As Of Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["GST Registration Type"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GST Registration Type", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["GSTIN Number"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GSTIN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Tax Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Cst Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Cst Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["PAN Number"].ToString(), Validations.ValueType.AlphaNumeric, true, "PAN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Terms"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Terms", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Effective Date"].ToString(), Validations.ValueType.DateTime, true, "Effective Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Prefered Delivery Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Delivery Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Prefered Paymant Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Paymant Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing Street1"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Street1", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        if (!Validations.ValidateDataType(dr["Billing Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
                        #endregion
                    }
                    #endregion

                    if (!DisplayErroeMessage)
                    {
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Error Message"));
                        ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.IndexOf("Row Number"));

                        using (DBHelper dbhlper = new DBHelper("[spSupMSTSupplierUpload]", true))
                        {
                            dbhlper.AddPparameter("@Supplier_Data", ds.Tables[0], DBHelper.param_types.Structured);

                            dbhlper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                            dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            dbhlper.Execute_NonQuery(out errormsg);
                        }
                    }
                    else
                    {
                        errormsg = "Problem in excel data. Please rectify before upload";
                        return ds;
                    }
                }
            }

            return null;
        }

        public bool Save_Supplier(bool isOnlyDelete, SupplierInfo objSupplierInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;
            //string NewSupID = "";

            #region Validations
            if (!Validations.ValidateDataType(objSupplierInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.SupID, Validations.ValueType.Integer, true, "Supplier ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Title, Validations.ValueType.AlphaNumericSpecialChar, true, "Title", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.First_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "First name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Middle_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Last name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Last_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Middle name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Safix, Validations.ValueType.AlphaNumericSpecialChar, true, "Safix", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Sex, Validations.ValueType.Alphabet, true, "Sex", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.DOB, Validations.ValueType.DateTime, true, "DOB", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSupplierInfo.EmailID, Validations.ValueType.Email, true, "EmailID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Mobile, Validations.ValueType.MobileNumber, true, "Mobile number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.State, Validations.ValueType.Numeric, true, "State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Country, Validations.ValueType.Numeric, true, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Website, Validations.ValueType.AlphaNumericSpecialChar, true, "Website", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSupplierInfo.GSTRegistrationType, Validations.ValueType.Numeric, true, "GST registration type", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.ParentSupID, Validations.ValueType.Numeric, true, "Parent Supplier id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.PrefferedPaymentMethod, Validations.ValueType.Numeric, true, "Preffered payment method", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.PrefferedDeliveryMethod, Validations.ValueType.Numeric, true, "Preffered delivery method", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.TaxRegNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Reg. No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.CSTRegNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Cst Reg. No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.PANNo, Validations.ValueType.AlphaNumericSpecialChar, true, "Pan No.", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Terms, Validations.ValueType.Numeric, true, "Terms", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSupplierInfo.OpeningBalance, Validations.ValueType.Numeric, true, "Opening balance", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.AsOfDate, Validations.ValueType.DateTime, true, "As of date ", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSupplierInfo.Shipping_Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1 of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Shipping_Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2 of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Shipping_City, Validations.ValueType.AlphaNumericSpecialChar, true, "City of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Shipping_State, Validations.ValueType.Numeric, true, "State of Shipping address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Shipping_Country, Validations.ValueType.Numeric, true, "Country of Shipping address", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objSupplierInfo.Billing_Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1 of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Billing_Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2 of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Billing_City, Validations.ValueType.AlphaNumericSpecialChar, true, "City of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Billing_State, Validations.ValueType.Numeric, true, "State of Billing address", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objSupplierInfo.Billing_Country, Validations.ValueType.Numeric, true, "Country of Billing address", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spSupMSTSupplierSave]", true))
            {
                dbhlper.AddPparameter("@SupID", (objSupplierInfo.SupID == "" ? "0" : objSupplierInfo.SupID), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Title", HttpUtility.HtmlEncode(objSupplierInfo.Title), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objSupplierInfo.First_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objSupplierInfo.Middle_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objSupplierInfo.Last_Name), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objSupplierInfo.Safix), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objSupplierInfo.Sex), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objSupplierInfo.DOB), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(objSupplierInfo.OrganizationCode), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@EmailID", objSupplierInfo.EmailID, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Mobile", objSupplierInfo.Mobile, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objSupplierInfo.Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objSupplierInfo.Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@City", HttpUtility.HtmlEncode(objSupplierInfo.City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@State", (objSupplierInfo.State == "" ? "0" : objSupplierInfo.State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Country", (objSupplierInfo.Country == "" ? "0" : objSupplierInfo.Country), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@Website", HttpUtility.HtmlEncode(objSupplierInfo.Website), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@GSTRegistrationType", (objSupplierInfo.GSTRegistrationType == "" ? "0" : objSupplierInfo.GSTRegistrationType), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@GSTIN", HttpUtility.HtmlEncode(objSupplierInfo.GSTIN), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@IsSubSupplier", (objSupplierInfo.IsSubSupplier ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ParentSupID", (objSupplierInfo.ParentSupID == "" ? "0" : objSupplierInfo.ParentSupID), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@BillingWith", HttpUtility.HtmlEncode(objSupplierInfo.BillingWith), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objSupplierInfo.Notes), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PrefferedPaymentMethod", HttpUtility.HtmlEncode(objSupplierInfo.PrefferedPaymentMethod), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PrefferedDeliveryMethod", HttpUtility.HtmlEncode(objSupplierInfo.PrefferedDeliveryMethod), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@TaxRegNo", HttpUtility.HtmlEncode(objSupplierInfo.TaxRegNo), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CSTRegNo", objSupplierInfo.CSTRegNo, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@PANNo", HttpUtility.HtmlEncode(objSupplierInfo.PANNo), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Terms", (objSupplierInfo.Terms == "" ? "0" : objSupplierInfo.ParentSupID), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@OpeningBalance", (objSupplierInfo.OpeningBalance == "" ? "0" : objSupplierInfo.OpeningBalance), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@AsOfDate", Validations.ConvertToDateReturn_ddMMyyyy_blank(objSupplierInfo.AsOfDate), DBHelper.param_types.Varchar);

                dbhlper.AddPparameter("@ShippingStreet1", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingStreet2", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingCity", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ShippingState", (objSupplierInfo.Shipping_State == "" ? "0" : objSupplierInfo.Shipping_State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@ShippingCountry", (objSupplierInfo.Shipping_Country == "" ? "0" : objSupplierInfo.Shipping_Country), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@BillingStreet1", HttpUtility.HtmlEncode(objSupplierInfo.Billing_Street1), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingStreet2", HttpUtility.HtmlEncode(objSupplierInfo.Billing_Street2), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingCity", HttpUtility.HtmlEncode(objSupplierInfo.Billing_City), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@BillingState", (objSupplierInfo.Billing_State == "" ? "0" : objSupplierInfo.Billing_State), DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@BillingCountry", (objSupplierInfo.Billing_Country == "" ? "0" : objSupplierInfo.Billing_Country), DBHelper.param_types.BigInt);

                dbhlper.AddPparameter("@IsActive", objSupplierInfo.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewSupID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        //#region User Related
        ////public List<UserInfo> GetList_User(string UserCode, string UserType, bool IsActive)
        ////{
        ////    using (DBHelper dbhlper = new DBHelper("spSupMSTUserGet"))
        ////    {
        ////        dbhlper.AddPparameter("@UserType", UserCode);
        ////        dbhlper.AddPparameter("@UserCode", UserType);

        ////        using (DataSet ds = dbhlper.Execute_Query())
        ////        {

        ////            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        ////            {
        ////                List<UserInfo> list = new List<UserInfo>();
        ////                UserInfo obj = null;

        ////                foreach (DataRow dr in ds.Tables[0].Rows)
        ////                {
        ////                    obj = new UserInfo();
        ////                    obj.UserCode = dr["UserCode"].ToString();
        ////                    obj.Title = dr["Title"].ToString();
        ////                    obj.First_Name = dr["FirstName"].ToString();
        ////                    obj.Middle_Name = dr["MiddleName"].ToString();
        ////                    obj.Last_Name = dr["LastName"].ToString();
        ////                    obj.DOB = dr["DOB"].ToString();
        ////                    obj.Sex = dr["Sex"].ToString();

        ////                    obj.EmailID = dr["EmailID"].ToString();
        ////                    obj.MobileNumber = dr["Mobile"].ToString();
        ////                    obj.Street1 = dr["Street1"].ToString();
        ////                    obj.City = dr["City"].ToString();
        ////                    obj.State = dr["State"].ToString();
        ////                    obj.StateName = dr["StateName"].ToString();
        ////                    obj.Country = dr["Country"].ToString();
        ////                    obj.CountryName = dr["CountryName"].ToString();

        ////                    if (!string.IsNullOrEmpty(UserCode) && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
        ////                    {
        ////                        obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
        ////                        UserBranchRoleMappingInfo objFunction = null;

        ////                        foreach (DataRow drc in ds.Tables[1].Rows)
        ////                        {
        ////                            objFunction = new UserBranchRoleMappingInfo();

        ////                            objFunction.FunctionID = drc["FunctionId"].ToString();
        ////                            objFunction.FunctionName = drc["FunctionName"].ToString();
        ////                            objFunction.BranchID = drc["BranchID"].ToString();
        ////                            objFunction.BranchName = drc["BranchName"].ToString();
        ////                            objFunction.OrganizationCode = drc["OrganizationCode"].ToString();
        ////                            objFunction.OrganizationName = drc["OrganizationName"].ToString();
        ////                            objFunction.isSelected = drc["isSelected"].ToString().Trim().ToUpper() == "Y";

        ////                            obj.UserBranchRoleList.Add(objFunction);
        ////                        }
        ////                    }

        ////                    list.Add(obj);
        ////                }

        ////                return list;
        ////            }
        ////        }

        ////        return null;
        ////    }
        ////}

        ////public UserInfo GetDetails_User(string UserCode, string UserType, bool IsActive)
        ////{
        ////    if (!string.IsNullOrEmpty(UserCode) && !string.IsNullOrEmpty(UserType))
        ////    {
        ////        List<UserInfo> list = GetList_User(UserCode, UserType, IsActive);

        ////        if (list != null && list.Count() > 0)
        ////        {
        ////            return list[0];
        ////        }
        ////    }

        ////    return new UserInfo();
        ////}

        ////public bool User_Registration(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, UserInfo objUserInfo, out string errormsg)
        ////{
        ////    errormsg = "";
        ////    flag = false;

        ////    #region Validations
        ////    if (string.IsNullOrEmpty(objUserInfoToBeSaved.OrganizationName)) { errormsg = "Organization name should not be blank"; return false; }
        ////    if (string.IsNullOrEmpty(objUserInfoToBeSaved.EmailID)) { errormsg = "EmailID should not be blank"; return false; }
        ////    if (string.IsNullOrEmpty(objUserInfoToBeSaved.MobileNumber)) { errormsg = "Mobile number should not be blank"; return false; }
        ////    if (string.IsNullOrEmpty(objUserInfoToBeSaved.Password)) { errormsg = "Password should not be blank"; return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserID, Validations.ValueType.Integer, true, "User ID", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.EmailID, Validations.ValueType.Email, true, "EmailID", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.MobileNumber, Validations.ValueType.MobileNumber, true, "Mobile number", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.State, Validations.ValueType.Integer, true, "State", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Country, Validations.ValueType.Integer, true, "Country", out errormsg)) { return false; }
        ////    #endregion

        ////    using (DBHelper dbhlper = new DBHelper("[SPURMSTUserRegistration]", true))
        ////    {
        ////        dbhlper.AddPparameter("@UserType", objUserInfoToBeSaved.UserType, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@OrganizationName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.OrganizationName), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@UserName", "", DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Mobile", objUserInfoToBeSaved.MobileNumber, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Password", objUserInfoToBeSaved.Password, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@EmailID", objUserInfoToBeSaved.EmailID, DBHelper.param_types.Varchar);

        ////        dbhlper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street1), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street2), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@City", HttpUtility.HtmlEncode(objUserInfoToBeSaved.City), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@State", (objUserInfoToBeSaved.State.Trim().Length > 0 ? Convert.ToInt32(objUserInfoToBeSaved.State) : 0), DBHelper.param_types.BigInt);
        ////        dbhlper.AddPparameter("@Country", (objUserInfoToBeSaved.Country.Trim().Length > 0 ? Convert.ToInt32(objUserInfoToBeSaved.Country) : 0), DBHelper.param_types.BigInt);
        ////        dbhlper.AddPparameter("@PIN", objUserInfoToBeSaved.PIN, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
        ////        dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

        ////        return dbhlper.Execute_NonQuery(out errormsg);
        ////    }
        ////}

        ////public bool Save_User(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, UserInfo objUserInfo, out string errormsg)
        ////{
        ////    errormsg = "";
        ////    flag = false;

        ////    #region Validations
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserID, Validations.ValueType.Integer, true, "User ID", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Title, Validations.ValueType.AlphaNumericSpecialChar, true, "Title", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.First_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "First name", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Middle_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Last name", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Last_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Middle name", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Sex, Validations.ValueType.Alphabet, true, "Sex", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.DOB, Validations.ValueType.DateTime, true, "DOB", out errormsg)) { return false; }

        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.EmailID, Validations.ValueType.Email, true, "EmailID", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.MobileNumber, Validations.ValueType.MobileNumber, true, "Mobile number", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.State, Validations.ValueType.Integer, true, "State", out errormsg)) { return false; }
        ////    if (!Validations.ValidateDataType(objUserInfoToBeSaved.Country, Validations.ValueType.Integer, true, "Country", out errormsg)) { return false; }
        ////    #endregion

        ////    using (DBHelper dbhlper = new DBHelper("SPURMSTUserRegisteredSave", true))
        ////    {
        ////        #region MyRegion
        ////        dbhlper.AddPparameter("@IsOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@UserName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.UserName), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Title", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Title), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.First_Name), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Middle_Name), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Last_Name), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objUserInfoToBeSaved.DOB), DBHelper.param_types.DateTime);
        ////        dbhlper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Sex), DBHelper.param_types.Varchar);

        ////        dbhlper.AddPparameter("@AccessAllowed", (objUserInfoToBeSaved.AccessAllowed ? "Y" : ""), DBHelper.param_types.Varchar);

        ////        dbhlper.AddPparameter("@IsApproved", (objUserInfoToBeSaved.IsApproved ? "Y" : ""), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@DONEBY", objUserInfo.UserID, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@UserCode", "", DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Mobile", objUserInfoToBeSaved.MobileNumber, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@IsMobileActive", HttpUtility.HtmlEncode(objUserInfoToBeSaved.IsMobileActive ? "Y" : ""), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Password", objUserInfoToBeSaved.Password, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@EmailID", objUserInfoToBeSaved.EmailID, DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@IsEmailActive", objUserInfoToBeSaved.IsEmailActive ? "Y" : "", DBHelper.param_types.Varchar);

        ////        dbhlper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street1), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street2), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@City", HttpUtility.HtmlEncode(objUserInfoToBeSaved.City), DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@State", objUserInfoToBeSaved.State, DBHelper.param_types.BigInt);
        ////        dbhlper.AddPparameter("@Country", objUserInfoToBeSaved.Country, DBHelper.param_types.BigInt);
        ////        dbhlper.AddPparameter("@IsAddressApproved", "Y", DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@IsAddressActive", "Y", DBHelper.param_types.Varchar);
        ////        dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
        ////        #endregion

        ////        return dbhlper.Execute_NonQuery(out errormsg);
        ////    }
        ////}
        //#endregion

        #region Login Related
        public UserInfo User_Login(string UserName, string Password, string UserType, out string errormsg)
        {
            errormsg = "";

            using (DBHelper dbhlper = new DBHelper("spLogin"))
            {
                try
                {
                    dbhlper.AddPparameter("@UserType", "R");
                    dbhlper.AddPparameter("@UserCode", UserName);
                    dbhlper.AddPparameter("@Password", Password);

                    using (DataSet ds = dbhlper.Execute_Query())
                    {

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            UserInfo obj = new UserInfo();

                            obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                            obj.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                            obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                            obj.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            obj.Country = ds.Tables[0].Rows[0]["Country"].ToString();

                            obj.UserRegisteredEmail = new UserEmailInfo();
                            obj.UserRegisteredEmail.IsEmailVerified = ds.Tables[0].Rows[0]["IsEmailVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = ds.Tables[0].Rows[0]["IsEmailConfirmationSend"].ToString().Trim().ToUpper() == "Y";

                            obj.UserRegisteredMobile = new UserMobileInfo();
                            obj.UserRegisteredMobile.IsMobileVerified = ds.Tables[0].Rows[0]["IsMobileVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = ds.Tables[0].Rows[0]["IsMobileConfirmationSent"].ToString().Trim().ToUpper() == "Y";

                            obj.MobileNumber = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                            obj.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                            obj.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                            obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                            obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                            obj.EffectiveRole = Get_Effective_Role_ForAUser("", obj.UserCode);

                            Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall-Success");

                            return obj;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                    errormsg = ex.Message;
                }
                return null;
            }
        }

        public bool User_ChangePassword(string OldPssword, string NewPassword, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(OldPssword, Validations.ValueType.AlphaNumericSpecialChar, false, "Old Password", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(NewPassword, Validations.ValueType.AlphaNumericSpecialChar, false, "New Password", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spChangePassword]", true))
            {
                dbhlper.AddPparameter("@UserCode", objUserInfo.UserCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@NewPassword", NewPassword, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OldPassword", OldPssword, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public bool User_ForgotPassword(string UserCodeEmailIDMobile, string OTPValidityDuration, string OTPSendOption, string UserCode, out string OTP, out string errormsg)
        {
            errormsg = "";
            OTP = "";

            #region Validations
            if (!Validations.ValidateDataType(UserCodeEmailIDMobile, Validations.ValueType.AlphaNumericSpecialChar, false, "User ID", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spForgotPassword]", true))
            {
                dbhlper.AddPparameter("@UserCodeEmailIDMobile", UserCodeEmailIDMobile, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OTPValidityDuration", OTPValidityDuration, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OTPSendOption", OTPSendOption, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@UserCode", UserCode, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@OTP", OTP, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }

        public UserInfo SocialMediaLogin(string UserName, string UserType, out string errormsg)
        {
            errormsg = "";

            using (DBHelper dbhlper = new DBHelper("spLogin"))
            {
                try
                {
                    dbhlper.AddPparameter("@UserType", "");
                    dbhlper.AddPparameter("@UserCode", UserName);

                    using (DataSet ds = dbhlper.Execute_Query())
                    {

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            UserInfo obj = new UserInfo();

                            obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                            obj.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                            obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                            obj.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();

                            obj.UserRegisteredEmail = new UserEmailInfo();
                            obj.UserRegisteredEmail.IsEmailVerified = ds.Tables[0].Rows[0]["IsEmailVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = ds.Tables[0].Rows[0]["IsEmailConfirmationSend"].ToString().Trim().ToUpper() == "Y";

                            obj.UserRegisteredMobile = new UserMobileInfo();
                            obj.UserRegisteredMobile.IsMobileVerified = ds.Tables[0].Rows[0]["IsMobileVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = ds.Tables[0].Rows[0]["IsMobileConfirmationSent"].ToString().Trim().ToUpper() == "Y";

                            obj.MobileNumber = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                            obj.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                            obj.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                            obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                            obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                            obj.EffectiveRole = Get_Effective_Role_ForAUser("", obj.UserCode);

                            Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall-Success");

                            return obj;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                }
                return null;
            }
        }

        #endregion

        #region Email Log
        public static bool Save_EmailLog(string MailOption, string Subject, string From, string To, string CC, string Body, string ErrorMessage, string InnerException, string StackStress, int UserId)
        {
            string errormsg = "";

            using (DBHelper dbhlper = new DBHelper("spSaveEmailLog", true))
            {
                #region MyRegion
                dbhlper.AddPparameter("@UserID", UserId, DBHelper.param_types.BigInt);
                dbhlper.AddPparameter("@MailOption", MailOption, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Subject", Subject, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@Body", Body, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@From", From, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@TO", To, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@CC", CC, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@ErrorMessage", ErrorMessage, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@InnerException", InnerException, DBHelper.param_types.Varchar);
                dbhlper.AddPparameter("@StackStress", StackStress, DBHelper.param_types.Varchar);
                #endregion

                return dbhlper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion
    }
}