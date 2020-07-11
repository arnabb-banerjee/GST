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
        #region Organization Related
        public static List<OrganizationInfo> GetList_OrganizationDropdownList(string OrganizationCode, string isActive, string UserCode)
        {
            using (DBHelper dbhlper = new DBHelper("[spURMSTOrganizationGet]"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@IsActive", isActive.Trim());
                DBHelper.AddPparameter("@UserCode", UserCode);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static List<OrganizationInfo> GetList_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId)
        {
            using (DBHelper dbhlper = new DBHelper("sporganizationget"))
            {
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@OrganizationName", OrganizationName);
                DBHelper.AddPparameter("@City", City);
                DBHelper.AddPparameter("@State", State);
                DBHelper.AddPparameter("@Country", Country);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@UserID", UserID);
                DBHelper.AddPparameter("@LanguageId", LanguageId);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static OrganizationInfo GetDetails_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId)
        {
            List<OrganizationInfo> list = GetList_Organization(OrganizationCode, OrganizationName, City, State, Country, UserID, IsActive, LanguageId);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new OrganizationInfo();
        }

        public static bool Save_Organization(bool isOnlyDelete, OrganizationInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.OrganizationName, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization Name", out errormsg)) { return false; }

            using (DBHelper dbhlper = new DBHelper("spOrganizationsave", true))
            {
                DBHelper.AddPparameter("@OrganizationCode", obj.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationName", obj.OrganizationName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Organization Accountant Related
        public static List<OrganizationAccountantInfo> GetList_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            using (DBHelper dbhlper = new DBHelper("[spMapOrganizationAccountantGet]"))
            {
                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@ID", ID.Trim().Length > 0 ? ID.Trim() : "0");
                DBHelper.AddPparameter("@AccountantCode", AccountantCode);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static List<OrganizationAccountantInfo> GetDetails_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            using (DBHelper dbhlper = new DBHelper("[spMapOrganizationAccountantGet]"))
            {
                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@ID", ID.Trim().Length > 0 ? ID.Trim() : "0");
                DBHelper.AddPparameter("@AccountantCode", AccountantCode);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static bool Save_OrganizationAccountant(bool isOnlyDelete, OrganizationAccountantInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.AccountantCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Accountant", out errormsg)) { return false; }

            using (DBHelper dbhlper = new DBHelper("spMapOrganizationAccountantSave", true))
            {
                DBHelper.AddPparameter("@ID", obj.ID.Trim().Length > 0 ? obj.ID : "0", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AccountantCode", obj.AccountantCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", obj.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Organization Dashboard
        public static List<OrganizationDashBoardInfo> GetDashboard_Organization(string OrganizationCode, string FromDate, string ToDate, string Type)
        {
            using (DBHelper dbhlper = new DBHelper("[DashBoardValues]"))
            {
                DBHelper.AddPparameter("@FromDate", FromDate);
                DBHelper.AddPparameter("@ToDate", ToDate);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
                DBHelper.AddPparameter("@Type", Type);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<OrganizationDashBoardInfo> list = new List<OrganizationDashBoardInfo>();
                        OrganizationDashBoardInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new OrganizationDashBoardInfo();
                            obj.Type = dr["Type"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.Months = dr["Months"] == DBNull.Value ? 0 : Convert.ToInt16(dr["Months"]);
                            obj.NoOfInvoice = dr["NoOfInvoice"] == DBNull.Value ? 0 : Convert.ToInt16(dr["NoOfInvoice"]);
                            obj.PricePerUnit = dr["PricePerUnit"] == DBNull.Value ? 0.00 : Convert.ToDouble(dr["PricePerUnit"]);
                            obj.Quantity = dr["Quantity"] == DBNull.Value ? 0.00 : Convert.ToDouble(dr["Quantity"]);
                            obj.TotalAmount = dr["TotalAmount"] == DBNull.Value ? 0.00 : Convert.ToDouble(dr["TotalAmount"]);
                            obj.PricePerUnit = dr["TotalTax"] == DBNull.Value ? 0.00 : Convert.ToDouble(dr["TotalTax"]);
                            obj.AmountIncludeTax = dr["AmountIncludeTax"] == DBNull.Value ? 0 : Convert.ToInt16(dr["AmountIncludeTax"]);

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            var list1 = new List<OrganizationDashBoardInfo>();
            list1.Add(new OrganizationDashBoardInfo());
            return list1;
        }

        #endregion
    }
}