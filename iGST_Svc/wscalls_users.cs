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
        #region User Activity Related 
        public static UserInfo UserLogin(string UserID, string Password)
        {
            using (DBHelper dbhlper = new DBHelper("spLoginUser"))
            {
                DBHelper.AddPparameter("@UserID", UserID);
                DBHelper.AddPparameter("@Password", Password);

                using (DataSet ds = DBHelper.Execute_Query())
                {

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        UserInfo obj = new UserInfo();
                        obj.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                        obj.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                        obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                        obj.ManagerID = ds.Tables[0].Rows[0]["ManagerID"].ToString();
                        obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                        obj.ManagerName = ds.Tables[0].Rows[0]["ManagerName"].ToString();
                        obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                        obj.AccessAllowed = ds.Tables[0].Rows[0]["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
                        obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                        obj.IsMainUser = ds.Tables[0].Rows[0]["IsMainUser"].ToString().Trim().ToUpper() == "Y";
                        obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().ToString().Trim().ToUpper() == "Y";

                        return obj;
                    }

                    return null;
                }
            }
        }

        public static bool UserProfileUpdate(string UserID, string First_Name, string Last_Name, string Mobile, string EmailID, string Street1, string Street2, string City, string State, string Country, string BranchName, out string NewDatauniqueID, out string errormsg)
        {
            errormsg = "";
            NewDatauniqueID = "";
            flag = false;

            using (DBHelper dbhlper = new DBHelper("spUserRegisteredProfileUpdate", true))
            {
                DBHelper.AddPparameter("@UserID", UserID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FirstName", First_Name, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LastName", Last_Name, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Mobile", Mobile, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@EmailID", EmailID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street1", Street1, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street2", Street2, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", City, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", State, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Country", Country, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@BranchName", BranchName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }
        #endregion

        #region UserModerate Related
        public static List<UserInfo> GetList_UserModerate(string OrganizationCode, string UserCode, string UserType)
        {
            using (DBHelper dbhlper = new DBHelper("spUrMstUserGet"))
            {
                DBHelper.AddPparameter("@UserType", UserType);
                DBHelper.AddPparameter("@UserCode", UserCode);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new UserInfo();
                            obj.UserType = dr["UserType"].ToString();
                            obj.UserTypeName = dr["UserTypeName"].ToString();
                            obj.DisplayName = dr["DisplayName"].ToString();
                            obj.UserCode = dr["UserCode"].ToString();
                            obj.First_Name = dr["FirstName"].ToString();
                            obj.Middle_Name = dr["MiddleName"].ToString();
                            obj.Last_Name = dr["LastName"].ToString();
                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim() == "Y";
                            obj.Sex = dr["Sex"].ToString();
                            obj.EmailID = dr["EmailID"].ToString();
                            obj.OrganizationCode = dr["OrganizationCode"].ToString();
                            obj.OrganizationName = dr["OrganizationName"].ToString();

                            if (!string.IsNullOrEmpty(UserCode))
                            {
                                obj.Title = dr["Title"].ToString();
                                obj.Safix = dr["Safix"].ToString();
                                obj.DOB = dr["DOB"].ToString().Length > 0 ? Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";
                                obj.UserRegisteredEmail = new UserEmailInfo();
                                if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                                {
                                    obj.UserRegisteredEmail.EmailConfirmationSentOn = Convert.ToDateTime(dr["EmailConfirmationSentOn"]).ToString().Replace("/", "-");
                                }
                                if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                                {
                                    obj.UserRegisteredEmail.EmailVerifiedOn = Convert.ToDateTime(dr["EmailVerifiedOn"]).ToString().Replace("/", "-");
                                }
                                obj.UserRegisteredEmail.IsActive = dr["IsEmailActive"].ToString().Trim() == "Y";
                                obj.UserRegisteredEmail.IsEmailConfirmationSend = dr["IsEmailConfirmationSend"].ToString() == "Y";
                                obj.UserRegisteredEmail.IsEmailVerified = dr["IsEmailVerified"].ToString() == "Y";
                            }

                            obj.MobileNumber = dr["Mobile"].ToString();

                            if (!string.IsNullOrEmpty(UserCode))
                            {

                                obj.UserRegisteredMobile = new UserMobileInfo();
                                if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                                {
                                    obj.UserRegisteredMobile.MobileConfirmationSentOn = Convert.ToDateTime(dr["MobileConfirmationSentOn"]).ToString().Replace("/", "-");
                                }
                                if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                                {
                                    obj.UserRegisteredMobile.MobileVerifiedOn = dr["MobileVerifiedOn"].ToString();
                                }
                                obj.UserRegisteredMobile.IsActive = dr["IsMobileActive"].ToString() == "Y";
                                obj.UserRegisteredMobile.IsMobileConfirmationSent = dr["IsMobileConfirmationSent"].ToString() == "Y";
                                obj.UserRegisteredMobile.IsMobileVerified = dr["IsMobileVerified"].ToString() == "Y";
                                obj.Street1 = dr["Street1"].ToString();
                                obj.Street2 = dr["Street2"].ToString();
                                obj.City = dr["City"].ToString();
                                obj.State = dr["State"].ToString();
                                obj.Country = dr["Country"].ToString();
                                obj.StateName = dr["StateName"].ToString();
                                obj.CountryName = dr["CountryName"].ToString();
                            }


                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
                            obj.IsActive = dr["IsActive"].ToString().Trim() == "Y";
                            obj.IsApproved = dr["IsApproved"].ToString().Trim() == "Y";
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                                UserBranchRoleMappingInfo newChildObj = null;

                                foreach (DataRow dr1 in ds.Tables[1].Rows)
                                {
                                    newChildObj = new UserBranchRoleMappingInfo();

                                    if (!string.IsNullOrEmpty(UserCode))
                                    {

                                        newChildObj.BranchID = dr1["BranchID"].ToString();
                                        newChildObj.BranchName = dr1["BranchName"].ToString();
                                        newChildObj.FunctionID = dr1["FunctionId"].ToString();
                                        newChildObj.FunctionName = dr1["FunctionName"].ToString();
                                        newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                                    }

                                    newChildObj.OrganizationCode = dr1["OrganizationCode"].ToString();
                                    newChildObj.OrganizationName = dr1["OrganizationName"].ToString();

                                    obj.UserBranchRoleList.Add(newChildObj);
                                }

                                obj.OrganizationCode = obj.UserBranchRoleList[0].OrganizationCode;
                                obj.OrganizationName = obj.UserBranchRoleList[0].OrganizationName;
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                    else if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = new UserInfo();

                        obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                        UserBranchRoleMappingInfo newChildObj = null;

                        foreach (DataRow dr1 in ds.Tables[1].Rows)
                        {
                            newChildObj = new UserBranchRoleMappingInfo();

                            //newChildObj.BranchID = dr1["BranchID"] != DBNull.Value ? dr1["BranchID"].ToString() : "0";
                            //newChildObj.BranchName = dr1["BranchName"] != DBNull.Value ? dr1["BranchName"].ToString() : "";
                            newChildObj.FunctionID = dr1["FunctionId"] != DBNull.Value ? dr1["FunctionId"].ToString() : "0";
                            newChildObj.FunctionName = dr1["FunctionName"] != DBNull.Value ? dr1["FunctionName"].ToString() : "";
                            newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                            //newChildObj.OrganizationCode = dr1["OrganizationCode"] != DBNull.Value ? dr1["OrganizationCode"].ToString() : "";
                            //newChildObj.OrganizationName = dr1["OrganizationName"] != DBNull.Value ? dr1["OrganizationName"].ToString() : "";

                            obj.UserBranchRoleList.Add(newChildObj);
                        }

                        list.Add(obj);

                        return list;
                    }
                }
            }

            return null;
        }

        public static List<UserInfo> GetList_UserModerate(string OrganizationCode, string UserCode, string UserType, bool isDetailsRequired)
        {
            using (DBHelper dbhlper = new DBHelper("spUrMstUserGet"))
            {
                DBHelper.AddPparameter("@UserType", UserType);
                DBHelper.AddPparameter("@UserCode", UserCode);
                DBHelper.AddPparameter("@OrganizationCode", OrganizationCode);

                using (DataSet ds = DBHelper.Execute_Query())
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = null;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj = new UserInfo();
                            obj.UserType = dr["UserType"].ToString();
                            obj.UserTypeName = dr["UserTypeName"].ToString();
                            obj.DisplayName = dr["DisplayName"].ToString();
                            obj.UserCode = dr["UserCode"].ToString();
                            obj.First_Name = dr["FirstName"].ToString();
                            obj.Middle_Name = dr["MiddleName"].ToString();
                            obj.Last_Name = dr["LastName"].ToString();
                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim() == "Y";
                            obj.Sex = dr["Sex"].ToString();
                            obj.Title = dr["Title"].ToString();
                            obj.Safix = dr["Safix"].ToString();
                            obj.DOB = dr["DOB"].ToString().Length > 0 ? Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy").Replace("-", "/") : "";

                            if (isDetailsRequired)
                            {
                                obj.Notes = obj.Safix = dr["Notes"].ToString();
                            }

                            obj.EmailID = dr["EmailID"].ToString();
                            obj.UserRegisteredEmail = new UserEmailInfo();
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredEmail.EmailConfirmationSentOn = Convert.ToDateTime(dr["EmailConfirmationSentOn"]).ToString().Replace("/", "-");
                            }
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredEmail.EmailVerifiedOn = Convert.ToDateTime(dr["EmailVerifiedOn"]).ToString().Replace("/", "-");
                            }
                            obj.UserRegisteredEmail.IsActive = dr["IsEmailActive"].ToString().Trim() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = dr["IsEmailConfirmationSend"].ToString() == "Y";
                            obj.UserRegisteredEmail.IsEmailVerified = dr["IsEmailVerified"].ToString() == "Y";

                            obj.MobileNumber = dr["Mobile"].ToString();
                            obj.UserRegisteredMobile = new UserMobileInfo();
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredMobile.MobileConfirmationSentOn = Convert.ToDateTime(dr["MobileConfirmationSentOn"]).ToString().Replace("/", "-");
                            }
                            if (dr["EmailConfirmationSentOn"] != DBNull.Value)
                            {
                                obj.UserRegisteredMobile.MobileVerifiedOn = dr["MobileVerifiedOn"].ToString();
                            }
                            obj.UserRegisteredMobile.IsActive = dr["IsMobileActive"].ToString() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = dr["IsMobileConfirmationSent"].ToString() == "Y";
                            obj.UserRegisteredMobile.IsMobileVerified = dr["IsMobileVerified"].ToString() == "Y";

                            obj.Street1 = dr["Street1"].ToString();
                            obj.Street2 = dr["Street2"].ToString();
                            obj.City = dr["City"].ToString();
                            obj.State = dr["State"].ToString();
                            obj.Country = dr["Country"].ToString();
                            obj.StateName = dr["StateName"].ToString();
                            obj.CountryName = dr["CountryName"].ToString();

                            obj.AccessAllowed = dr["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
                            obj.IsActive = dr["IsActive"].ToString().Trim() == "Y";
                            obj.IsApproved = dr["IsApproved"].ToString().Trim() == "Y";
                            obj.LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
                            obj.LastModifiedBy = dr["LastModifiedBy"].ToString();
                            obj.ActivityType = dr["ActivityType"].ToString();
                            obj.DatauniqueID = dr["DatauniqueID"].ToString();

                            if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                                UserBranchRoleMappingInfo newChildObj = null;

                                foreach (DataRow dr1 in ds.Tables[1].Rows)
                                {
                                    newChildObj = new UserBranchRoleMappingInfo();

                                    newChildObj.BranchID = dr1["BranchID"].ToString();
                                    newChildObj.BranchName = dr1["BranchName"].ToString();
                                    newChildObj.FunctionID = dr1["FunctionId"].ToString();
                                    newChildObj.FunctionName = dr1["FunctionName"].ToString();
                                    newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                                    newChildObj.OrganizationCode = dr1["OrganizationCode"].ToString();
                                    newChildObj.OrganizationName = dr1["OrganizationName"].ToString();

                                    obj.UserBranchRoleList.Add(newChildObj);
                                }

                                obj.OrganizationCode = obj.UserBranchRoleList[0].OrganizationCode;
                                obj.OrganizationName = obj.UserBranchRoleList[0].OrganizationName;
                            }

                            list.Add(obj);
                        }

                        return list;
                    }
                    else if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        List<UserInfo> list = new List<UserInfo>();
                        UserInfo obj = new UserInfo();

                        obj.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                        UserBranchRoleMappingInfo newChildObj = null;

                        foreach (DataRow dr1 in ds.Tables[1].Rows)
                        {
                            newChildObj = new UserBranchRoleMappingInfo();

                            //newChildObj.BranchID = dr1["BranchID"] != DBNull.Value ? dr1["BranchID"].ToString() : "0";
                            //newChildObj.BranchName = dr1["BranchName"] != DBNull.Value ? dr1["BranchName"].ToString() : "";
                            newChildObj.FunctionID = dr1["FunctionId"] != DBNull.Value ? dr1["FunctionId"].ToString() : "0";
                            newChildObj.FunctionName = dr1["FunctionName"] != DBNull.Value ? dr1["FunctionName"].ToString() : "";
                            newChildObj.isSelected = dr1["isSelected"].ToString().Trim() == "Y";
                            //newChildObj.OrganizationCode = dr1["OrganizationCode"] != DBNull.Value ? dr1["OrganizationCode"].ToString() : "";
                            //newChildObj.OrganizationName = dr1["OrganizationName"] != DBNull.Value ? dr1["OrganizationName"].ToString() : "";

                            obj.UserBranchRoleList.Add(newChildObj);
                        }

                        list.Add(obj);

                        return list;
                    }
                }
            }

            return null;
        }

        public static UserInfo GetDetails_UserModerate(string UserCode)
        {
            List<UserInfo> list = GetList_UserModerate("", string.IsNullOrEmpty(UserCode) ? "<NEW>" : UserCode.Trim(), "");

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }

            return new UserInfo();
        }

        /// <summary>
        /// Registered User Registration
        /// </summary>
        /// <param name="isOnlyDelete"></param>
        /// <param name="objUserInfoToBeSaved"></param>
        /// <param name="objUserInfo"></param>
        /// <param name="functions"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public static bool Save_UserModerate(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, string UserCode, string functions, out string errormsg)
        {
            errormsg = "";
            flag = false;
            string accountkey = Guid.NewGuid().ToString() + System.DateTime.Now.ToString();

            #region Validations
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserID, Validations.ValueType.Integer, true, "User ID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.UserType, Validations.ValueType.PairValueAEMR, false, "User Type", out errormsg)) { return false; }

            if (objUserInfoToBeSaved.UserType.Trim().ToUpper() == "R"
                || objUserInfoToBeSaved.UserType.Trim().ToUpper() == "E"
                || objUserInfoToBeSaved.UserType.Trim().ToUpper() == "A")
            {
                if (!Validations.ValidateDataType(objUserInfoToBeSaved.OrganizationName, Validations.ValueType.AlphaNumericSpecialChar, (objUserInfoToBeSaved.UserType.Trim().ToUpper() == "A"), "Organization name", out errormsg)) { return false; }
            }

            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Title, Validations.ValueType.Integer, true, "Title", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Safix, Validations.ValueType.Integer, true, "Safix", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.First_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "First name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Middle_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Last name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Last_Name, Validations.ValueType.AlphaNumericSpecialChar, true, "Middle name", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Sex, Validations.ValueType.Alphabet, true, "Sex", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.DOB, Validations.ValueType.DateTime, true, "DOB", out errormsg)) { return false; }

            if (!Validations.ValidateDataType(objUserInfoToBeSaved.EmailID, Validations.ValueType.Email, false, "EmailID", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.MobileNumber, Validations.ValueType.MobileNumber, false, "Mobile number", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street1, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 1", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Street2, Validations.ValueType.AlphaNumericSpecialChar, true, "Street 2", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.City, Validations.ValueType.AlphaNumericSpecialChar, true, "City", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.State, Validations.ValueType.Integer, true, "State", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Country, Validations.ValueType.Integer, true, "Country", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Password, Validations.ValueType.Password, true, "Password", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Functions, Validations.ValueType.AlphaNumericSpecialChar, true, "Functions", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(objUserInfoToBeSaved.Notes, Validations.ValueType.AlphaNumericSpecialChar, true, "Notes", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[SPURMSTUserRegistration]", true))
            {
                #region MyRegion
                DBHelper.AddPparameter("@IsOnlyDelete", (isOnlyDelete ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", objUserInfoToBeSaved.UserCode.ToString().Trim(), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserType", HttpUtility.HtmlEncode(objUserInfoToBeSaved.UserType), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Title", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Title), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Safix", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Safix), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@FirstName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.First_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@MiddleName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Middle_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@LastName", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Last_Name), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DOB", Validations.ConvertToDateReturn_ddMMyyyy_blank(objUserInfoToBeSaved.DOB), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Sex", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Sex), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@AccessAllowed", (objUserInfoToBeSaved.AccessAllowed ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsApproved", (objUserInfoToBeSaved.IsApproved ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsActive", (objUserInfoToBeSaved.IsActive ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@DONEBY", UserCode != null && UserCode.Length > 0 ? UserCode : "", DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@Notes", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Notes), DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@Mobile", objUserInfoToBeSaved.MobileNumber, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsMobileActive", HttpUtility.HtmlEncode(objUserInfoToBeSaved.IsMobileActive ? "Y" : ""), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@EmailID", objUserInfoToBeSaved.EmailID, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@EmailVerificationKey", accountkey, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@IsEmailActive", objUserInfoToBeSaved.IsEmailActive ? "Y" : "", DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@Password", objUserInfoToBeSaved.Password, DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@Street1", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street1), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Street2", HttpUtility.HtmlEncode(objUserInfoToBeSaved.Street2), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@City", HttpUtility.HtmlEncode(objUserInfoToBeSaved.City), DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@State", (objUserInfoToBeSaved.State == "" ? "0" : objUserInfoToBeSaved.State), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@Country", (objUserInfoToBeSaved.Country == "" ? "0" : objUserInfoToBeSaved.Country), DBHelper.param_types.BigInt);
                DBHelper.AddPparameter("@PIN", objUserInfoToBeSaved.PIN, DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@OrganizationCode1", objUserInfoToBeSaved.OrganizationCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OrganizationName", objUserInfoToBeSaved.OrganizationName, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@Functions", functions, DBHelper.param_types.Varchar);

                DBHelper.AddPparameter("@NewUserCode", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@BranchId", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@OrganizationCode", "", DBHelper.param_types.Varchar, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@NewDatauniqueID", 0, DBHelper.param_types.BigInt, 50, DBHelper.param_direction.Output);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);
                #endregion

                //if (dbhlper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg))
                if (DBHelper.Execute_NonQuery(out errormsg))
                {
                    #region Email Send on first time entry
                    if (UserCode == "")
                    {
                        NameValueCollection appsettings = System.Configuration.ConfigurationManager.AppSettings;
                        System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();

                        sbHtml.AppendLine("<table>");
                        sbHtml.AppendLine("  <tr><td>Hi " + objUserInfoToBeSaved.First_Name + "</td></tr>");
                        sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                        sbHtml.AppendLine("  <tr><td>Thank yo registering in out website. Everything is already done, just one step is pending to activate your account. Follow the below step.</td></tr>");
                        sbHtml.AppendLine("  <tr><td>Please click on the <a href='" + System.Configuration.ConfigurationManager.AppSettings["WebURL"].ToString() + "activateregaccount?accountkey=" + accountkey + "'>link</a> to activate your account</td></tr>");
                        sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                        sbHtml.AppendLine("  <tr><td>&nbsp;</td></tr>");
                        sbHtml.AppendLine("  <tr><td>Regards</td></tr>");
                        sbHtml.AppendLine("  <tr><td><a href='" + appsettings["WebURL"].ToString() + "'>Big page</a></td></tr>");
                        sbHtml.AppendLine("</table>");

                        new MailHelper("EmailConfirmationOnRegistration", "Email verification for your BigPage account", sbHtml.ToString(), appsettings["WebSenderEmailID"].ToString(), appsettings["WebSenderEmailDisplayName"].ToString(), objUserInfoToBeSaved.EmailID, objUserInfoToBeSaved.First_Name + " " + objUserInfoToBeSaved.Last_Name, "", "");
                    }
                    #endregion

                    //string NewUserCode = dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 5).ToString();
                    //int OrganizationId = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count-4));
                    //int BranchId = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 3));
                    //int DatauniqueID = Convert.ToInt32(dbhlper.GetCurrentParameterValue(dbhlper.GetCurrentParameters().Count - 2));

                    //dbhlper.SetSQLCommandText("[spUrMstUserRegisteredFunctionMappingSave]");
                    //foreach (UserBranchRoleMappingInfo obj in MappingList)
                    //{
                    //    dbhlper.ClearParameters();
                    //    DBHelper.AddPparameter("@UserId", NewUserCode, DBHelper.param_types.Varchar);
                    //    DBHelper.AddPparameter("@DatauniqueID", DatauniqueID, DBHelper.param_types.BigInt);
                    //    DBHelper.AddPparameter("@FunctionId", obj.FunctionID, DBHelper.param_types.BigInt);
                    //    DBHelper.AddPparameter("@BranchId", BranchId, DBHelper.param_types.BigInt);
                    //    DBHelper.AddPparameter("@OrganizationId", OrganizationId, DBHelper.param_types.BigInt);
                    //    DBHelper.AddPparameter("@DoneBy", objUserInfo.UserID, DBHelper.param_types.BigInt);
                    //    DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                    //    if (!dbhlper.Execute_NonQuery_DoNotCloseConnectionOnSuccess(out errormsg))
                    //    {
                    //        return false;
                    //    }
                    //}

                    //dbhlper.CloseConnection(true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Email_Verification(string EmailVerifictionKey, out string errormsg)
        {
            errormsg = "";
            flag = false;

            using (DBHelper dbhlper = new DBHelper("[SPURMSTEmailVerification]", true))
            {
                #region MyRegion
                DBHelper.AddPparameter("@EmailVerification", HttpUtility.HtmlEncode(EmailVerifictionKey), DBHelper.param_types.Varchar);
                #endregion

                if (DBHelper.Execute_NonQuery(out errormsg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public static UserInfo ModerateUserLogin(string UserID, string Password)
        //{
        //    using (DBHelper dbhlper = new DBHelper("spLoginModerate"))
        //    {
        //        DBHelper.AddPparameter("@UserID", UserID);
        //        DBHelper.AddPparameter("@Password", Password);

        //        using (DataSet ds = DBHelper.Execute_Query())
        //        {

        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                UserInfo obj = new UserInfo();
        //                obj.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
        //                obj.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
        //                obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
        //                obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
        //                obj.AccessAllowed = ds.Tables[0].Rows[0]["AccessAllowed"].ToString().Trim().ToUpper() == "Y";
        //                obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
        //                obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().ToString().Trim().ToUpper() == "Y";

        //                return obj;
        //            }

        //            return null;
        //        }
        //    }
        //}
        #endregion
    }
}