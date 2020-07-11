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
        #region Product Related
        public static List<ProductInfo> GetList_ProductDropdownlist(string ProductID, string ProductName, bool IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spPrMstProductGet"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@ProductID", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                DBHelper.AddPparameter("@ProductName", ProductName);
                //DBHelper.AddPparameter("@IsExpense", (IsExpense ? "Y" : ""));
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@CategoryId", CategoryId.Trim().Length > 0 ? Convert.ToInt32(CategoryId) : 0);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static List<ProductInfo> GetList_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spPrMstProductGet"))
            {
                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@ProductID", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                DBHelper.AddPparameter("@ProductName", ProductName);
                //DBHelper.AddPparameter("@IsExpense", IsExpense);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@CategoryId", CategoryId.Trim().Length > 0 ? Convert.ToInt32(CategoryId) : 0);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            obj.CountryId = dr["CountryId"].ToString();
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

        public static ProductInfo GetDetails_Product(string ProductID, string ProductName, string IsExpense, string OrganizationCode, string CategoryId, bool IsActive, string LanguageId)
        {
            if (!string.IsNullOrEmpty(ProductID))
            {
                List<ProductInfo> list = GetList_Product(ProductID, ProductName, IsExpense, OrganizationCode, CategoryId, IsActive, LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ProductInfo();
        }

        public static bool Save_Product(bool isOnlyDelete, ProductInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.ProductId, Validations.ValueType.Integer, true, "Product ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ProductName, Validations.ValueType.AlphaNumericSpecialChar, false, "Product Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CategoryId, Validations.ValueType.Numeric, false, "Category", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spPRMSTProductSave]", true))
            {
                DBHelper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@ProductName", HttpUtility.HtmlEncode(obj.ProductName), DBHelper.param_types.Varchar);
                //DBHelper.AddPparameter("@IsExpense", (obj.IsExpense ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CategoryId", obj.CategoryId.Trim().Length > 0 ? Convert.ToInt32(obj.CategoryId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CountryID", obj.CountryId.Trim().Length > 0 ? Convert.ToInt32(obj.CountryId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static bool Upload_Product(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                int Category = -1;
                int Product = -1;
                int Country = -1;

                #region Column Validation
                int index = ds.Tables[0].Columns.IndexOf("Category");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("CATEGORY"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("category"); }
                if (index < 0) { errormsg = "Column [Category] is not available"; return true; }
                Category = index;

                index = ds.Tables[0].Columns.IndexOf("Product");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("PRODUCT"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("product"); }
                if (index < 0) { errormsg = "Column [Product] is not available"; return true; }
                Product = index;

                index = ds.Tables[0].Columns.IndexOf("Country");
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("COUNTRY"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("country"); }
                if (index < 0) { index = ds.Tables[0].Columns.IndexOf("cntry"); }
                if (index < 0) { errormsg = "Column [Country] is not available"; return true; }
                Country = index;
                #endregion

                using (DBHelper dbhlper = new DBHelper("[spPRMstProductUpload2]", true))
                {
                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr[Product].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Transaction ID", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Category].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Chq No", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Country].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Particulars", out errormsg)) { return false; }
                        #endregion

                        DBHelper.ClearParameters();

                        #region Parameter Add
                        DBHelper.AddPparameter("@isOvereWrite", isOvereWrite ? "Y" : "N", DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ProductName", dr[Product].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@CategoryName", dr[Category].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@CountryName", dr[Country].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@IsActive", "Y", DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@isOnlyDelete", "N", DBHelper.param_types.Varchar);
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

        public static bool Upload_Product2(bool isOvereWrite, DataSet ds, UserInfo objUserInfo, out bool bReturn, out string errormsg)
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
                            DBHelper.AddPparameter("@ProductData", ds.Tables[0], DBHelper.param_types.Structured);

                            DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
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

            errormsg = "No data is available to be uploaded";
            return false;
        }

        public static List<ProductOrganiztionInfo> GetList_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            List<DBHelper.Parameter> ParamList = new List<DBHelper.Parameter>();
            ParamList.Add(new DBHelper.Parameter("@Mode", 0));
            ParamList.Add(new DBHelper.Parameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0));
            ParamList.Add(new DBHelper.Parameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0));
            ParamList.Add(new DBHelper.Parameter("@OrganizationCode", OrganizationCode));
            ParamList.Add(new DBHelper.Parameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0));
            ParamList.Add(new DBHelper.Parameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0));
            // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
            //DBHelper.AddPparameter("@isExpense", isExpense);
            // Modified on [30th August 2019] by [Partha]

            using (DBHelper dbhlper = new DBHelper())
            {
                using (DataSet ds = dbhlper.ExecuteQuery("spMAPOrganizationproductGetList", ParamList))
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

        public static List<ProductOrganiztionInfo> GetDetails_ProductOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductGet]"))
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

        public static bool Save_ProductOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ProductId, Validations.ValueType.Integer, true, "Product Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CategoryId, Validations.ValueType.Integer, false, "Category Info", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.SKU, Validations.ValueType.AlphaNumericSpecialChar, true, "SKU", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.Description, Validations.ValueType.AlphaNumericSpecialChar, true, "Descripition", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductSave]", true))
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

        public static GSTInfo Get_Gst(string ProductId, string ShipStateId, string OrganizationCode)
        {
            using (DBHelper dbhlper = new DBHelper("spGet_Gst"))
            {
                GSTInfo obj = new GSTInfo();
                obj.Percentage = "0";
                obj.SalePrice = "0";

                DBHelper.AddPparameter("@ProductId", ProductId.Trim().Length > 0 ? Convert.ToInt32(ProductId) : 0);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@ShipStateID", ShipStateId.Trim().Length > 0 ? Convert.ToInt32(ShipStateId) : 0);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static GSTInfo Get_GstCategory(string CategoryId, string ShipStateId, string OrganizationCode)
        {
            using (DBHelper dbhlper = new DBHelper("spGet_GstCategory"))
            {
                GSTInfo obj = new GSTInfo();
                obj.Percentage = "0";
                obj.SalePrice = "0";

                DBHelper.AddPparameter("@CategoryId", CategoryId.Trim().Length > 0 ? Convert.ToInt32(CategoryId) : 0);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@ShipStateID", ShipStateId.Trim().Length > 0 ? Convert.ToInt32(ShipStateId) : 0);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Percentage = ds.Tables[0].Rows[0]["Percentage"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["Percentage"].ToString();
                    }
                }

                return obj;
            }
        }

        public static bool Save_ProductOrganizationImage(bool isOnlyDelete, ProductOrganiztionImageInfo obj, UserInfo objUserInfo, out string errormsg)
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
                DBHelper.AddPparameter("@ImageId", obj.ImageId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@OrganizationproductId", obj.OrganizationproductId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ProductId", obj.ProductId.Trim().Length > 0 ? Convert.ToInt32(obj.ProductId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@FileData", obj.FileData == null ? new byte[] { } : obj.FileData, DBHelper.param_types.Image);
                DBHelper.AddPparameter("@FileName", obj.FileName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FileType", obj.FileType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Seq", obj.Seq, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsMain", obj.Ismain.Trim().ToUpper(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", obj.IsActive.Trim().ToUpper(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<ProductOrganiztionImageInfo> GetList_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationproductImageGet]"))
            {
                DBHelper.AddPparameter("@ImageId", ImageId);
                DBHelper.AddPparameter("@OrganizationproductId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                DBHelper.AddPparameter("@ProductId", ProductID.Trim().Length > 0 ? Convert.ToInt32(ProductID) : 0);
                DBHelper.AddPparameter("@IsActive", IsActive);
                DBHelper.AddPparameter("@IsMain", IsMain);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static ProductOrganiztionImageInfo GetDetails_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
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
    }
}