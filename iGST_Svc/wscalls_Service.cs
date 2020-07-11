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
        #region ServiceClass Related
        public static List<ServiceClassInfo> GetList_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceClassGet]"))
            {
                DBHelper.AddPparameter("@ServiceClassId", ServiceClassID);
                DBHelper.AddPparameter("@ServiceClassName", ServiceClassName);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceClassInfo> list = new List<ServiceClassInfo>();
                        ServiceClassInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceClassInfo();
                            obj.ServiceClassId = dr["ServiceClassID"].ToString();
                            obj.ServiceClassName = dr["ServiceClassName"].ToString();
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

        public static ServiceClassInfo GetDetails_ServiceClass(string ServiceClassID, string ServiceClassName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceClassID))
            {
                List<ServiceClassInfo> list = GetList_ServiceClass(ServiceClassID, ServiceClassName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceClassInfo();
        }

        public static bool Save_ServiceClass(bool isOnlyDelete, ServiceClassInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceClassId, Validations.ValueType.Integer, true, "ServiceClass Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceClassName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceClass Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceClassSave]", true))
            {
                DBHelper.AddPparameter("@ServiceClassId", obj.ServiceClassId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ServiceClassName", obj.ServiceClassName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region ServiceType Related
        public static List<ServiceTypeInfo> GetList_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceTypeGet]"))
            {
                DBHelper.AddPparameter("@ServiceTypeId", ServiceTypeID);
                DBHelper.AddPparameter("@ServiceTypeName", ServiceTypeName);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceTypeInfo> list = new List<ServiceTypeInfo>();
                        ServiceTypeInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceTypeInfo();
                            obj.ServiceTypeId = dr["ServiceTypeID"].ToString();
                            obj.ServiceTypeName = dr["ServiceTypeName"].ToString();
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

        public static ServiceTypeInfo GetDetails_ServiceType(string ServiceTypeID, string ServiceTypeName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceTypeID))
            {
                List<ServiceTypeInfo> list = GetList_ServiceType(ServiceTypeID, ServiceTypeName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceTypeInfo();
        }

        public static bool Save_ServiceType(bool isOnlyDelete, ServiceTypeInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceTypeId, Validations.ValueType.Integer, true, "ServiceType Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceTypeName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceType Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceTypeSave]", true))
            {
                DBHelper.AddPparameter("@ServiceTypeId", obj.ServiceTypeId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ServiceTypeName", obj.ServiceTypeName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region ServiceUnit Related
        public static List<ServiceUnitInfo> GetList_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("[spMSTServiceUnitGet]"))
            {
                DBHelper.AddPparameter("@ServiceUnitId", ServiceUnitID);
                DBHelper.AddPparameter("@ServiceUnitName", ServiceUnitName);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<ServiceUnitInfo> list = new List<ServiceUnitInfo>();
                        ServiceUnitInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new ServiceUnitInfo();
                            obj.ServiceUnitId = dr["ServiceUnitID"].ToString();
                            obj.ServiceUnitName = dr["ServiceUnitName"].ToString();
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

        public static ServiceUnitInfo GetDetails_ServiceUnit(string ServiceUnitID, string ServiceUnitName, bool IsActive)
        {
            if (!string.IsNullOrEmpty(ServiceUnitID))
            {
                List<ServiceUnitInfo> list = GetList_ServiceUnit(ServiceUnitID, ServiceUnitName, IsActive);

                if (list != null && list.Count() > 0)
                {
                    return list[0];
                }
            }

            return new ServiceUnitInfo();
        }

        public static bool Save_ServiceUnit(bool isOnlyDelete, ServiceUnitInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(obj.ServiceUnitId, Validations.ValueType.Integer, true, "ServiceUnit Id", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.ServiceUnitName, Validations.ValueType.AlphaNumericSpecialChar, true, "ServiceUnit Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spMSTServiceUnitSave]", true))
            {
                DBHelper.AddPparameter("@ServiceUnitId", obj.ServiceUnitId, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ServiceUnitName", obj.ServiceUnitName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? "Y" : "N"), DBHelper.param_types.Varchar);
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