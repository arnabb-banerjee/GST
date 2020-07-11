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
        #region Terms Related
        public static List<TermsInfo> GetList_Terms(string TermsID, string OrganisationCode)
        {
            List<TermsInfo> list = new List<TermsInfo>();

            using (DBHelper dbhlper = new DBHelper("spMSTTermsGet"))
            {
                DBHelper.AddPparameter("@ID", string.IsNullOrEmpty(TermsID) ? "0" : TermsID);
                DBHelper.AddPparameter("@OrganizationCode", string.IsNullOrEmpty(OrganisationCode) ? "" : OrganisationCode);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static TermsInfo GetDetails_Terms(string TermsID, string OrganisationCode)
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

        public static bool Save_Terms(bool isOnlyDelete, TermsInfo TermsInfo, UserInfo objUserInfo, out string errormsg)
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
                DBHelper.AddPparameter("@Id", (TermsInfo.Id == "" ? "0" : TermsInfo.Id), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Name", HttpUtility.HtmlEncode(TermsInfo.Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", HttpUtility.HtmlEncode(TermsInfo.OrganizationCode), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DueInFixedNumberDays", HttpUtility.HtmlEncode(TermsInfo.DueInFixedNumberDays), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DueInCertainDayOfMonth", HttpUtility.HtmlEncode(TermsInfo.DueInCertainDayOfMonth), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DueInNextMonth", HttpUtility.HtmlEncode(TermsInfo.DueInNextMonth), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Discount", HttpUtility.HtmlEncode(TermsInfo.Discount), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@IsActive", TermsInfo.IsActive, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", NewDatauniqueID, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }

        }
        #endregion

    }
}