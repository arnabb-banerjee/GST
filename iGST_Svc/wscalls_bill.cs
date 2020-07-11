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
    public sealed partial class wsBills
    {
        #region Bill Related
        public static List<InvoiceInfo> GetList_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            using (DBHelper dbhlper = new DBHelper("[GetInvoiceListMasters]"))
            {
                DBHelper.AddPparameter("@InvoiceID", InvoiceID);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@BranchID", BranchID);
                DBHelper.AddPparameter("@CusID", CusID);
                DBHelper.AddPparameter("@InvoiceDateFrom", InvoiceDateFrom);
                DBHelper.AddPparameter("@InvoiceDateTo", InvoiceDateTo);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            obj.AmountIncludeTax = dr["AmountIncludeTax"].ToString();
                            obj.AmountExcludeTax = dr["AmountExcludeTax"].ToString();
                            obj.TaxOnProduct = dr["TaxOnProduct"].ToString();

                            if (InvoiceID.Trim().ToString().Length > 0)
                            {
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

                                /*Values from child table*/
                                obj.AmountIncludeTax = dr["AmountIncludeTax"].ToString();
                                obj.TaxOnProduct = dr["TaxOnProduct"].ToString();
                                obj.AmountExcludeTax = dr["AmountExcludeTax"].ToString();

                                /*Values from child table*/

                                if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
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
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static InvoiceInfo GetDetails_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
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

        public static bool Save_Bill(bool isOnlyDelete, InvoiceInfo objBillInfo, string UserCode, out string errormsg)
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
                DBHelper.AddPparameter("@InvoiceType", "B", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@InvoiceDate", objBillInfo.InvoiceDate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@InvoiceDueDate", objBillInfo.InvoiceDueDate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@InvoiceID", objBillInfo.InvoiceID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.InvoiceID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@InvoiceNumber", objBillInfo.InvoiceNumber, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BranchID", objBillInfo.BranchID.Trim().Length > 0 ? Convert.ToInt32(objBillInfo.BranchID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@OrganizationCode", objBillInfo.OrganizationCode.Trim().Length > 0 ? objBillInfo.OrganizationCode : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CusID", Convert.ToInt32(objBillInfo.CusID));
                DBHelper.AddPparameter("@ShipAddress", objBillInfo.ShipAddress, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShipCity", objBillInfo.ShipCity, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShipState", Convert.ToInt32(objBillInfo.ShipState), DBHelper.param_types.Int);
                DBHelper.AddPparameter("@ShipCountry", Convert.ToInt32(objBillInfo.ShipCountry), DBHelper.param_types.Int);
                DBHelper.AddPparameter("@XMLProductData", objBillInfo.ProductIDs, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Prices", objBillInfo.Prices, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Taxes", objBillInfo.Taxes, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ChangedCurrency", objBillInfo.ChangedCurrency, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PrevConversionRate", objBillInfo.PrevConversionRate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ConversionRate", objBillInfo.ConversionRate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@SumAmount", objBillInfo.SumAmount, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AmountPayable", objBillInfo.AmountPayable, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AmountDue", objBillInfo.AmountDue, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsReturned", (objBillInfo.IsReturned ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsCancelled", (objBillInfo.IsCancelled ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion
    }
}