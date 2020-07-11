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
        #region Language Related
        public static List<LanguageInfo> GetList_Language(string LanguageID, string LanguageName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[splangMSTLanguageGet]"))
            {
                DBHelper.AddPparameter("@LanguageId", LanguageID);
                DBHelper.AddPparameter("@LanguageName", LanguageName);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static LanguageInfo GetDetails_Language(string LanguageID, string LanguageName, bool IsActive)
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

        public static bool Save_Language(bool isOnlyDelete, LanguageInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.LanguageName, Validations.ValueType.AlphaNumericSpecialChar, true, "Language Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spLangMSTLanguageSave]", true))
            {
                DBHelper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LanguageName", obj.LanguageName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<LanguageValueInfo> GetList_DataValueLanguageWise(string MasterTablePrefixs, string LanguageIds, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[splangValuGet]"))
            {
                DBHelper.AddPparameter("@MasterIDField", "");
                DBHelper.AddPparameter("@MasterTablePrefixs", MasterTablePrefixs);
                DBHelper.AddPparameter("@LanguageIds", LanguageIds);
                DBHelper.AddPparameter("@IsActive", "");

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static bool Save_DataValueLanguageWise(bool isOnlyDelete, LanguageValueInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[splangValuSave]", true))
            {
                DBHelper.AddPparameter("@MasterIDField", obj.MasterIDField, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@MasterTablePrefix", obj.MasterTablePrefix, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Value", obj.value, DBHelper.param_types.NVarchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static List<LanguageCountryInfo> GetList_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spLanguageCountryGet]"))
            {
                DBHelper.AddPparameter("@Id", ID.Trim().Length > 0 ? ID : "0");
                DBHelper.AddPparameter("@LanguageId", LanguageID);
                DBHelper.AddPparameter("@CountryId", CountryID);
                DBHelper.AddPparameter("@Visibility", Visibility);

                using (DataSet ds = DBHelper.Execute_Query())
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

        public static LanguageCountryInfo GetDetails_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, bool IsActive)
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

        public static bool Save_LanguageCountry(bool isOnlyDelete, LanguageCountryInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.LanguageId, Validations.ValueType.Integer, true, "Language Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.CountryId, Validations.ValueType.Integer, true, "Countrry Id", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spLanguageCountrySave]", true))
            {
                DBHelper.AddPparameter("@Id", obj.Id, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@LanguageId", obj.LanguageId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CountryId", obj.CountryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Visibility", (obj.Visibility ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Priority", obj.Proirity, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        #endregion
    }
}