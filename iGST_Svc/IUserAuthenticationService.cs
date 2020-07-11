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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserAuthenticationService
    {
        #region Login Related
        [OperationContract]
        UserInfo User_Login(string UserName, string Password, string UserType, out string errormsg);

        [OperationContract]
        bool User_ChangePassword(string OldPssword, string NewPassword, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        bool User_ForgotPassword(string UserCodeEmailIDMobile, string OTPValidityDuration, string OTPSendOption, string UserCode, out string OTP, out string errormsg);

        [OperationContract]
        UserInfo SocialMediaLogin(string UserName, string UserType, out string errormsg);
        #endregion

        #region User Activities				
        //returns null if failed
        //[OperationContract]
        //bool ModerateUserLogin(string UserID, string Password, out UserInfo objUserInfo, out string errorMessage);

        //[OperationContract] bool UserModerate_PasswordChange(string UserIdToSave, bool isNeedToCheckPassword, string Password, string PasswordobeChanged, string UserID, out string errormsg);
        #endregion

        #region UserModerate Related
        [OperationContract]
        List<UserInfo> GetList_UserModerate(string OrganizationCode, string UserCode, string UserType);

        [OperationContract]
        UserInfo GetDetails_UserModerate(string UserCode);

        [OperationContract]
        bool Save_UserModerate(bool isOnlyDelete, UserInfo objUserInfoToBeSaved, string UserCode, string functions, out string errormsg);

        [OperationContract]
        bool Email_Verification(string EmailVerifictionKey, out string errormsg);
        #endregion

        #region Registered User Activities 
        //[OperationContract]
        //bool UserRegistration(string Company, string Email, string Mobile, string Password, out UserInfo obj, out string ErrorCode);

        //returns null if failed
        [OperationContract]
        bool UserLogin(string UserID, string Password, out UserInfo obj, out string ErrorCode);

        //[OperationContract] bool UserProfileUpdate(string OrganizationName, string EmailID, string Mobile, string Street1, string Street2, string City, string State, string Country, out UserInfo obj, out string ErrorCode);

        [OperationContract]
        bool UserRegistered_EmailChange(string UserIdToSave, bool isNeedToCheckEmailID, string EmailID, bool isNeedToCheckPassword, string Password, string EmailIDTobeChanged, string UserID, out string errormsg);

        [OperationContract]
        bool UserRegistered_MobileChange(string UserIdToSave, bool isNeedToCheckMobile, string Mobile, bool isNeedToCheckPassword, string Password, string MobileTobeChanged, string UserID, out string errormsg);
        #endregion

        #region Organization Related 
        //Moderate user can view list of the all organisatiom 
        [OperationContract]
        List<OrganizationInfo> GetList_OrganizationDropdownList(string OrganizationCode, string isActive, string UserCode);

        [OperationContract]
        List<OrganizationInfo> GetList_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, string LanguageId);

        //Registered user can view Organisation name
        //Moderate user can view details of organisation
        [OperationContract]
        OrganizationInfo GetDetails_Organization(string OrganizationCode, string OrganizationName, string City, string State, string Country, string UserID, bool IsActive, bool NeedBranchList, string LanguageId);

        //Registered user can change Organisation name
        //Moderate user can change details of organisation
        [OperationContract]
        bool Save_Organization(bool isOnlyDelete, OrganizationInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Organization Accountant Related
        [OperationContract]
        List<OrganizationAccountantInfo> GetList_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit);

        [OperationContract]
        List<OrganizationAccountantInfo> GetDetails_OrganizationAccountant(string ID, string OrganizationCode, string AccountantCode, bool IsAudit);

        [OperationContract]
        bool Save_OrganizationAccountant(bool isOnlyDelete, OrganizationAccountantInfo obj, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region DashBoardRelated
        [OperationContract]
        List<OrganizationDashBoardInfo> GetDashboard_Organization(string OrganizationCode, string FromDate, string ToDate, string Type);
        #endregion
    }
}
