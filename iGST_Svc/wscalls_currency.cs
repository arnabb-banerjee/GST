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
    public sealed partial class wsCurrency
    {
        public static List<CurrencyOrganiztionInfo> GetDetails_BUCurrencies(string OrganizationproductId, string CurrencyId, string OrganizationCode)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationcurrencyGet]"))
            {
                List<CurrencyOrganiztionInfo> list = new List<CurrencyOrganiztionInfo>();

                DBHelper.AddPparameter("@Mode", 0);
                DBHelper.AddPparameter("@OrganizationCurrencyId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                DBHelper.AddPparameter("@CurrencyId", CurrencyId);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        CurrencyOrganiztionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CurrencyOrganiztionInfo();
                            obj.OrganizationproductId = dr["OrganizationCurrencyId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganizationCurrencyId"]);
                            obj.CurrencyId = dr["CurrencyId"].ToString();
                            obj.Symbol = dr["symbol"].ToString();
                            obj.CurrencyName = dr["name"].ToString();
                            obj.isSelected = dr["isExist"].ToString().Trim().ToUpper() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }
        public static List<CurrencyOrganiztionInfo> GetDetails_CurrencyOrganization(string OrganizationproductId, string CurrencyId, string OrganizationCode)
        {
            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationcurrencyGet]"))
            {
                List<CurrencyOrganiztionInfo> list = new List<CurrencyOrganiztionInfo>();

                DBHelper.AddPparameter("@Mode", 1);
                DBHelper.AddPparameter("@OrganizationCurrencyId", OrganizationproductId.Trim().Length > 0 ? Convert.ToInt32(OrganizationproductId) : 0);
                DBHelper.AddPparameter("@CurrencyId", CurrencyId);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        CurrencyOrganiztionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new CurrencyOrganiztionInfo();
                            obj.OrganizationproductId = dr["OrganizationCurrencyId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganizationCurrencyId"]);
                            obj.CurrencyId = dr["CurrencyId"].ToString();
                            obj.Symbol = dr["symbol"].ToString();
                            obj.CurrencyName = dr["name"].ToString();
                            obj.isSelected = dr["isExist"].ToString().Trim().ToUpper() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static bool Save_CurrencyOrganization(bool isOnlyDelete, CurrencyOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            bool flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CurrencyId, Validations.ValueType.Alphabet, true, "Currency Info", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMAPOrganizationcurrencySave]", true))
            {
                DBHelper.AddPparameter("@OrganizationCurrencyId", obj.OrganizationproductId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CurrencyId", obj.CurrencyId.Trim().Length > 0 ? obj.CurrencyId : "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", obj.OrganizationCode.Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

    }
}