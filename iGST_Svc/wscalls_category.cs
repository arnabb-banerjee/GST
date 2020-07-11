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
        #region Category Related
        public static List<CategoryInfo> GetList_CategoryForDropdown(string IsExpenseType, int CountryID, string CategoryID, string Option, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spPrMstCategoryGet]"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@CategoryId", !string.IsNullOrEmpty(CategoryID) && !string.IsNullOrWhiteSpace(CategoryID) ? Convert.ToInt32(CategoryID) : 0);

                if (Option == "P")
                {
                    DBHelper.AddPparameter("@WillCarryContent", "N");
                }
                else if (Option == "C")
                {
                    DBHelper.AddPparameter("@WillCarryContent", "Y");
                }
                else
                {
                    DBHelper.AddPparameter("@WillCarryContent", "");
                }

                DBHelper.AddPparameter("@CountryId", CountryID);
                DBHelper.AddPparameter("@ProductId", 0);
                DBHelper.AddPparameter("@IsActive", "Y");
                DBHelper.AddPparameter("@IsExpenseType", IsExpenseType);

                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0, DBHelper.param_types.BigInt);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            obj.ParentCategoryId = dr["ParentCategoryId"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static List<CategoryInfo> GetList_Category(string IsExpenseType, string CategoryID, string CountryId, string ProductId, bool IsActive, string Option, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("[spPRMSTCategoryGet]"))
            {
                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@CategoryId", CategoryID.Trim().Length > 0 ? Convert.ToInt32(CategoryID) : 0);

                if (Option == "P")
                {
                    DBHelper.AddPparameter("@WillCarryContent", "N");
                }
                else if (Option == "C")
                {
                    DBHelper.AddPparameter("@WillCarryContent", "Y");
                }
                else
                {
                    DBHelper.AddPparameter("@WillCarryContent", "");
                }

                DBHelper.AddPparameter("@CountryId", CountryId.Trim().Length > 0 ? Convert.ToInt32(CountryId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ProductId", ProductId.Trim().Length > 0 ? Convert.ToInt32(ProductId) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@IsActive", "");
                DBHelper.AddPparameter("@IsExpenseType", IsExpenseType);
                DBHelper.AddPparameter("@LanguageId", LanguageId.Trim().Length > 0 ? Convert.ToInt32(LanguageId) : 0, DBHelper.param_types.BigInt);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            obj.CountryId = dr["CountryId"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.IsExpenseType = dr["IsExpenseType"].ToString() == "Y";
                            /*obj.CGST = dr["CGST"].ToString();
                            obj.SGST = dr["SGST"].ToString();
                            obj.IGST = dr["IGST"].ToString();*/
                            if (dr["LastModifiedOn"] != DBNull.Value)
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

        public static CategoryInfo GetDetails_Category(string CategoryID, string CountryId, string ProductId, bool IsActive, string LanguageId)
        {
            if (!string.IsNullOrEmpty(CategoryID))
            {
                List<CategoryInfo> list = GetList_Category("", CategoryID, CountryId, ProductId, IsActive, "", LanguageId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new CategoryInfo();
        }

        public static bool Save_Category(bool isOnlyDelete, CategoryInfo objCategory, string UserCode, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(objCategory.CategoryId, Validations.ValueType.Integer, true, "Category ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCategory.CategoryName, Validations.ValueType.AlphaNumericSpecialChar, true, "category Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objCategory.CountryId, Validations.ValueType.Numeric, true, "Country details", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spPrMstCategorySave]", true))
            {
                DBHelper.AddPparameter("@CategoryId", objCategory.CategoryId == "" ? 0 : Convert.ToInt32(objCategory.CategoryId), DBHelper.param_types.Int);
                DBHelper.AddPparameter("@ParentCategoryId", objCategory.ParentCategoryId == "" ? 0 : Convert.ToInt32(objCategory.ParentCategoryId), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CategoryName", HttpUtility.HtmlEncode(objCategory.CategoryName), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CountryId", HttpUtility.HtmlEncode(objCategory.CountryId), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ServiceOrGoods", objCategory.ServiceOrGoods, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@HSNSACCode", objCategory.ServiceOrGoods == "G" ? objCategory.HSNCode : objCategory.SACCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@WillCarryContent", objCategory.WillCarryContent, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", objCategory.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsExpenseType", objCategory.IsExpenseType ? "Y" : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static bool Upload_Category(bool isOvereWrite, DataSet ds, string UserCode, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                #region Column Validation
                int Category = -1;
                int ParentCategory = -1;
                int Country = -1;
                int taxname = -1;
                int applicabletype = -1;
                int isparentcategory = -1;
                int percentage = -1;
                int HSNSACCode = -1;
                int ServiceGoods = -1;
                int IsExpenseType = -1;

                foreach (DataColumn c in ds.Tables[0].Columns)
                {
                    if (c.ColumnName.ToUpper().Contains("EXPENSE")) { IsExpenseType = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("COUNTRY")) { Country = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("PERCENTAGE") || c.ColumnName.ToUpper().Contains("%")) { percentage = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("APPLICATION")) { applicabletype = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("TAX NAME")) { taxname = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("HSN")) { HSNSACCode = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("SERVICE")) { ServiceGoods = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("PARENT CATEGORY") && c.ColumnName.ToUpper().Contains("IS")) { isparentcategory = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper().Contains("PARENT CATEGORY") && !c.ColumnName.ToUpper().Contains("IS")) { ParentCategory = ds.Tables[0].Columns.IndexOf(c); }
                    else if (c.ColumnName.ToUpper() == "CATEGORY") { Category = ds.Tables[0].Columns.IndexOf(c); }
                }

                if (Category < 0) { errormsg = "Column [Category] is not available"; return true; }
                if (isparentcategory < 0) { errormsg = "Column [Is Parent Category] is not available"; return true; }
                if (ServiceGoods < 0) { errormsg = "Column [ServiceGoods] is not available"; return true; }
                if (HSNSACCode < 0) { errormsg = "Column [HSNSAC] is not available"; return true; }
                if (taxname < 0) { errormsg = "Column [Tax Name] is not available"; return true; }
                if (percentage < 0) { errormsg = "Column [Percentage] is not available"; return true; }
                if (applicabletype < 0) { errormsg = "Column [Application Type] is not available"; return true; }
                if (ParentCategory < 0) { errormsg = "Column [Parent Category] is not available"; return true; }
                if (Country < 0) { errormsg = "Column [Country] is not available"; return true; }
                if (IsExpenseType < 0) { errormsg = "Column [Is Expense Category?] is not available"; return true; }
                #endregion

                using (DBHelper dbhlper = new DBHelper("[spPRMstCategoryUpload2]", true))
                {
                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (dr[Category].ToString().Trim().Length == 0) { errormsg = "Category should not be blank for any record"; return false; }
                        //if (dr[Category].ToString().Trim().Length > 0) { errormsg = "Category should not be blank for any record"; return false; }
                        //if (!Validations.ValidateDataType(dr[ParentCategory].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Category", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[taxname].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Name", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[applicabletype].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Application Type", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[Country].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Country", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[percentage].ToString(), Validations.ValueType.Percentage, true, "Percentage", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[isparentcategory].ToString(), Validations.ValueType.PairValueYN, true, "Is Parent Category?", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[IsExpenseType].ToString(), Validations.ValueType.PairValueYN, true, "Is Expense Category?", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr[ServiceGoods].ToString(), Validations.ValueType.AlphabetSpecialChar, true, "Service Goods", out errormsg)) { return false; }
                        #endregion

                        DBHelper.ClearParameters();

                        #region Parameter Add
                        DBHelper.AddPparameter("@isOvereWrite", isOvereWrite ? "Y" : "N", DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@CategoryName", dr[Category].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ParentCategoryName", dr[ParentCategory].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ServiceOrGoods", dr[ServiceGoods].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@HSNSACCode", dr[HSNSACCode].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@TaxName", dr[taxname].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@Percentage", dr[percentage].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@CountrtyName", dr[Country].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ApplicableType", dr[applicabletype].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@WillCarryContent", dr[isparentcategory].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@IsActive", "Y", DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@IsExpenseType", dr[isparentcategory].ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
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

        public static bool Upload_Category2(bool isOvereWrite, DataSet ds, string UserCode, out bool bReturn, out string errormsg)
        {
            bReturn = false;
            errormsg = "";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Columns.IndexOf("Category") < 0) { errormsg = "[Category] column is not available"; return false; }
                else if (ds.Tables[0].Columns.IndexOf("Parent Category") < 0) { errormsg = "[Parent Category] column is not available"; return false; }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; return false; }
                else if (ds.Tables[0].Columns.IndexOf("Tax Name") < 0) { errormsg = "[Tax Name] column is not available"; return false; }
                else if (ds.Tables[0].Columns.IndexOf("Percentage") < 0) { errormsg = "[Percentage] column is not available"; return false; }
                else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; return false; }
                else
                {
                    foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                    {
                        #region Validation
                        if (!Validations.ValidateDataType(dr["Category"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Category", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Parent Category"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Category", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, false, "Country", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Tax Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Name", out errormsg)) { return false; }
                        if (!Validations.ValidateDataType(dr["Percentage"].ToString(), Validations.ValueType.Percentage, true, "Percentage", out errormsg)) { return false; }
                        #endregion
                    }

                    using (DBHelper dbhlper = new DBHelper("[spPRMstCategoryUpload]", true))
                    {
                        DBHelper.AddPparameter("@CategoryData", ds.Tables[0], DBHelper.param_types.Structured);

                        DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                        DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                        return DBHelper.Execute_NonQuery(out errormsg);
                    }

                }
            }

            errormsg = "No data is available to be uploaded";
            return false;
        }

        #endregion
    }
}