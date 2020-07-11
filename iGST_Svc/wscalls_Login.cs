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
        #region Login Related
        public static UserInfo User_Login(string UserName, string Password, string UserType, out string errormsg)
        {
            errormsg = "";

            List<DBHelper.Parameter> ParamList = new List<DBHelper.Parameter>();
            ParamList.Add(new DBHelper.Parameter("@UserType", "R"));
            ParamList.Add(new DBHelper.Parameter("@UserCode", UserName));
            ParamList.Add(new DBHelper.Parameter("@Password", Password));

            using (DBHelper dbhlper = new DBHelper())
            {
                try
                {
                    using (DataSet ds = dbhlper.ExecuteQuery("spLogin", ParamList))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            UserInfo obj = new UserInfo();

                            obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                            obj.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                            obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                            obj.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();
                            obj.Country = ds.Tables[0].Rows[0]["Country"].ToString();

                            obj.UserRegisteredEmail = new UserEmailInfo();
                            obj.UserRegisteredEmail.IsEmailVerified = ds.Tables[0].Rows[0]["IsEmailVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = ds.Tables[0].Rows[0]["IsEmailConfirmationSend"].ToString().Trim().ToUpper() == "Y";

                            obj.UserRegisteredMobile = new UserMobileInfo();
                            obj.UserRegisteredMobile.IsMobileVerified = ds.Tables[0].Rows[0]["IsMobileVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = ds.Tables[0].Rows[0]["IsMobileConfirmationSent"].ToString().Trim().ToUpper() == "Y";

                            obj.MobileNumber = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                            obj.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                            obj.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                            obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                            obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                            obj.EffectiveRole = Get_Effective_Role_ForAUser("", obj.UserCode);
                            if (obj.EffectiveRole != null)
                            {
                                obj.ApplicableCurrencies = wsCurrency.GetDetails_BUCurrencies("", "", obj.OrganizationCode);
                            }

                            Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall-Success");

                            return obj;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                    errormsg = ex.Message;
                }
                return null;
            }
        }

        public static bool User_ChangePassword(string OldPssword, string NewPassword, UserInfo objUserInfo, out string errormsg)
        {
            errormsg = "";

            #region Validations
            if (!Validations.ValidateDataType(OldPssword, Validations.ValueType.AlphaNumericSpecialChar, false, "Old Password", out errormsg)) { return false; }
            if (!Validations.ValidateDataType(NewPassword, Validations.ValueType.AlphaNumericSpecialChar, false, "New Password", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spChangePassword]", true))
            {
                DBHelper.AddPparameter("@UserCode", objUserInfo.UserCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@NewPassword", NewPassword, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OldPassword", OldPssword, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static bool User_ForgotPassword(string UserCodeEmailIDMobile, string OTPValidityDuration, string OTPSendOption, string UserCode, out string OTP, out string errormsg)
        {
            errormsg = "";
            OTP = "";

            #region Validations
            if (!Validations.ValidateDataType(UserCodeEmailIDMobile, Validations.ValueType.AlphaNumericSpecialChar, false, "User ID", out errormsg)) { return false; }
            #endregion

            using (DBHelper dbhlper = new DBHelper("[spForgotPassword]", true))
            {
                DBHelper.AddPparameter("@UserCodeEmailIDMobile", UserCodeEmailIDMobile, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OTPValidityDuration", OTPValidityDuration, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OTPSendOption", OTPSendOption, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@UserCode", UserCode, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@OTP", OTP, DBHelper.param_types.Varchar);
                DBHelper.AddPparameter("@ErrorMessage", errormsg, DBHelper.param_types.Varchar, 500, DBHelper.param_direction.Output);

                return DBHelper.Execute_NonQuery(out errormsg);
            }
        }

        public static UserInfo SocialMediaLogin(string UserName, string UserType, out string errormsg)
        {
            errormsg = "";

            using (DBHelper dbhlper = new DBHelper("spLogin"))
            {
                try
                {
                    DBHelper.AddPparameter("@UserType", "");
                    DBHelper.AddPparameter("@UserCode", UserName);

                    using (DataSet ds = DBHelper.Execute_Query())
                    {

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            UserInfo obj = new UserInfo();

                            obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                            obj.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                            obj.First_Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            obj.Last_Name = ds.Tables[0].Rows[0]["LastName"].ToString();
                            obj.EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString();

                            obj.UserRegisteredEmail = new UserEmailInfo();
                            obj.UserRegisteredEmail.IsEmailVerified = ds.Tables[0].Rows[0]["IsEmailVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredEmail.IsEmailConfirmationSend = ds.Tables[0].Rows[0]["IsEmailConfirmationSend"].ToString().Trim().ToUpper() == "Y";

                            obj.UserRegisteredMobile = new UserMobileInfo();
                            obj.UserRegisteredMobile.IsMobileVerified = ds.Tables[0].Rows[0]["IsMobileVerified"].ToString().Trim().ToUpper() == "Y";
                            obj.UserRegisteredMobile.IsMobileConfirmationSent = ds.Tables[0].Rows[0]["IsMobileConfirmationSent"].ToString().Trim().ToUpper() == "Y";

                            obj.MobileNumber = ds.Tables[0].Rows[0]["Mobile"].ToString();
                            obj.IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString().Trim().ToUpper() == "Y";
                            obj.IsApproved = ds.Tables[0].Rows[0]["IsApproved"].ToString().Trim().ToUpper() == "Y";
                            obj.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                            obj.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                            obj.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                            obj.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                            obj.EffectiveRole = Get_Effective_Role_ForAUser("", obj.UserCode);

                            Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall-Success");

                            return obj;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                }
                return null;
            }
        }

        #endregion
    }
}