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
        #region Role Related
        public static List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            using (DBHelper dbhlper = new DBHelper("SPURMSTROLEGET"))
            {
                DBHelper.AddPparameter("@RoleID", !string.IsNullOrEmpty(RoleID) ? Convert.ToInt64(RoleID) : 0);
                DBHelper.AddPparameter("@BranchId", BranchId);
                DBHelper.AddPparameter("@IsActive", (IsActive ? "Y" : ""));
                DBHelper.AddPparameter("@UserID", UserID);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<RoleInfo> list = new List<RoleInfo>();
                        RoleInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            #region Read Object
                            obj = new RoleInfo();
                            obj.RoleID = dr["RoleID"].ToString();
                            obj.RoleName = HttpUtility.HtmlEncode(dr["RoleName"].ToString());

                            obj.IsOnlyForModerateUsers = dr["IsOnlyForModerateUsers"].ToString().Trim().ToUpper() == "Y";
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";

                            obj.Category_Audit = dr["CanManageCategory"].ToString().Contains("5");
                            obj.Category_View = dr["CanManageCategory"].ToString().Contains("1");
                            obj.Category_Add = dr["CanManageCategory"].ToString().Contains("2");
                            obj.Category_Edit = dr["CanManageCategory"].ToString().Contains("3");
                            obj.Category_Delete = dr["CanManageCategory"].ToString().Contains("4");

                            obj.Product_Audit = dr["CanManageProduct"].ToString().Contains("5");
                            obj.Product_View = dr["CanManageProduct"].ToString().Contains("1");
                            obj.Product_Add = dr["CanManageProduct"].ToString().Contains("2");
                            obj.Product_Edit = dr["CanManageProduct"].ToString().Contains("3");
                            obj.Product_Delete = dr["CanManageProduct"].ToString().Contains("4");

                            obj.Role_Audit = dr["CanManageRole"].ToString().Contains("5");
                            obj.Role_View = dr["CanManageRole"].ToString().Contains("1");
                            obj.Role_Add = dr["CanManageRole"].ToString().Contains("2");
                            obj.Role_Edit = dr["CanManageRole"].ToString().Contains("3");
                            obj.Role_Delete = dr["CanManageRole"].ToString().Contains("4");

                            obj.Function_Audit = dr["CanManageFunction"].ToString().Contains("5");
                            obj.Function_View = dr["CanManageFunction"].ToString().Contains("1");
                            obj.Function_Add = dr["CanManageFunction"].ToString().Contains("2");
                            obj.Function_Edit = dr["CanManageFunction"].ToString().Contains("3");
                            obj.Function_Delete = dr["CanManageFunction"].ToString().Contains("4");

                            obj.UserRegistered_Audit = dr["CanManageUserRegistered"].ToString().Contains("5");
                            obj.UserRegistered_View = dr["CanManageUserRegistered"].ToString().Contains("1");
                            obj.UserRegistered_Add = dr["CanManageUserRegistered"].ToString().Contains("2");
                            obj.UserRegistered_Edit = dr["CanManageUserRegistered"].ToString().Contains("3");
                            obj.UserRegistered_Delete = dr["CanManageUserRegistered"].ToString().Contains("4");

                            obj.UserModerate_Audit = dr["CanManageUserModerate"].ToString().Contains("5");
                            obj.UserModerate_View = dr["CanManageUserModerate"].ToString().Contains("1");
                            obj.UserModerate_Add = dr["CanManageUserModerate"].ToString().Contains("2");
                            obj.UserModerate_Edit = dr["CanManageUserModerate"].ToString().Contains("3");
                            obj.UserModerate_Delete = dr["CanManageUserModerate"].ToString().Contains("4");

                            obj.Organization_Audit = dr["CanManageOrganization"].ToString().Contains("5");
                            obj.Organization_View = dr["CanManageOrganization"].ToString().Contains("1");
                            obj.Organization_Add = dr["CanManageOrganization"].ToString().Contains("2");
                            obj.Organization_Edit = dr["CanManageOrganization"].ToString().Contains("3");
                            obj.Organization_Delete = dr["CanManageOrganization"].ToString().Contains("4");

                            obj.Branch_Audit = dr["CanManageBranch"].ToString().Contains("5");
                            obj.Branch_View = dr["CanManageBranch"].ToString().Contains("1");
                            obj.Branch_Add = dr["CanManageBranch"].ToString().Contains("2");
                            obj.Branch_Edit = dr["CanManageBranch"].ToString().Contains("3");
                            obj.Branch_Delete = dr["CanManageBranch"].ToString().Contains("4");

                            obj.Customer_Audit = dr["CanManageCustomer"].ToString().Contains("5");
                            obj.Customer_View = dr["CanManageCustomer"].ToString().Contains("1");
                            obj.Customer_Add = dr["CanManageCustomer"].ToString().Contains("2");
                            obj.Customer_Edit = dr["CanManageCustomer"].ToString().Contains("3");
                            obj.Customer_Delete = dr["CanManageCustomer"].ToString().Contains("4");

                            obj.Bill_Audit = dr["CanManageBill"].ToString().Contains("5");
                            obj.Bill_View = dr["CanManageBill"].ToString().Contains("1");
                            obj.Bill_Add = dr["CanManageBill"].ToString().Contains("2");
                            obj.Bill_Edit = dr["CanManageBill"].ToString().Contains("3");
                            obj.Bill_Delete = dr["CanManageBill"].ToString().Contains("4");

                            obj.Language_Audit = dr["CanManageLanguage"].ToString().Contains("5");
                            obj.Language_View = dr["CanManageLanguage"].ToString().Contains("1");
                            obj.Language_Add = dr["CanManageLanguage"].ToString().Contains("2");
                            obj.Language_Edit = dr["CanManageLanguage"].ToString().Contains("3");
                            obj.Language_Delete = dr["CanManageLanguage"].ToString().Contains("4");

                            obj.DefineLanguage_Audit = dr["CanManageDefineLanguage"].ToString().Contains("5");
                            obj.DefineLanguage_View = dr["CanManageDefineLanguage"].ToString().Contains("1");
                            obj.DefineLanguage_Add = dr["CanManageDefineLanguage"].ToString().Contains("2");
                            obj.DefineLanguage_Edit = dr["CanManageDefineLanguage"].ToString().Contains("3");
                            obj.DefineLanguage_Delete = dr["CanManageDefineLanguage"].ToString().Contains("4");

                            obj.StaticValue_Audit = dr["CanManageStaticValue"].ToString().Contains("5");
                            obj.StaticValue_View = dr["CanManageStaticValue"].ToString().Contains("1");
                            obj.StaticValue_Add = dr["CanManageStaticValue"].ToString().Contains("2");
                            obj.StaticValue_Edit = dr["CanManageStaticValue"].ToString().Contains("3");
                            obj.StaticValue_Delete = dr["CanManageStaticValue"].ToString().Contains("4");

                            obj.Terms_Audit = dr["CanManageTerms"].ToString().Contains("5");
                            obj.Terms_View = dr["CanManageTerms"].ToString().Contains("1");
                            obj.Terms_Add = dr["CanManageTerms"].ToString().Contains("2");
                            obj.Terms_Edit = dr["CanManageTerms"].ToString().Contains("3");
                            obj.Terms_Delete = dr["CanManageTerms"].ToString().Contains("4");

                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.IsActive = dr["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            list.Add(obj);
                            #endregion
                        }

                        return list;
                    }
                }

                return null;
            }
        }

        public static RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            List<RoleInfo> list = GetList_Role(RoleID, BranchId, UserID, IsActive);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new RoleInfo();
        }

        public static bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";
            flag = false;

            #region Validations
            if (!Validations.ValidateDataType(obj.RoleID, Validations.ValueType.Integer, true, "Unique Code", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(obj.RoleName, Validations.ValueType.AlphaNumericSpecialChar, false, "Role Name", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("spUrMstRoleSave", true))
            {
                DBHelper.AddPparameter("@RoleID", !string.IsNullOrEmpty(obj.RoleID) ? Convert.ToInt64(obj.RoleID) : 0, DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@RoleName", HttpUtility.HtmlEncode(obj.RoleName), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageCategory", (obj.Category_View ? "1" : "") + (obj.Category_Add ? "2" : "") + (obj.Category_Edit ? "3" : "") + (obj.Category_Delete ? "4" : "") + (obj.Category_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageProduct", (obj.Product_View ? "1" : "") + (obj.Product_Add ? "2" : "") + (obj.Product_Edit ? "3" : "") + (obj.Product_Delete ? "4" : "") + (obj.Product_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageRole", (obj.Role_View ? "1" : "") + (obj.Role_Add ? "2" : "") + (obj.Role_Edit ? "3" : "") + (obj.Role_Delete ? "4" : "") + (obj.Role_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageFunction", (obj.Function_View ? "1" : "") + (obj.Function_Add ? "2" : "") + (obj.Function_Edit ? "3" : "") + (obj.Function_Delete ? "4" : "") + (obj.Function_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageUserRegistered", (obj.UserRegistered_View ? "1" : "") + (obj.UserRegistered_Add ? "2" : "") + (obj.UserRegistered_Edit ? "3" : "") + (obj.UserRegistered_Delete ? "4" : "") + (obj.UserRegistered_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageUserModerate", (obj.UserModerate_View ? "1" : "") + (obj.UserModerate_Add ? "2" : "") + (obj.UserModerate_Edit ? "3" : "") + (obj.UserModerate_Delete ? "4" : "") + (obj.UserModerate_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageOrganization", (obj.Organization_View ? "1" : "") + (obj.Organization_Add ? "2" : "") + (obj.Organization_Edit ? "3" : "") + (obj.Organization_Delete ? "4" : "") + (obj.Organization_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageBranch", (obj.Branch_View ? "1" : "") + (obj.Branch_Add ? "2" : "") + (obj.Branch_Edit ? "3" : "") + (obj.Branch_Delete ? "4" : "") + (obj.Branch_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageCustomer", (obj.Customer_View ? "1" : "") + (obj.Customer_Add ? "2" : "") + (obj.Customer_Edit ? "3" : "") + (obj.Customer_Delete ? "4" : "") + (obj.Customer_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageBill", (obj.Bill_View ? "1" : "") + (obj.Bill_Add ? "2" : "") + (obj.Bill_Edit ? "3" : "") + (obj.Bill_Delete ? "4" : "") + (obj.Bill_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageLanguage", (obj.Language_View ? "1" : "") + (obj.Language_Add ? "2" : "") + (obj.Language_Edit ? "3" : "") + (obj.Language_Delete ? "4" : "") + (obj.Language_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageDefineLanguage", (obj.DefineLanguage_View ? "1" : "") + (obj.DefineLanguage_Add ? "2" : "") + (obj.DefineLanguage_Edit ? "3" : "") + (obj.DefineLanguage_Delete ? "4" : "") + (obj.DefineLanguage_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageStaticvalue", (obj.StaticValue_View ? "1" : "") + (obj.StaticValue_Add ? "2" : "") + (obj.StaticValue_Edit ? "3" : "") + (obj.StaticValue_Edit ? "4" : "") + (obj.StaticValue_Edit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@CanManageTerms", (obj.Terms_View ? "1" : "") + (obj.Terms_Add ? "2" : "") + (obj.Terms_Edit ? "3" : "") + (obj.Terms_Delete ? "4" : "") + (obj.Terms_Audit ? "5" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyForModerateUsers", obj.IsOnlyForModerateUsers ? 'Y' : 'N', DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isForMembershipView", (obj.isForMembershipView ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (obj.IsActive ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@isOnlyDelete", (isOnlyDelete ? 'Y' : 'N'), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);

            }
        }

        public static RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID)
        {
            List<DBHelper.Parameter> ParamList = new List<DBHelper.Parameter>();
            ParamList.Add(new DBHelper.Parameter("@UserCode", UserID));
            ParamList.Add(new DBHelper.Parameter("@BranchId", BranchId));

            using (DBHelper dbhlper = new DBHelper())
            {
                using (DataSet ds = dbhlper.ExecuteQuery("GetEffectiveRoleForAUser", ParamList))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        RoleInfo obj = new RoleInfo();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            #region Read Object
                            if (!obj.Category_Audit) obj.Category_Audit = dr["CanManageCategory"].ToString().Contains("5");
                            if (!obj.Category_View) obj.Category_View = dr["CanManageCategory"].ToString().Contains("1");
                            if (!obj.Category_Add) obj.Category_Add = dr["CanManageCategory"].ToString().Contains("2");
                            if (!obj.Category_Edit) obj.Category_Edit = dr["CanManageCategory"].ToString().Contains("3");
                            if (!obj.Category_Delete) obj.Category_Delete = dr["CanManageCategory"].ToString().Contains("4");

                            if (!obj.Product_Audit) obj.Product_Audit = dr["CanManageProduct"].ToString().Contains("5");
                            if (!obj.Product_View) obj.Product_View = dr["CanManageProduct"].ToString().Contains("1");
                            if (!obj.Product_Add) obj.Product_Add = dr["CanManageProduct"].ToString().Contains("2");
                            if (!obj.Product_Edit) obj.Product_Edit = dr["CanManageProduct"].ToString().Contains("3");
                            if (!obj.Product_Delete) obj.Product_Delete = dr["CanManageProduct"].ToString().Contains("4");

                            if (!obj.Role_Audit) obj.Role_Audit = dr["CanManageRole"].ToString().Contains("5");
                            if (!obj.Role_View) obj.Role_View = dr["CanManageRole"].ToString().Contains("1");
                            if (!obj.Role_Add) obj.Role_Add = dr["CanManageRole"].ToString().Contains("2");
                            if (!obj.Role_Edit) obj.Role_Edit = dr["CanManageRole"].ToString().Contains("3");
                            if (!obj.Role_Delete) obj.Role_Delete = dr["CanManageRole"].ToString().Contains("4");

                            if (!obj.Function_Audit) obj.Function_Audit = dr["CanManageFunction"].ToString().Contains("5");
                            if (!obj.Function_View) obj.Function_View = dr["CanManageFunction"].ToString().Contains("1");
                            if (!obj.Function_Add) obj.Function_Add = dr["CanManageFunction"].ToString().Contains("2");
                            if (!obj.Function_Edit) obj.Function_Edit = dr["CanManageFunction"].ToString().Contains("3");
                            if (!obj.Function_Delete) obj.Function_Delete = dr["CanManageFunction"].ToString().Contains("4");

                            if (!obj.UserRegistered_Audit) obj.UserRegistered_Audit = dr["CanManageUserRegistered"].ToString().Contains("5");
                            if (!obj.UserRegistered_View) obj.UserRegistered_View = dr["CanManageUserRegistered"].ToString().Contains("1");
                            if (!obj.UserRegistered_Add) obj.UserRegistered_Add = dr["CanManageUserRegistered"].ToString().Contains("2");
                            if (!obj.UserRegistered_Edit) obj.UserRegistered_Edit = dr["CanManageUserRegistered"].ToString().Contains("3");
                            if (!obj.UserRegistered_Delete) obj.UserRegistered_Delete = dr["CanManageUserRegistered"].ToString().Contains("4");

                            if (!obj.UserModerate_Audit) obj.UserModerate_Audit = dr["CanManageUserModerate"].ToString().Contains("5");
                            if (!obj.UserModerate_View) obj.UserModerate_View = dr["CanManageUserModerate"].ToString().Contains("1");
                            if (!obj.UserModerate_Add) obj.UserModerate_Add = dr["CanManageUserModerate"].ToString().Contains("2");
                            if (!obj.UserModerate_Edit) obj.UserModerate_Edit = dr["CanManageUserModerate"].ToString().Contains("3");
                            if (!obj.UserModerate_Delete) obj.UserModerate_Delete = dr["CanManageUserModerate"].ToString().Contains("4");

                            if (!obj.Organization_Audit) obj.Organization_Audit = dr["CanManageOrganization"].ToString().Contains("5");
                            if (!obj.Organization_View) obj.Organization_View = dr["CanManageOrganization"].ToString().Contains("1");
                            if (!obj.Organization_Add) obj.Organization_Add = dr["CanManageOrganization"].ToString().Contains("2");
                            if (!obj.Organization_Edit) obj.Organization_Edit = dr["CanManageOrganization"].ToString().Contains("3");
                            if (!obj.Organization_Delete) obj.Organization_Delete = dr["CanManageOrganization"].ToString().Contains("4");

                            if (!obj.Branch_Audit) obj.Branch_Audit = dr["CanManageBranch"].ToString().Contains("5");
                            if (!obj.Branch_View) obj.Branch_View = dr["CanManageBranch"].ToString().Contains("1");
                            if (!obj.Branch_Add) obj.Branch_Add = dr["CanManageBranch"].ToString().Contains("2");
                            if (!obj.Branch_Edit) obj.Branch_Edit = dr["CanManageBranch"].ToString().Contains("3");
                            if (!obj.Branch_Delete) obj.Branch_Delete = dr["CanManageBranch"].ToString().Contains("4");

                            if (!obj.Customer_Audit) obj.Customer_Audit = dr["CanManageCustomer"].ToString().Contains("5");
                            if (!obj.Customer_View) obj.Customer_View = dr["CanManageCustomer"].ToString().Contains("1");
                            if (!obj.Customer_Add) obj.Customer_Add = dr["CanManageCustomer"].ToString().Contains("2");
                            if (!obj.Customer_Edit) obj.Customer_Edit = dr["CanManageCustomer"].ToString().Contains("3");
                            if (!obj.Customer_Delete) obj.Customer_Delete = dr["CanManageCustomer"].ToString().Contains("4");

                            if (!obj.Bill_Audit) obj.Bill_Audit = dr["CanManageBill"].ToString().Contains("5");
                            if (!obj.Bill_View) obj.Bill_View = dr["CanManageBill"].ToString().Contains("1");
                            if (!obj.Bill_Add) obj.Bill_Add = dr["CanManageBill"].ToString().Contains("2");
                            if (!obj.Bill_Edit) obj.Bill_Edit = dr["CanManageBill"].ToString().Contains("3");
                            if (!obj.Bill_Delete) obj.Bill_Delete = dr["CanManageBill"].ToString().Contains("4");

                            if (!obj.Language_Audit) obj.Language_Audit = dr["CanManageLanguage"].ToString().Contains("5");
                            if (!obj.Language_View) obj.Language_View = dr["CanManageLanguage"].ToString().Contains("1");
                            if (!obj.Language_Add) obj.Language_Add = dr["CanManageLanguage"].ToString().Contains("2");
                            if (!obj.Language_Edit) obj.Language_Edit = dr["CanManageLanguage"].ToString().Contains("3");
                            if (!obj.Language_Delete) obj.Language_Delete = dr["CanManageLanguage"].ToString().Contains("4");

                            if (!obj.DefineLanguage_Audit) obj.DefineLanguage_Audit = dr["CanManageDefineLanguage"].ToString().Contains("5");
                            if (!obj.DefineLanguage_View) obj.DefineLanguage_View = dr["CanManageDefineLanguage"].ToString().Contains("1");
                            if (!obj.DefineLanguage_Add) obj.DefineLanguage_Add = dr["CanManageDefineLanguage"].ToString().Contains("2");
                            if (!obj.DefineLanguage_Edit) obj.DefineLanguage_Edit = dr["CanManageDefineLanguage"].ToString().Contains("3");
                            if (!obj.DefineLanguage_Delete) obj.DefineLanguage_Delete = dr["CanManageDefineLanguage"].ToString().Contains("4");

                            if (!obj.StaticValue_Audit) obj.StaticValue_Audit = dr["CanManageStaticValue"].ToString().Contains("5");
                            if (!obj.StaticValue_View) obj.StaticValue_View = dr["CanManageStaticValue"].ToString().Contains("1");
                            if (!obj.StaticValue_Add) obj.StaticValue_Add = dr["CanManageStaticValue"].ToString().Contains("2");
                            if (!obj.StaticValue_Edit) obj.StaticValue_Edit = dr["CanManageStaticValue"].ToString().Contains("3");
                            if (!obj.StaticValue_Delete) obj.StaticValue_Delete = dr["CanManageStaticValue"].ToString().Contains("4");

                            if (!obj.Terms_Audit) obj.Terms_Audit = dr["CanManageTerms"].ToString().Contains("5");
                            if (!obj.Terms_View) obj.Terms_View = dr["CanManageTerms"].ToString().Contains("1");
                            if (!obj.Terms_Add) obj.Terms_Add = dr["CanManageTerms"].ToString().Contains("2");
                            if (!obj.Terms_Edit) obj.Terms_Edit = dr["CanManageTerms"].ToString().Contains("3");
                            if (!obj.Terms_Delete) obj.Terms_Delete = dr["CanManageTerms"].ToString().Contains("4");
                            #endregion
                        }

                        Common.ErrorLog.LogSQLErrors_Comments(null, "Get_Effective_Role_ForAUser:=> Role Read complete");


                        return obj;
                    }
                }

                return new RoleInfo();
            }
        }
        #endregion
    }
}