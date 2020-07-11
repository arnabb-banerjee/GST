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
    public sealed partial class wsCustomer
    {
        #region Customer Related
        public static byte[] GetExcel_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
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

        public static List<CustomerInfo> GetList_CustomerDropdownlist(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spCusMstCustomerGet"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@UserType", UserType);
                DBHelper.AddPparameter("@CusID", CusID);
                DBHelper.AddPparameter("@BranchId", BranchId);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@UserID", UserID);
                DBHelper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static List<CustomerInfo> GetList_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("spCusMstCustomerGet"))
            {
                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@UserType", UserType);
                DBHelper.AddPparameter("@CusID", CusID);
                DBHelper.AddPparameter("@BranchId", BranchId);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@UserID", UserID);
                DBHelper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = DBHelper.Execute_Query())
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
                            obj.Notes = dr["Notes"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static CustomerInfo GetDetails_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
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

        //public static bool Upload_Customer(string UserType, bool isOvereWrite, DataSet ds, string OrganizationCode, UserInfo objUserInfo, out bool bReturn, out string errormsg)
        //{
        //    bReturn = false;
        //    errormsg = "";

        //    if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return null; }

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        #region Validation - Column existance
        //        if (ds.Tables[0].Columns.IndexOf("First Name") < 0) { errormsg = "[First Name] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Last Name") < 0) { errormsg = "[Last Name] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Company Name") < 0) { errormsg = "[Company Name] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Email") < 0) { errormsg = "[Email] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Mobile") < 0) { errormsg = "[Mobile] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("FAX") < 0) { errormsg = "[FAX] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Street") < 0) { errormsg = "[Street] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("City") < 0) { errormsg = "[City] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("State") < 0) { errormsg = "[State] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Country") < 0) { errormsg = "[Country] column is not available"; }
        //        //else if (ds.Tables[0].Columns.IndexOf("Opening Balance") < 0) { errormsg = "[Opening Balance] column is not available"; }
        //        //else if (ds.Tables[0].Columns.IndexOf("As Of Date") < 0) { errormsg = "[As Of Date] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("GST Registration Type") < 0) { errormsg = "[GST Registration Type] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("GSTIN Number") < 0) { errormsg = "[GSTIN Number] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Tax Reg No") < 0) { errormsg = "[Tax Reg.No.] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Cst Reg No") < 0) { errormsg = "[Cst Reg. No.] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("PAN Number") < 0) { errormsg = "[PAN Number] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Terms") < 0) { errormsg = "[Terms] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Effective Date") < 0) { errormsg = "[Effective Date] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Prefered Delivery Method") < 0) { errormsg = "[Prefered Delivery Method] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Prefered Paymant Method") < 0) { errormsg = "[Prefered Paymant Method] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Parent Customer") < 0) { errormsg = "[Parent Customer] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Billing Street1") < 0) { errormsg = "[Billing Street1] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Billing City") < 0) { errormsg = "[Billing City] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Billing State") < 0) { errormsg = "[Billing State] column is not available"; }
        //        else if (ds.Tables[0].Columns.IndexOf("Billing Country") < 0) { errormsg = "[Billing Country] column is not available"; }
        //        #endregion

        //        else
        //        {
        //            using (DBHelper dbhlper = new DBHelper("[spCUSMSTCustomerSave]", true))
        //            {
        //                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    #region Validation - Data Type
        //                    if (!Validations.ValidateDataType(dr["First Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "First Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Middle Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Last Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Last Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Company Name"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Company Name", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Email"].ToString(), Validations.ValueType.Email, true, "Email", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Mobile"].ToString(), Validations.ValueType.MobileNumber, true, "Mobile", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["FAX"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "FAX", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Street"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Street", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Parent Customer"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Parent Customer", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Opening Balance"].ToString(), Validations.ValueType.Numeric, true, "Opening Balance", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["As Of Date"].ToString(), Validations.ValueType.DateTime, true, "As Of Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["GST Registration Type"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GST Registration Type", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["GSTIN Number"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "GSTIN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Tax Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Tax Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Cst Reg No"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Cst Reg.No.", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["PAN Number"].ToString(), Validations.ValueType.AlphaNumeric, true, "PAN Number", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Terms"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Terms", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Effective Date"].ToString(), Validations.ValueType.DateTime, true, "Effective Date", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Prefered Delivery Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Delivery Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Prefered Paymant Method"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Prefered Paymant Method", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Billing Street1"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Street1", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Billing City"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing City", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Billing State"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing State", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    if (!Validations.ValidateDataType(dr["Billing Country"].ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "Billing Country", out errormsg)) { dr["Error Message"] = errormsg; DisplayErroeMessage = true; }
        //                    #endregion

        //                    DBHelper.ClearParameters();

        //                    #region Parameter Add
        //                    DBHelper.AddPparameter("@UserType", UserType.Trim(), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@CusID", "0", DBHelper.param_types.BigInt);
        //                    DBHelper.AddPparameter("@Title", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(dr["First Name"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(dr["Middle Name"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@LastName", HttpUtility.HtmlEncode(dr["Last Name"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Safix", HttpUtility.HtmlEncode(dr["Last Name"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Sex", HttpUtility.HtmlEncode(dr["Last Name"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(dr["Last Name"].ToString()), DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@EmailID", HttpUtility.HtmlEncode(dr["Email"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Mobile", HttpUtility.HtmlEncode(dr["Mobile"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Street1", HttpUtility.HtmlEncode(dr["Street"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Street2", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@City", HttpUtility.HtmlEncode(dr["City"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@State", HttpUtility.HtmlEncode(dr["State"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Country", HttpUtility.HtmlEncode(dr["Country"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Website", "", DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@GSTRegistrationType", HttpUtility.HtmlEncode(dr["GST Registration Type"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@GSTIN", HttpUtility.HtmlEncode(dr["GSTIN Number"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@IsSubCustomer", (HttpUtility.HtmlEncode(dr["Parent Customer"].ToString()).Trim().Length > 0 ? "Y" : ""), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@ParentCusID", HttpUtility.HtmlEncode(dr["Parent Customer"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@BillingWith", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Notes", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@PrefferedPaymentMethod", HttpUtility.HtmlEncode(dr["Prefered Delivery Method"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@PrefferedDeliveryMethod", HttpUtility.HtmlEncode(dr["Prefered Paymant Method"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@TaxRegNo", HttpUtility.HtmlEncode(dr["Tax Reg No"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@CSTRegNo", HttpUtility.HtmlEncode(dr["Cst Reg No"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@PANNo", HttpUtility.HtmlEncode(dr["PAN Number"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@Terms", "", DBHelper.param_types.BigInt);

        //                    DBHelper.AddPparameter("@OpeningBalance", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@AsOfDate", "", DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@ShippingStreet1", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@ShippingStreet2", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@ShippingCity", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@ShippingState", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@ShippingCountry", "", DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@BillingStreet1", HttpUtility.HtmlEncode(dr["Billing Street1"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@BillingStreet2", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@BillingCity", HttpUtility.HtmlEncode(dr["Billing City"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@BillingState", HttpUtility.HtmlEncode(dr["Billing State"].ToString()), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@BillingCountry", HttpUtility.HtmlEncode(dr["Billing Country"].ToString()), DBHelper.param_types.Varchar);

        //                    DBHelper.AddPparameter("@IsActive", "Y", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@isOnlyDelete", "", DBHelper.param_types.Varchar);
        //                    DBHelper.AddPparameter("@NewCusID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
        //                    DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
        //                    DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
        //                    #endregion

        //                    if (DBHelper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg) == false)
        //                    {
        //                        return false;
        //                    }
        //                }

        //                DBHelper.CloseConnection(true);
        //            }
        //        }

        //        if (errormsg.Trim().Length == 0)
        //            errormsg = "No data is available to be uploaded";
        //    }

        //    return false;
        //}

        public static DataSet Upload_Customer(string UserType, bool isOvereWrite, DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg)
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
                            DBHelper.AddPparameter("@CustomerData", ds.Tables[0], DBHelper.param_types.Structured);
                            DBHelper.AddPparameter("@UserType", UserType.Trim().ToUpper(), DBHelper.param_types.Varchar);
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

        public static bool Save_Customer(string UserType, bool isOnlyDelete, CustomerInfo objCustomerInfo, string UserCode, out string errormsg)
        {
            errormsg = "";
            bool flag = false;
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
                DBHelper.AddPparameter("@UserType", UserType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CusID", (objCustomerInfo.CusID == "" ? "0" : objCustomerInfo.CusID), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Title", HttpUtility.HtmlEncode(objCustomerInfo.Title), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objCustomerInfo.First_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objCustomerInfo.Middle_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objCustomerInfo.Last_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objCustomerInfo.Safix), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objCustomerInfo.Sex), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objCustomerInfo.DOB), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(objCustomerInfo.OrganizationCode), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@EmailID", objCustomerInfo.EmailID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Mobile", objCustomerInfo.Mobile, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objCustomerInfo.Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objCustomerInfo.Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", HttpUtility.HtmlEncode(objCustomerInfo.City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", (objCustomerInfo.State == "" ? "0" : objCustomerInfo.State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Country", (objCustomerInfo.Country == "" ? "0" : objCustomerInfo.Country), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Website", HttpUtility.HtmlEncode(objCustomerInfo.Website), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@GSTRegistrationType", (objCustomerInfo.GSTRegistrationType == "" ? "0" : objCustomerInfo.GSTRegistrationType), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@GSTIN", HttpUtility.HtmlEncode(objCustomerInfo.GSTIN), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsSubCustomer", (objCustomerInfo.IsSubCustomer ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ParentCusID", (objCustomerInfo.ParentCusID == "" ? "0" : objCustomerInfo.ParentCusID), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@BillingWith", HttpUtility.HtmlEncode(objCustomerInfo.BillingWith), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objCustomerInfo.Notes), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PrefferedPaymentMethod", HttpUtility.HtmlEncode(objCustomerInfo.PrefferedPaymentMethod), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PrefferedDeliveryMethod", HttpUtility.HtmlEncode(objCustomerInfo.PrefferedDeliveryMethod), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@TaxRegNo", HttpUtility.HtmlEncode(objCustomerInfo.TaxRegNo), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CSTRegNo", objCustomerInfo.CSTRegNo, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PANNo", HttpUtility.HtmlEncode(objCustomerInfo.PANNo), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Terms", (objCustomerInfo.Terms == "" ? "0" : objCustomerInfo.ParentCusID), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@OpeningBalance", (objCustomerInfo.OpeningBalance == "" ? "0" : objCustomerInfo.OpeningBalance), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AsOfDate", Validations.ConvertToDateReturn_ddMMyyyy_blank(objCustomerInfo.AsOfDate), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@ShippingStreet1", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingStreet2", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingCity", HttpUtility.HtmlEncode(objCustomerInfo.Shipping_City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ShippingState", (objCustomerInfo.Shipping_State == "" ? "0" : objCustomerInfo.Shipping_State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ShippingCountry", (objCustomerInfo.Shipping_Country == "" ? "0" : objCustomerInfo.Shipping_Country), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@BillingStreet1", HttpUtility.HtmlEncode(objCustomerInfo.Billing_Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingStreet2", HttpUtility.HtmlEncode(objCustomerInfo.Billing_Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingCity", HttpUtility.HtmlEncode(objCustomerInfo.Billing_City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BillingState", (objCustomerInfo.Billing_State == "" ? "0" : objCustomerInfo.Billing_State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@BillingCountry", (objCustomerInfo.Billing_Country == "" ? "0" : objCustomerInfo.Billing_Country), DBHelper.param_types.BigInt);

                DBHelper.AddPparameter("@IsActive", objCustomerInfo.IsActive ? "Y" : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewCusID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static bool Save_CustomerImage(bool isOnlyDelete, CustomerImageInfo obj, string UserCode, out string errormsg)
        {
            errormsg = "";
            bool flag = false;

            #region Validations
            if (isOnlyDelete)
            {
                if (obj.ImageId < 1) { errormsg = "Image identity should come"; return false; }
            }
            else
            {
                if (obj.CustomerId < 1)
                {
                    errormsg = "Customer information is not available"; return false;
                }
                if (!Validations.ValidateDataType(obj.FileName, Validations.ValueType.AlphaNumericSpecialChar, false, "File Name", out errormsg)) { return false; }
                if (!Validations.ValidateDataType(obj.FileType, Validations.ValueType.AlphaNumericSpecialChar, false, "File Type", out errormsg)) { return false; }
            }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spCUSMSTCustomerImageSave]", true))
            {
                DBHelper.AddPparameter("@ImageId", obj.ImageId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CustomerId", Convert.ToInt32(obj.CustomerId), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@FileData", obj.FileData == null ? new byte[] { } : obj.FileData, DBHelper.param_types.Image);
                DBHelper.AddPparameter("@FileName", obj.FileName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FileType", obj.FileType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Seq", obj.Seq, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsMain", obj.Ismain.Trim().ToUpper(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", obj.IsActive.Trim().ToUpper(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<CustomerImageInfo> GetList_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain)
        {
            using (DBHelper dbhlper = new DBHelper("[spCUSMSTCustomerImageGet]"))
            {
                DBHelper.AddPparameter("@ImageId", ImageId);
                DBHelper.AddPparameter("@CustomerId", Convert.ToInt32(CustomerID));
                DBHelper.AddPparameter("@IsActive", IsActive);
                DBHelper.AddPparameter("@IsMain", IsMain);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<CustomerImageInfo> list = new List<CustomerImageInfo>();
                        CustomerImageInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CustomerImageInfo();
                            obj.ImageId = dr["ImageId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ImageId"]);
                            obj.CustomerId = Convert.ToInt32(dr["CustomerId"]);
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

        public static CustomerImageInfo GetDetails_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain)
        {
            var list = GetList_CustomerImage(ImageId, CustomerID, IsActive, IsMain);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            CustomerImageInfo objProductOrganiztionImageInfo = new CustomerImageInfo();
            objProductOrganiztionImageInfo.ImageId = 0;
            objProductOrganiztionImageInfo.IsActive = "";
            objProductOrganiztionImageInfo.Ismain = "";
            objProductOrganiztionImageInfo.CustomerId = 0;
            return objProductOrganiztionImageInfo;
        }
        #endregion

    }
}