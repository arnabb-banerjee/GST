
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
using Common;
using BusinessObjects;
using System.Data;
using System.Web.Script.Serialization;

namespace iGST.Controllers
{
    public partial class UsersController : Controller
    {
        string ErrorMessage = "";

        #region User Moderate Related
        [Route("accountants")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Accountant()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return View("~/Views/MasterPages/UserAccountantList.cshtml", iGstSvc.GetList_UserModerate(OrganizationCode, "", "A"));
            }
        }

        [Route("accountant")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Accountant(string UserCode)
        {
            string OrganizationCode = "";

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo obj = iGstSvc.GetDetails_UserModerate(UserCode);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                ViewBag.Countries_Basic = CommonMethods.ListCountry(obj.Country);

                ViewBag.State_Basic = CommonMethods.ListStates(obj.Country, obj.State);

                ListStaticValue_RegtypeDeliveryPaymentTitleSafix("", "", "", obj.Title, obj.Safix);

                return PartialView("~/Views/MasterPages/UserAccountantDetails.cshtml", obj);
            }
        }

        [Route("businessusers")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_UserRegistered()
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return View("~/Views/MasterPages/UserRegisteredList.cshtml", iGstSvc.GetList_UserModerate("", "", "R"));
            }
        }

        [Route("businessuser")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_UserRegistered(string UserCode)
        {
            string OrganizationCode = "";

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo obj = iGstSvc.GetDetails_UserModerate(UserCode);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                ViewBag.Countries_Basic = CommonMethods.ListCountry(obj.Country);

                ViewBag.State_Basic = CommonMethods.ListStates(obj.Country, obj.State);

                ListStaticValue_RegtypeDeliveryPaymentTitleSafix("", "", "", obj.Title, obj.Safix);

                return PartialView("~/Views/MasterPages/UserRegisteredDetails.cshtml", iGstSvc.GetDetails_UserModerate(UserCode));
            }
        }

        [Route("employees")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_UserEmpolyee()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return View("~/Views/MasterPages/UserEmployeeList.cshtml", iGstSvc.GetList_UserModerate(OrganizationCode, "", "E"));
            }
        }

        [Route("employee")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_UserEmployee(string UserCode)
        {
            string OrganizationCode = "";

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo obj = iGstSvc.GetDetails_UserModerate(UserCode);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                ViewBag.Countries_Basic = CommonMethods.ListCountry(obj.Country);

                ViewBag.State_Basic = CommonMethods.ListStates(obj.Country, obj.State);

                ListStaticValue_RegtypeDeliveryPaymentTitleSafix("", "", "", obj.Title, obj.Safix);

                return PartialView("~/Views/MasterPages/UserEmployeeDetails.cshtml", obj);
            }
        }

        [Route("users")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_UserModerate()
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return View("~/Views/MasterPages/UserModerateList.cshtml", iGstSvc.GetList_UserModerate("", "", "M"));
            }
        }

        [Route("moderate")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_UserModerate(string UserCode)
        {
            string OrganizationCode = "";

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo obj = iGstSvc.GetDetails_UserModerate(UserCode);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                ViewBag.Countries_Basic = CommonMethods.ListCountry(obj.Country);

                ViewBag.State_Basic = CommonMethods.ListStates(obj.Country, obj.State);

                ListStaticValue_RegtypeDeliveryPaymentTitleSafix("", "", "", obj.Title, obj.Safix);

                return PartialView("~/Views/MasterPages/UserModerateDetails.cshtml", iGstSvc.GetDetails_UserModerate(UserCode));
            }
        }

        [Route("moderatesave")]
        [ValidateUserSession(ActionName = "UserRegistered")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_UserModerate(string isOnlyDelete, string UserType, string OrganizaionName, string OrganizaionCode, string UserCode, string dob, string Sex, string Title, string Safix, string First_name, string Middle_name, string Last_name, string EmailID, string Mobile, string Address, string Street2, string City, string State, string Country, string PIN, string IsActive, string isApproved, string Functions, string Password, string Notes)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizaionName = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo objUserInfoToBeSaved = new UserInfo();

                objUserInfoToBeSaved.UserCode = UserCode;
                objUserInfoToBeSaved.UserType = UserType;
                //objUserInfoToBeSaved.IsMainUser = (IsMainUser != null && IsMainUser.Trim().ToUpper() == "Y");
                objUserInfoToBeSaved.IsActive = (IsActive != null && IsActive.Trim().ToUpper() == "Y");
                objUserInfoToBeSaved.IsApproved = (isApproved != null && isApproved.Trim().ToUpper() == "Y");

                objUserInfoToBeSaved.OrganizationName = OrganizaionName;
                objUserInfoToBeSaved.OrganizationCode = OrganizaionCode;
                objUserInfoToBeSaved.Title = Title;
                objUserInfoToBeSaved.DOB = dob;
                objUserInfoToBeSaved.Sex = Sex;
                objUserInfoToBeSaved.Safix = Safix;
                objUserInfoToBeSaved.First_Name = First_name;
                objUserInfoToBeSaved.Last_Name = Last_name;
                objUserInfoToBeSaved.Middle_Name = Middle_name;
                objUserInfoToBeSaved.Street1 = Address;
                objUserInfoToBeSaved.Street2 = Street2;
                objUserInfoToBeSaved.City = City;
                objUserInfoToBeSaved.State = State;
                objUserInfoToBeSaved.Country = Country;
                objUserInfoToBeSaved.EmailID = EmailID;
                objUserInfoToBeSaved.MobileNumber = Mobile;
                objUserInfoToBeSaved.Functions = Functions;
                objUserInfoToBeSaved.Password = Password;

                objUserInfoToBeSaved.Notes = Notes;

                //objUserInfoToBeSaved.UserBranchRoleList = new List<UserBranchRoleMappingInfo>();
                //UserBranchRoleMappingInfo objFunction = null;

                //foreach (string str in Functions.Split('#'))
                //{
                //    objFunction = new UserBranchRoleMappingInfo();

                //    string[] arry = str.Split(',');

                //    if (arry.Length == 2)
                //    {
                //        objFunction.FunctionID = arry[0].Trim();
                //        objFunction.BranchID = arry[1].Trim();
                //    }
                //    else
                //    {
                //        objUserInfoToBeSaved = null;
                //        objFunction = null;
                //        return Json("Iternal Problem: in permission level", JsonRequestBehavior.AllowGet);
                //    }

                //    objUserInfoToBeSaved.UserBranchRoleList.Add(objFunction);
                //}

                if (iGstSvc.Save_UserModerate(isOnlyDelete.Trim().ToUpper() == "Y", objUserInfoToBeSaved, ((UserInfo)Session["UserDetails"]).UserCode, Functions, out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }

                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region User Registration Related
        /*
        [ValidateUserSession]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_UserList()
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return View("~/Views/MasterPages/BankList", iGstSvc.GetList_Bank("", "", true));
            }
        }

        [ValidateUserSession]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_User(string BankID)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return PartialView("~/Views/MasterPages/BankDetails.cshtml", iGstSvc.GetDetails_Bank(BankID, "", true));
            }
        }
        */

        [Route("registration")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult User_Registration(string UserType, string FirstName, string MidName, string LastName, string OrganizaionName, string UserID, string Street1, string EmailID, string Mobile, string City, string State, string Country, string Password)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                UserInfo objUserInfoToBeSaved = new UserInfo();

                objUserInfoToBeSaved.UserCode = "";
                objUserInfoToBeSaved.UserType = UserType.Trim().ToUpper();
                //objUserInfoToBeSaved.IsMainUser = (IsMainUser != null && IsMainUser.Trim().ToUpper() == "Y");
                objUserInfoToBeSaved.IsActive = true;
                objUserInfoToBeSaved.IsApproved = false;

                objUserInfoToBeSaved.OrganizationCode = "";
                objUserInfoToBeSaved.OrganizationName = OrganizaionName;
                objUserInfoToBeSaved.Title = "";
                objUserInfoToBeSaved.First_Name = FirstName;
                objUserInfoToBeSaved.Last_Name = LastName;
                objUserInfoToBeSaved.Middle_Name = MidName;
                objUserInfoToBeSaved.Street1 = Street1;
                objUserInfoToBeSaved.City = City;
                objUserInfoToBeSaved.State = State;
                objUserInfoToBeSaved.Country = Country;
                objUserInfoToBeSaved.EmailID = EmailID;
                objUserInfoToBeSaved.MobileNumber = Mobile;
                objUserInfoToBeSaved.Functions = "";
                objUserInfoToBeSaved.Password = Password;

                if (iGstSvc.Save_UserModerate(false, objUserInfoToBeSaved, "", "", out ErrorMessage))
                {
                    //return Json("Ok", JsonRequestBehavior.AllowGet);
                    //return new NormalLoginController().User_Login(EmailID, Password, UserType.Trim().ToUpper(), "");

                    //return RedirectToAction("processlogin", "NormalLogin", new { UserCode = EmailID, Password = Password, UserType = UserType.Trim().ToUpper(), returnUrl = "" });

                    UserInfo objUserInfo = iGstSvc.User_Login(EmailID, Password, UserType, out ErrorMessage);
                    //return Json("Error during login=>iuyiuyiuyiyi"+ objUserInfo.UserCode, JsonRequestBehavior.AllowGet);

                    if (objUserInfo != null && objUserInfo.UserCode != null)
                    {
                        SignInStatus UserAccountSatus = SignInStatus.Success;

                        Session["UserDetails"] = objUserInfo;
                        string UserName = System.Web.HttpContext.Current.User.Identity.Name;

                        switch (UserAccountSatus)
                        {
                            case SignInStatus.Success:
                                return Json("Ok", JsonRequestBehavior.AllowGet);
                            case SignInStatus.LockedOut:
                                return View("~/Views/MasterPages/Lockout.cshtml");
                            case SignInStatus.RequiresVerification:
                                return RedirectToAction("SendCode", new { ReturnUrl = "" });
                            case SignInStatus.Failure:
                            default:
                                return Json("Invalid login attempt.", JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }

        //[ValidateUserSession]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Save_User(string isOnlyDelete, string UserType, string OrganizaionName, string UserID, string Title, string First_name, string Middle_name, string Last_name, string Address, string EmailID, string Mobile, string City, string State, string Country, string Password, string IsMainUser, string IsActive, string IsApproed)
        //{
        //    using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
        //    {
        //        UserInfo objUserInfoToBeSaved = new UserInfo();
        //        objUserInfoToBeSaved.UserID = UserID;
        //        objUserInfoToBeSaved.UserType = "M";
        //        objUserInfoToBeSaved.IsMainUser = (IsMainUser != null && IsMainUser.Trim().ToUpper() == "Y");
        //        objUserInfoToBeSaved.IsActive = (IsActive != null && IsActive.Trim().ToUpper() == "Y");
        //        objUserInfoToBeSaved.IsApproved = (IsApproed != null && IsApproed.Trim().ToUpper() == "Y");
        //        objUserInfoToBeSaved.OrganizationName = OrganizaionName;
        //        objUserInfoToBeSaved.Title = Title;
        //        objUserInfoToBeSaved.First_Name = First_name;
        //        objUserInfoToBeSaved.Last_Name = Last_name;
        //        objUserInfoToBeSaved.Middle_Name = Middle_name;
        //        objUserInfoToBeSaved.Street1 = Address;
        //        objUserInfoToBeSaved.City = City;
        //        objUserInfoToBeSaved.State = State;
        //        objUserInfoToBeSaved.Country = Country;
        //        objUserInfoToBeSaved.EmailID = EmailID;
        //        objUserInfoToBeSaved.MobileNumber = Mobile;
        //        objUserInfoToBeSaved.Password = Password;

        //        if (iGstSvc.Save_User(isOnlyDelete.Trim().ToUpper() == "Y", objUserInfoToBeSaved, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
        //        {
        //            return Json("Ok", JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Methods
        public void ListStaticValue_RegtypeDeliveryPaymentTitleSafix(string SelRegType, string SelDelivery, string SelPayment, string SelTitle, string SelSafix)
        {
            List<StaticValuInfo> countries = CommonMethods.ListStaticValue();
            System.Text.StringBuilder sbPrefferedDeliveryMethodList = new System.Text.StringBuilder();
            System.Text.StringBuilder sbPrefferedPaymentMethodList = new System.Text.StringBuilder();
            System.Text.StringBuilder sbGSTRegistrationTypeList = new System.Text.StringBuilder();
            System.Text.StringBuilder sbTitleList = new System.Text.StringBuilder();
            System.Text.StringBuilder sbSafixList = new System.Text.StringBuilder();

            sbPrefferedDeliveryMethodList.AppendLine("<option value=\"\">Select one</option>");
            sbPrefferedPaymentMethodList.AppendLine("<option value=\"\">Select one</option>");
            sbGSTRegistrationTypeList.AppendLine("<option value=\"\">Select one</option>");
            sbTitleList.AppendLine("<option value=\"\">Select one</option>");
            sbSafixList.AppendLine("<option value=\"\">Select one</option>");

            if (countries != null && countries.Count > 0)
            {
                foreach (StaticValuInfo country in countries)
                {
                    if (country.Key == "GSTRegistrationType")
                    {
                        if (!string.IsNullOrEmpty(SelRegType) && country.Id.Trim() == SelRegType.Trim())
                            sbGSTRegistrationTypeList.AppendLine("<option value=\"" + country.Id + "\" selected=\"selected\">" + country.Value + "</option>");
                        else
                            sbGSTRegistrationTypeList.AppendLine("<option value=\"" + country.Id + "\">" + country.Value + "</option>");
                    }
                    else if (country.Key == "UserNameTitle")
                    {
                        if (!string.IsNullOrEmpty(SelTitle) && country.Id.Trim() == SelTitle.Trim())
                            sbTitleList.AppendLine("<option value=\"" + country.Id + "\" selected=\"selected\">" + country.Value + "</option>");
                        else
                            sbTitleList.AppendLine("<option value=\"" + country.Id + "\">" + country.Value + "</option>");
                    }
                    else if (country.Key == "UserNameSafix")
                    {
                        if (!string.IsNullOrEmpty(SelSafix) && country.Id.Trim() == SelSafix.Trim())
                            sbSafixList.AppendLine("<option value=\"" + country.Id + "\" selected=\"selected\">" + country.Value + "</option>");
                        else
                            sbSafixList.AppendLine("<option value=\"" + country.Id + "\">" + country.Value + "</option>");
                    }
                    else if (country.Key == "DeliveryMethod")
                    {
                        if (!string.IsNullOrEmpty(SelDelivery) && country.Id.Trim() == SelDelivery.Trim())
                            sbPrefferedDeliveryMethodList.AppendLine("<option value=\"" + country.Id + "\" selected=\"selected\">" + country.Value + "</option>");
                        else
                            sbPrefferedDeliveryMethodList.AppendLine("<option value=\"" + country.Id + "\">" + country.Value + "</option>");
                    }
                    else if (country.Key == "PaymentMethod")
                    {
                        if (!string.IsNullOrEmpty(SelPayment) && country.Id.Trim() == SelPayment.Trim())
                            sbPrefferedPaymentMethodList.AppendLine("<option value=\"" + country.Id + "\" selected=\"selected\">" + country.Value + "</option>");
                        else
                            sbPrefferedPaymentMethodList.AppendLine("<option value=\"" + country.Id + "\">" + country.Value + "</option>");
                    }
                }
            }

            ViewBag.PrefferedDeliveryMethod = sbPrefferedDeliveryMethodList.ToString();
            ViewBag.PrefferedPaymentMethod = sbPrefferedPaymentMethodList.ToString();
            ViewBag.GSTRegistrationType = sbGSTRegistrationTypeList.ToString();
            ViewBag.Title = sbTitleList.ToString();
            ViewBag.Safix = sbSafixList.ToString();
        }
        #endregion

    }
}