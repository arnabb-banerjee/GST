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
    public sealed partial class wsSupplier
    {
        #region Supplier Related
        public static byte[] GetExcel_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
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

        public static List<SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spSupMstSupplierGet"))
            {
                DBHelper.AddPparameter("@SupID", SupID);
                DBHelper.AddPparameter("@BranchId", BranchId);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@UserID", UserID);
                DBHelper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
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

        public static DataSet Upload_Supplier(bool isOvereWrite, DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg)
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
                            DBHelper.AddPparameter("@Supplier_Data", ds.Tables[0], DBHelper.param_types.Structured);

                            DBHelper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@isOvereWrite", (isOvereWrite ? "Y" : ""), DBHelper.param_types.Varchar);
                            DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                            DBHelper.Execute_NonQuery(out errormsg);
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

        public static bool Save_Supplier(bool isOnlyDelete, SupplierInfo objSupplierInfo, string UserCode, out string errormsg)
        {
            errormsg = "";
            bool flag = false;
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
                DBHelper.AddPparameter("@SupID", (objSupplierInfo.SupID == "" ? "0" : objSupplierInfo.SupID), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Title", HttpUtility.HtmlEncode(objSupplierInfo.Title), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objSupplierInfo.First_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objSupplierInfo.Middle_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objSupplierInfo.Last_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objSupplierInfo.Safix), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objSupplierInfo.Sex), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objSupplierInfo.DOB), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(objSupplierInfo.OrganizationCode), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@EmailID", objSupplierInfo.EmailID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Mobile", objSupplierInfo.Mobile, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objSupplierInfo.Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objSupplierInfo.Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", HttpUtility.HtmlEncode(objSupplierInfo.City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", (objSupplierInfo.State == "" ? "0" : objSupplierInfo.State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Country", (objSupplierInfo.Country == "" ? "0" : objSupplierInfo.Country), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Website", HttpUtility.HtmlEncode(objSupplierInfo.Website), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@GSTRegistrationType", (objSupplierInfo.GSTRegistrationType == "" ? "0" : objSupplierInfo.GSTRegistrationType), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@GSTIN", HttpUtility.HtmlEncode(objSupplierInfo.GSTIN), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsSubSupplier", (objSupplierInfo.IsSubSupplier ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ParentSupID", (objSupplierInfo.ParentSupID == "" ? "0" : objSupplierInfo.ParentSupID), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@BillingWith", HttpUtility.HtmlEncode(objSupplierInfo.BillingWith), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objSupplierInfo.Notes), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PrefferedPaymentMethod", HttpUtility.HtmlEncode(objSupplierInfo.PrefferedPaymentMethod), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PrefferedDeliveryMethod", HttpUtility.HtmlEncode(objSupplierInfo.PrefferedDeliveryMethod), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@TaxRegNo", HttpUtility.HtmlEncode(objSupplierInfo.TaxRegNo), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CSTRegNo", objSupplierInfo.CSTRegNo, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PANNo", HttpUtility.HtmlEncode(objSupplierInfo.PANNo), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Terms", (objSupplierInfo.Terms == "" ? "0" : objSupplierInfo.ParentSupID), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@OpeningBalance", (objSupplierInfo.OpeningBalance == "" ? "0" : objSupplierInfo.OpeningBalance), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AsOfDate", Validations.ConvertToDateReturn_ddMMyyyy_blank(objSupplierInfo.AsOfDate), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@ShippingStreet1", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingStreet2", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingCity", HttpUtility.HtmlEncode(objSupplierInfo.Shipping_City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingState", (objSupplierInfo.Shipping_State == "" ? "0" : objSupplierInfo.Shipping_State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ShippingCountry", (objSupplierInfo.Shipping_Country == "" ? "0" : objSupplierInfo.Shipping_Country), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@BillingStreet1", HttpUtility.HtmlEncode(objSupplierInfo.Billing_Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingStreet2", HttpUtility.HtmlEncode(objSupplierInfo.Billing_Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingCity", HttpUtility.HtmlEncode(objSupplierInfo.Billing_City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingState", (objSupplierInfo.Billing_State == "" ? "0" : objSupplierInfo.Billing_State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@BillingCountry", (objSupplierInfo.Billing_Country == "" ? "0" : objSupplierInfo.Billing_Country), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@IsActive", objSupplierInfo.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewSupID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion
    }
}