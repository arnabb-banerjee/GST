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
        #region Tax Related
        #region Tax master
        public static List<TaxMasterInfo> GetList_TaxMaster(int mode, string TaxDefinationID)
        {
            using (DBHelper dbhlper = new DBHelper("spTaxMstGet"))
            {
                DBHelper.AddPparameter("@Mode", mode);
                DBHelper.AddPparameter("@TaxDefinationID", TaxDefinationID);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<TaxMasterInfo> list = new List<TaxMasterInfo>();
                        TaxMasterInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new TaxMasterInfo();
                            obj.TaxDefinationID = dr["TaxDefinationID"].ToString();
                            obj.TaxName = dr["TaxName"].ToString();

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static TaxMasterInfo GetDetails_TaxMaster(int mode, string TaxDefinationID)
        {
            if (!string.IsNullOrEmpty(TaxDefinationID))
            {
                List<TaxMasterInfo> list = GetList_TaxMaster(mode, TaxDefinationID);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            TaxMasterInfo obj = new TaxMasterInfo();
            obj.TaxDefinationID = "0";
            obj.TaxName = "";

            return obj;
        }

        public static bool Save_TaxMaster(bool isOnlyDelete, TaxMasterInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.TaxDefinationID, Validations.ValueType.Integer, true, "Tax", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.TaxName, Validations.ValueType.AlphaNumericSpecialChar, false, "Tax Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spTaxMstSave]", true))
            {
                DBHelper.AddPparameter("@TaxDefinationID", objBankInfo.TaxDefinationID.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.TaxDefinationID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@TaxName", objBankInfo.TaxName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Tax Country mapping
        public static List<TaxCountryMapInfo> GetList_TaxCountryMap(int mode, string TaxDefinationID, string CountryId)
        {
            using (DBHelper dbhlper = new DBHelper("spTaxMapCountryGet"))
            {
                DBHelper.AddPparameter("@Mode", mode);
                DBHelper.AddPparameter("@TaxDefinationID", TaxDefinationID);
                DBHelper.AddPparameter("@CountryId", CountryId);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<TaxCountryMapInfo> list = new List<TaxCountryMapInfo>();
                        TaxCountryMapInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new TaxCountryMapInfo();
                            obj.TaxDefinationID = dr["TaxDefinationID"].ToString();
                            obj.TaxName = dr["TaxName"].ToString();
                            obj.CountryId = dr["CountryId"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.ApplicableType = dr["ApplicableType"].ToString();
                            obj.IsExist = dr["IsExist"].ToString().Trim() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static TaxCountryMapInfo GetDetails_TaxCountryMap(int mode, string TaxDefinationID, string CountryId)
        {
            if (!string.IsNullOrEmpty(TaxDefinationID) && !string.IsNullOrEmpty(CountryId))
            {
                List<TaxCountryMapInfo> list = GetList_TaxCountryMap(mode, TaxDefinationID, CountryId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            TaxCountryMapInfo obj = new TaxCountryMapInfo();
            obj.TaxDefinationID = "0";
            obj.CountryId = "0";
            obj.CountryName = "";
            obj.TaxName = "";

            return obj;

        }

        public static bool Save_TaxCountryMap(bool isOnlyDelete, TaxCountryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.TaxDefinationID, Validations.ValueType.Integer, true, "Tax", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CountryId, Validations.ValueType.Integer, false, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.ApplicableType, Validations.ValueType.AlphaNumericSpecialChar, false, "Applicable Type", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spTaxMapCountrySave]", true))
            {
                DBHelper.AddPparameter("@TaxDefinationID", objBankInfo.TaxDefinationID.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.TaxDefinationID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@CountryId", objBankInfo.CountryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@ApplicableType", objBankInfo.ApplicableType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region Tax Country Categrory mapping
        public static List<TaxCountryCategoryMapInfo> GetList_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            using (DBHelper dbhlper = new DBHelper("spTaxMapCountryCategoryGet"))
            {
                DBHelper.AddPparameter("@Mode", mode);
                DBHelper.AddPparameter("@TaxDefinationID", TaxDefinationID);
                DBHelper.AddPparameter("@CategoryId", CategoryId);
                DBHelper.AddPparameter("@CountryId", CountryId);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<TaxCountryCategoryMapInfo> list = new List<TaxCountryCategoryMapInfo>();
                        TaxCountryCategoryMapInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new TaxCountryCategoryMapInfo();
                            obj.TaxDefinationID = dr["TaxDefinationID"].ToString();
                            obj.TaxName = dr["TaxName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.CountryId = dr["CountryId"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.Percentage = dr["Percentage"].ToString();
                            obj.ApplicableType = dr["ApplicableType"].ToString();
                            obj.IsExist = dr["IsExist"].ToString().Trim() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static TaxCountryCategoryMapInfo GetDetails_TaxCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            if (!string.IsNullOrEmpty(TaxDefinationID) && !string.IsNullOrEmpty(CountryId) && !string.IsNullOrEmpty(CategoryId))
            {
                List<TaxCountryCategoryMapInfo> list = GetList_TaxCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            TaxCountryCategoryMapInfo obj = new TaxCountryCategoryMapInfo();
            obj.TaxDefinationID = "0";
            obj.CategoryId = "0";
            obj.CountryId = "0";
            obj.CategoryName = "";
            obj.CountryName = "";
            obj.TaxName = "";

            return obj;
        }

        public static bool Save_TaxCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.TaxDefinationID, Validations.ValueType.Integer, true, "Tax", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CountryId, Validations.ValueType.Integer, false, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CategoryId, Validations.ValueType.Integer, false, "Category", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Percentage, Validations.ValueType.Numeric, true, "Percentage", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.ApplicableType, Validations.ValueType.AlphaNumericSpecialChar, false, "Applicable Type", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spTaxMapCountryCategorySave]", true))
            {
                DBHelper.AddPparameter("@TaxDefinationID", objBankInfo.TaxDefinationID.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.TaxDefinationID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@CategoryId", objBankInfo.CategoryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CountryId", objBankInfo.CountryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Percentage", objBankInfo.Percentage, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ApplicableType", objBankInfo.ApplicableType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        // Added by Partha on 10/07/2019
        #region Tax for expense Country Categrory mapping
        public static List<TaxCountryCategoryMapInfo> GetList_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            using (DBHelper dbhlper = new DBHelper("spTaxMapCountryCategoryExpenseGet"))
            {
                DBHelper.AddPparameter("@Mode", mode);
                DBHelper.AddPparameter("@TaxDefinationID", TaxDefinationID);
                DBHelper.AddPparameter("@CategoryId", CategoryId);
                DBHelper.AddPparameter("@CountryId", CountryId);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<TaxCountryCategoryMapInfo> list = new List<TaxCountryCategoryMapInfo>();
                        TaxCountryCategoryMapInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new TaxCountryCategoryMapInfo();
                            obj.TaxDefinationID = dr["TaxDefinationID"].ToString();
                            obj.TaxName = dr["TaxName"].ToString();
                            obj.CategoryId = dr["CategoryId"].ToString();
                            obj.CategoryName = dr["CategoryName"].ToString();
                            obj.CountryId = dr["CountryId"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();
                            obj.Percentage = dr["Percentage"].ToString();
                            obj.ApplicableType = dr["ApplicableType"].ToString();
                            obj.IsExist = dr["IsExist"].ToString().Trim() == "Y";

                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }

            return null;
        }

        public static TaxCountryCategoryMapInfo GetDetails_TaxExpenseCountryCategoryMap(int mode, string TaxDefinationID, string CountryId, string CategoryId)
        {
            if (!string.IsNullOrEmpty(TaxDefinationID) && !string.IsNullOrEmpty(CountryId) && !string.IsNullOrEmpty(CategoryId))
            {
                List<TaxCountryCategoryMapInfo> list = GetList_TaxExpenseCountryCategoryMap(mode, TaxDefinationID, CountryId, CategoryId);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            TaxCountryCategoryMapInfo obj = new TaxCountryCategoryMapInfo();
            obj.TaxDefinationID = "0";
            obj.CategoryId = "0";
            obj.CountryId = "0";
            obj.CategoryName = "";
            obj.CountryName = "";
            obj.TaxName = "";

            return obj;
        }

        public static bool Save_TaxExpenseCountryCategoryMap(bool isOnlyDelete, TaxCountryCategoryMapInfo objBankInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBankInfo.TaxDefinationID, Validations.ValueType.Integer, true, "Tax", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CountryId, Validations.ValueType.Integer, false, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.CategoryId, Validations.ValueType.Integer, false, "Category", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.Percentage, Validations.ValueType.Numeric, true, "Percentage", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBankInfo.ApplicableType, Validations.ValueType.AlphaNumericSpecialChar, false, "Applicable Type", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spTaxMapCountryCategoryExpenseSave]", true))
            {
                DBHelper.AddPparameter("@TaxDefinationID", objBankInfo.TaxDefinationID.Trim().Length > 0 ? Convert.ToInt32(objBankInfo.TaxDefinationID) : 0, DBHelper.param_types.Int);
                DBHelper.AddPparameter("@CategoryId", objBankInfo.CategoryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@CountryId", objBankInfo.CountryId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Percentage", objBankInfo.Percentage, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ApplicableType", objBankInfo.ApplicableType, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        // End: 10/07/2019
        #endregion
    }
}