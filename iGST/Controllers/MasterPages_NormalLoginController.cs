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
    public class NormalLoginController : Controller
    {
        string ErrorMessage = "";

        //[Route("login")]
        public ActionResult GoToLogin()
        {
            ViewBag.Countries = CommonMethods.ListCountry("");

            return View("~/Views/MasterPages/UserRegistrationLogin.cshtml");
        }

        [Route("processlogin")]
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult User_Login(string UserCode, string Password, string UserType, string returnUrl)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                try
                {
                    UserInfo objUserInfo = iGstSvc.User_Login(UserCode, Password, UserType, out ErrorMessage);
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
                                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                            case SignInStatus.Failure:
                            default:
                                return Json("Invalid login attempt.", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {

                    return Json(ex.Message + ex.StackTrace + ex.InnerException, JsonRequestBehavior.AllowGet);
                    //Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        // POST: /Account/LogOff
        //[HttpPost]
        [Route("logout")]
        public ActionResult LogOff()
        {
            Session["CustomerList"] = null;
            Session["UserDetails"] = null;
            Session["Language"] = null;
            Session["LanguageCountrtyList"] = null;
            Session["ProductList"] = null;
            Session["Organization"] = null;
            Session["Customers"] = null;
            Session["Suppliers"] = null;
            Session["Banks"] = null;
            Session["ProductOrganiztion"] = null;

            System.Web.HttpContext.Current.Session["CustomerList"] = null;
            System.Web.HttpContext.Current.Session["UserDetails"] = null;
            System.Web.HttpContext.Current.Session["Language"] = null;
            System.Web.HttpContext.Current.Session["LanguageCountrtyList"] = null;
            System.Web.HttpContext.Current.Session["ProductList"] = null;
            System.Web.HttpContext.Current.Session["Organization"] = null;
            System.Web.HttpContext.Current.Session["Customers"] = null;
            System.Web.HttpContext.Current.Session["Suppliers"] = null;
            System.Web.HttpContext.Current.Session["Banks"] = null;
            System.Web.HttpContext.Current.Session["ProductOrganiztion"] = null;
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [Route("changepassword")]
        public ActionResult User_ChangePassword(string OldPssword, string NewPassword)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/UserPasswordChange.cshtml");
            }
        }

        [Route("passwordsave")]
        public ActionResult User_PasswordSave(string OldPssword, string NewPassword)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                string ErrorMessage = "";
                bool bSuccess = iGstSvc.User_ChangePassword(OldPssword, NewPassword, (UserInfo)Session["UserDetails"], out ErrorMessage);

                if (ErrorMessage.Trim().Length > 0)
                {
                    return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

        }

        [Route("forgotpassword")]
        public ActionResult User_ForgotPassword(string UserCodeEmailIDMobile, string OTPSendOption)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                string OTPValidityDuration = System.Configuration.ConfigurationManager.AppSettings["OTPValidityDuration"].ToString();
                string OTP = "";
                string ErrorMessage = "";
                bool bSuccess = iGstSvc.User_ForgotPassword(UserCodeEmailIDMobile, OTPValidityDuration, OTPSendOption, "", out OTP, out ErrorMessage);

                if (ErrorMessage.Trim().Length > 0)
                {
                    return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View("~/Views/MasterPages/UserRegistrationLogin.cshtml");
                }
            }

        }

        [Route("emailidverification")]
        public ActionResult Email_Verification(string EmailVerifictionKey)
        {
            string errormsg = "";
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                try
                {
                    if (!string.IsNullOrEmpty(EmailVerifictionKey))
                    {
                        iGstSvc.Email_Verification(EmailVerifictionKey, out errormsg);

                        if (errormsg.Trim().Length > 0)
                        {
                            return Json(errormsg, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Your email verified successfully", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {

                    return Json(ex.Message + ex.StackTrace + ex.InnerException, JsonRequestBehavior.AllowGet);
                    //Common.ErrorLog.LogSQLErrors_Comments(null, "Login-User-Method-wscall", ex);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

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