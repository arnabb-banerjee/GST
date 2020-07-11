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
    public sealed partial class wsExpense
    {
        public static List<ProductOrganiztionInfo> GetList_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationExpenseGet]"))
            {
                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                DBHelper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0);
                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);
                // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
                //DBHelper.AddPparameter("@isExpense", "E");
                // Modified on [30th August 2019] by [Partha]
                using (DataSet ds = DBHelper.Execute_Query())
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
                            // Modified on [24th August 2019] by [Partha] cause [show OrganizationName in List] - Start
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            // Modified on [24th August 2019] by [Partha] - End
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNSACCode = dr["HSNSACCode"].ToString();
                            obj.Name = dr["name"].ToString();
                            obj.Description = dr["Description"].ToString();
                            obj.SKU = dr["SKU"].ToString();
                            obj.Unit = dr["Unit"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Unit"]);
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

        public static List<ProductOrganiztionInfo> GetDetails_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationExpenseGet]"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                DBHelper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0);
                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            //obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.ProductName = dr["ProductName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.ServiceOrGoods = dr["ServiceOrGoods"].ToString();
                            obj.HSNSACCode = dr["HSNSACCode"].ToString();
                            obj.Name = dr["name"].ToString();
                            obj.Description = dr["Description"].ToString();
                            obj.SKU = dr["SKU"].ToString();
                            obj.Unit = dr["Unit"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Unit"]);
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

        public static bool Save_ExpenseOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            bool flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ProductId, Validations.ValueType.Integer, true, "Product Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CategoryId, Validations.ValueType.Integer, true, "Category Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.SKU, Validations.ValueType.AlphaNumericSpecialChar, true, "SKU", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Description, Validations.ValueType.AlphaNumericSpecialChar, true, "Descripition", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationExpenseSave]", true))
            {
                DBHelper.AddPparameter("@OrganizationproductId", obj.OrganizationproductId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CategoryId", obj.CategoryId.Trim().Length > 0 ? Convert.ToInt32(obj.CategoryId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@OrganizationCode", obj.OrganizationCode.Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Name", obj.Name, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Description", obj.Description, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@SKU", obj.SKU, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Unit", obj.Unit, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Class", obj.Class, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@AbatementPercentage", obj.AbatementPercentage, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@ServiceType", obj.ServiceType, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@SalePrice", obj.SalePrice, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@isInclusiveTax", obj.isInclusiveTax ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AvailableQty", obj.AvailableQty, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@IncomeAccount", obj.IncomeAccount, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@SupplierId", obj.SupplierId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@PreferredSupplierId", obj.PreferredSupplierId, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@ReverseCharge", obj.ReverseCharge, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@PurchaseTax", obj.PurchaseTax, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@SaleTax", obj.SaleTax, DBHelper.param_types.Numeric);
                DBHelper.AddPparameter("@IsActive", obj.IsActive.Trim().ToUpper(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            using (DBHelper dbhlper = new DBHelper("[GetExpenseListMasters]"))
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
                            obj.AmountIncludeTax = dr["AmountIncludeTax"].ToString();
                            /*obj.AmountExcludeTax = dr["AmountExcludeTax"].ToString();*/
                            obj.TaxOnProduct = dr["TaxOnProduct"].ToString();

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
                                    objInvoiceProductInfo.isBreakupNeed = cdr["isBreakupNeed"].ToString() == "Y";

                                    obj.InvoiceProductList.Add(objInvoiceProductInfo);
                                }
                            }

                            if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                            {
                                obj.ExpenseBreakupList = new List<ExpenseBreakupInfo>();
                                ExpenseBreakupInfo objExpenseBreakupInfo = null;

                                foreach (DataRow edr in ds.Tables[2].Rows)
                                {
                                    objExpenseBreakupInfo = new ExpenseBreakupInfo();

                                    objExpenseBreakupInfo.InvoiceProductID = edr["InvoiceProductID"].ToString();
                                    objExpenseBreakupInfo.BreakupId = edr["BreakupId"].ToString();
                                    objExpenseBreakupInfo.FromDate = edr["FromDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(edr["FromDate"]);
                                    objExpenseBreakupInfo.Todate = edr["ToDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(edr["ToDate"]);
                                    objExpenseBreakupInfo.Description = edr["Description"].ToString();
                                    objExpenseBreakupInfo.Price = edr["Price"].ToString();
                                    objExpenseBreakupInfo.Remarks = edr["Remarks"].ToString();

                                    obj.ExpenseBreakupList.Add(objExpenseBreakupInfo);
                                }
                            }

                            // Modified on [18 Oct 2019] to add expense fieled - Start
                            /*if (InvoiceID.Trim().ToString().Length > 0 &&
                                (ds.Tables.Count > 1 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0))
                            {
                                obj.ExpenseBreakupList = new List<ExpenseBreakupInfo>();
                                ExpenseBreakupInfo objExpenseBreakupInfo = null;

                                foreach (DataRow edr in ds.Tables[2].Rows)
                                {
                                    objExpenseBreakupInfo = new ExpenseBreakupInfo();

                                    objExpenseBreakupInfo.ExpenseId = edr["ExpenseId"].ToString();
                                    objExpenseBreakupInfo.FromDate = edr["PricePerUnit"].ToString();
                                    objExpenseBreakupInfo.Todate = edr["ProductId"].ToString();
                                    objExpenseBreakupInfo.Quantity = edr["Quantity"].ToString();
                                    objExpenseBreakupInfo.TaxOnProduct = edr["TaxOnProduct"].ToString();
                                    objExpenseBreakupInfo.TotalAmount = edr["TotalAmount"].ToString();

                                    obj.InvoiceProductList.Add(objInvoiceProductInfo);
                                }
                            }
                            // Modified on [02 Oct 2019] to add expense fieled - End
                        }
                        */

                            list.Add(obj);
                        }

                        return list;

                    }
                }

                return null;
            }
        }

        public static InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            if (!string.IsNullOrEmpty(InvoiceID))
            {
                //Modified on [23th Juy 2019] by [Partha] cause [Data was not coming in Expense detais page after clicking any link from ExpenseList page] - Start
                List<InvoiceInfo> list = GetList_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new InvoiceInfo();
        }

        public static bool Save_Expense(bool isOnlyDelete, InvoiceInfo objBillInfo, UserInfo objUserInfo, out string errormsg)
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
            if (!Validations.CheckIntegerArray(objBillInfo.ProductIDs, Validations.ValueType.AlphaNumericSpecialChar, true, "Products", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMstExpenseSave]", true))
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
                // Modified on [18 Oct 2019] to add expense fieled - Start
                DBHelper.AddPparameter("@XMLExpenseData", objBillInfo.TravellingExpenses, DBHelper.param_types.Varchar);
                // Modified on [18 Oct 2019] to add expense fieled - End
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
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
    }
}