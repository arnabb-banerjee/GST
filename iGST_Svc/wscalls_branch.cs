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
        public static List<BranchInfo> GetList_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spMSTBranchGet"))
            {
                DBHelper.AddPparameter("@ID", BranchID);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<BranchInfo> list = new List<BranchInfo>();
                        BranchInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new BranchInfo();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();
                            obj.BranchID = dr["BranchID"].ToString();
                            obj.BranchName = dr["BranchName"].ToString();
                            obj.IsMainBranch = dr["IsMainBranch"].ToString();
                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();

                            if (dr["LastModifiedOn"] != DBNull.Value)
                            {
                                obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            }

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

        public static BranchInfo GetDetails_Branch(string BranchID, string OrganizationCode, string IsMainBranch, bool IsActive)
        {
            if (!string.IsNullOrEmpty(BranchID))
            {
                List<BranchInfo> list = GetList_Branch(BranchID, OrganizationCode, IsMainBranch, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new BranchInfo();
        }

        public static bool Save_Branch(bool isOnlyDelete, BranchInfo objBranchInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(objBranchInfo.BranchID, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.BranchName, Validations.ValueType.AlphaNumericSpecialChar, false, "Name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.City.ToString(), Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objBranchInfo.OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "City", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTBranchSave]", true))
            {
                DBHelper.AddPparameter("@BranchID", objBranchInfo.BranchID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BranchName", objBranchInfo.BranchName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", objBranchInfo.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsMainBranch", objBranchInfo.IsMainBranch, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street1", objBranchInfo.Street1, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street2", objBranchInfo.Street2, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", objBranchInfo.City, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", objBranchInfo.State, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Country", objBranchInfo.Country, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@PIN", objBranchInfo.PIN, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", objBranchInfo.IsActive, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        //public static List<BranchProductMappingInfo> GetList_BranchProductMapping(string OrganizationCode, string BranchID, string LanguageId)
        //{
        //    using (DBHelper dbhlper = new DBHelper("spBranchProductMappingGet"))
        //    {
        //        DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);
        //        DBHelper.AddPparameter("@BranchID", BranchID);
        //        DBHelper.AddPparameter("@LanguageId", LanguageId);

        //        using (DataSet ds = DBHelper.Execute_Query())
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                List<BranchProductMappingInfo> list = new List<BranchProductMappingInfo>();
        //                BranchProductMappingInfo obj = null;

        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    obj = new BranchProductMappingInfo();
        //                    obj.ProductId = dr["ProductID"].ToString();
        //                    obj.ProductName = dr["ProductName"].ToString();
        //                    obj.AllreadyExists = dr["AllreadyExists"].ToString();

        //                    list.Add(obj);
        //                }

        //                return list;
        //            }
        //        }
        //    }

        //    return null;
        //}

        public static bool Save_BranchProductMapping(bool isOnlyDelete, string OrganizationCode, string ProductIds, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(OrganizationCode, Validations.ValueType.AlphaNumericSpecialChar, false, "Organization", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(ProductIds, Validations.ValueType.AlphaNumericSpecialChar, false, "Products", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("spMasterDataMultiLanguageSave", true))
            {
                DBHelper.AddPparameter("@ProductIds", ProductIds, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BranchID", "", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
    }
}