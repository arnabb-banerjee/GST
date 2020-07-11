using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessObjects;

namespace iGST_Svc
{
    [KnownType(typeof(UserInfo))]
    public partial class UserAuthenticationService : IUserAuthenticationService
    {
        #region Login Related
        public UserInfo User_Login(string UserName, string Password, string UserType, out string errormsg)
        {
            return wscalls.User_Login(UserName, Password, UserType, out errormsg);
        }

        public bool User_ForgotPassword(string UserCodeEmailIDMobile, string OTPValidityDuration, string OTPSendOption, string UserCode, out string OTP, out string errormsg)
        {
            return wscalls.User_ForgotPassword(UserCodeEmailIDMobile, OTPValidityDuration, OTPSendOption, UserCode, out OTP, out errormsg);
        }

        public UserInfo SocialMediaLogin(string UserName, string UserType, out string errormsg)
        {
            return wscalls.SocialMediaLogin(UserName, UserType, out errormsg);
        }
        #endregion

        #region UserModerate Related
        public List<UserInfo> GetList_UserModerate(string OrganizationCode, string UserCode, string UserType)
        {
            return wscalls.GetList_UserModerate(OrganizationCode, UserCode, UserType);
        }

        public UserInfo GetDetails_UserModerate(string UserCode)
        {
            return wscalls.GetDetails_UserModerate(UserCode);
        }

        public bool Save_UserModerate(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, string UserCode, string functions, out string errormsg)
        {
            return wscalls.Save_UserModerate(isOnlyDelete, objUserInfoToBeSaved, UserCode, functions, out errormsg);
        }

        public bool Email_Verification(string EmailVerifictionKey, out string errormsg)
        {
            return wscalls.Email_Verification(EmailVerifictionKey, out errormsg);
        }
        #endregion

        #region User Activities				
        //returns null if failed
        //public bool ModerateUserLogin(string UserID, string Password, out UserInfo objUserInfo, out string errorMessage)
        //{
        //    errorMessage = "";
        //    objUserInfo = wscalls.ModerateUserLogin(UserID, Password);

        //    if (errorMessage != null)
        //    {
        //        if (!objUserInfo.AccessAllowed)
        //            errorMessage = "You are not allowed to access";
        //        else if (!objUserInfo.IsApproved)
        //            errorMessage = "Your account is not approved";
        //        else if (!objUserInfo.IsActive)
        //            errorMessage = "Your account is not active";
        //        else
        //            return true;
        //    }
        //    else
        //        errorMessage = "User name or password is wrong.";

        //    return false;
        //}
        #endregion

        #region Registered User Activities 
        //public bool UserRegistration(string Company, string Email, string Mobile, string Password, out UserInfo obj, out string ErrorCode)
        //{
        //    ErrorCode = "";
        //    obj = null;

        //    if (wscalls.UserRegistration(Company, Email, Mobile, Password, out ErrorCode))
        //    {
        //        obj = wscalls.UserLogin(Email, Password);

        //        if (obj != null)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //returns null if failed
        public bool UserLogin(string UserID, string Password, out UserInfo obj, out string errorMessage)
        {
            errorMessage = "";
            obj = wscalls.UserLogin(UserID, Password);

            if (obj != null)
            {
                if (!obj.AccessAllowed)
                    errorMessage = "You are not allowed to access the system. Please contact with the authority";
                else if (!obj.IsApproved)
                    errorMessage = "Your account is not approved. Please contact with the authority";
                else if (!obj.IsActive)
                    errorMessage = "Your account is not active. Please contact with the authority";
                else
                    return true;
            }
            else
                errorMessage = "User details not found";

            return false;
        }

        public bool UserProfileUpdate(string UserID, string First_Name, string Last_Name, string Mobile, string EmailID, string Street1, string Street2, string City, string State, string Country, string BranchName, out string NewDatauniqueID, out string errormsg)
        {
            errormsg = "";
            return wscalls.UserProfileUpdate(UserID, First_Name, Last_Name, Mobile, EmailID, Street1, Street2, City, State, Country, BranchName, out NewDatauniqueID, out errormsg);

        }

        public bool UserRegistered_EmailChange(string UserIdToSave, bool isNeedToCheckEmailID, string EmailID, bool isNeedToCheckPassword, string Password, string EmailIDTobeChanged, string UserID, out string errormsg)
        {
            errormsg = "";
            return false;
        }

        public bool UserRegistered_MobileChange(string UserIdToSave, bool isNeedToCheckMobile, string Mobile, bool isNeedToCheckPassword, string Password, string MobileTobeChanged, string UserID, out string errormsg)
        {
            errormsg = "";
            return false;
        }

        public bool User_ChangePassword(string OldPssword, string NewPassword, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.User_ChangePassword(OldPssword, NewPassword, objUserInfo, out errormsg);
        }
        #endregion

        #region Organization Related 
        //Moderate user can view list of the all organisatiom 
        public List<OrganizationInfo> GetList_OrganizationDropdownList(string OrganizationCode, string isActive, string UserCode)
        {
            return wscalls.GetList_OrganizationDropdownList(OrganizationCode, isActive, UserCode);
        }

        public List<OrganizationInfo> GetList_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId)
        {
            return wscalls.GetList_Organization(OrganizationCode, OrganizationName, City, State, Country, UserID, IsActive, LanguageId);
        }

        //Registered user can view Organisation name
        //Moderate user can view details of organisation
        public OrganizationInfo GetDetails_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, bool NeedBranchList, string LanguageId)
        {
            return wscalls.GetDetails_Organization(OrganizationCode, OrganizationName, City, State, Country, UserID, IsActive, LanguageId);
        }

        //Registered user can change Organisation name
        //Moderate user can change details of organisation
        public bool Save_Organization(bool isOnlyDelete, OrganizationInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Organization(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region Organization Accountant Related
        public List<OrganizationAccountantInfo> GetList_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            return wscalls.GetList_OrganizationAccountant(ID, OrganizationCode, AccountantCode, IsAudit);
        }

        public List<OrganizationAccountantInfo> GetDetails_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit)
        {
            return wscalls.GetDetails_OrganizationAccountant(ID, OrganizationCode, AccountantCode, IsAudit);
        }

        public bool Save_OrganizationAccountant(bool isOnlyDelete, OrganizationAccountantInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_OrganizationAccountant(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
        #endregion

        #region DashBoardRelated
        public List<OrganizationDashBoardInfo> GetDashboard_Organization(string OrganizationCode, string FromDate, string ToDate, string Type)
        {
            return wscalls.GetDashboard_Organization(OrganizationCode, FromDate, ToDate, Type);
        }
        #endregion
    }
}
