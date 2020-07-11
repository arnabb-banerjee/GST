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
        #region Function Related
        public static List<FunctionInfo> GetList_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("spURMSTFunctionsGet"))
            {
                DBHelper.AddPparameter("@ID", FunctionID);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<FunctionInfo> list = new List<FunctionInfo>();
                        FunctionInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new FunctionInfo();
                            obj.FunctionId = dr["FunctionId"].ToString();
                            obj.FunctionName = dr["FunctionName"].ToString();
                            obj.IsForModerate = dr["IsForModerate"].ToString();
                            obj.IsForMembership = dr["IsForMembership"].ToString();
                            obj.IsDesignation = dr["IsDesignation"].ToString();
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();
                            obj.IsDefaultForModerateUser = dr["IsDefaultForModerateUser"].ToString().Trim().ToUpper() == "Y";
                            obj.IsDefaultForRegisteredUser = dr["IsDefaultForRegisteredUser"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            if (!string.IsNullOrEmpty(FunctionID) && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
                            {
                                obj.RoleList = new List<RoleInfo>();
                                RoleInfo objRole = null;

                                foreach (DataRow drc in ds.Tables[1].Rows)
                                {
                                    objRole = new RoleInfo();

                                    objRole.RoleID = drc["RoleId"].ToString();
                                    objRole.RoleName = drc["RoleName"].ToString();
                                    objRole.isSelected = drc["IsSelected"].ToString().Trim().ToUpper() == "Y";
                                    objRole.IsOnlyForModerateUsers = drc["isOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";

                                    obj.RoleList.Add(objRole);
                                }
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                    else if (FunctionID.Trim() == "-1")
                    {
                        List<FunctionInfo> list = new List<FunctionInfo>();
                        FunctionInfo obj = null;

                        obj = new FunctionInfo();
                        obj.FunctionId = "0";
                        obj.FunctionName = "";
                        obj.IsForModerate = "";
                        obj.IsForMembership = "";
                        obj.IsDesignation = "";

                        if (!string.IsNullOrEmpty(FunctionID) && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
                        {
                            obj.RoleList = new List<RoleInfo>();
                            RoleInfo objRole = null;

                            foreach (DataRow drc in ds.Tables[1].Rows)
                            {
                                objRole = new RoleInfo();

                                objRole.RoleID = drc["RoleId"].ToString();
                                objRole.RoleName = drc["RoleName"].ToString();
                                objRole.isSelected = false;
                                objRole.IsOnlyForModerateUsers = drc["isOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";

                                obj.RoleList.Add(objRole);
                            }
                        }

                        list.Add(obj);

                        return list;
                    }
                }

                return null;
            }
        }

        public static FunctionInfo GetDetails_Function(string FunctionID, string OrganizationCode, bool IsActive)
        {
            if (!string.IsNullOrEmpty(FunctionID))
            {
                List<FunctionInfo> list = GetList_Function(FunctionID, OrganizationCode, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new FunctionInfo();
        }

        public static bool Save_Function(bool isOnlyDelete, FunctionInfo objFunctionInfo, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (objFunctionInfo.FunctionId.Trim() != "-1")
            {
                if (!Validations.ValidateDataType(objFunctionInfo.FunctionId, Validations.ValueType.Integer, true, "Id", out errormsg)) { return false; }
            }

            if (!Validations.ValidateDataType(objFunctionInfo.FunctionName, Validations.ValueType.AlphaNumericSpecialChar, true, "Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spURMSTFunctionsSave]", true))
            {
                DBHelper.AddPparameter("@FunctionId", objFunctionInfo.FunctionId, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@FunctionName", objFunctionInfo.FunctionName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsForModerate", objFunctionInfo.IsForModerate, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsForMembership", objFunctionInfo.IsForMembership, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsDesignation", objFunctionInfo.IsDesignation, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsDefaultForModerateUser", objFunctionInfo.IsDefaultForModerateUser ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsDefaultForRegisteredUser", objFunctionInfo.IsDefaultForRegisteredUser ? "Y" : "N", DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Roles", objFunctionInfo.Roles, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", objFunctionInfo.IsActive, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);

            }

        }
        #endregion
    }
}