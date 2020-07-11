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
    public partial class SettigsController : Controller
    {
        string ErrorMessage = "";

        [Route("settingcompany")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_SettingsCompany()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && (
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" ||
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "A"
                ) && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                SettingsInfo obj = iGstSvc.GetDetails_Settings(OrganizationCode, true);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                ViewBag.Countries = CommonMethods.ListCountry(obj.c_Country);
                ViewBag.States = CommonMethods.ListStates(obj.c_Country, obj.c_State);

                return View("~/Views/MasterPages/SettingsCompany.cshtml", obj);
            }
        }

        [Route("settingpayment")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_SettingsPayment()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && (
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" ||
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "A"
                ) && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                SettingsInfo obj = iGstSvc.GetDetails_Settings(OrganizationCode, true);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);

                return View("~/Views/MasterPages/SettingsPayment.cshtml", obj);
            }
        }

        [Route("settingcurrency")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_SettingsCurrency()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && (
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" ||
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "A"
                ) && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                SettingsInfo obj = iGstSvc.GetDetails_Settings(OrganizationCode, true);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);

                return View("~/Views/MasterPages/SettingsCurrency.cshtml", obj);
            }
        }

        [Route("settingalertnotification")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_SettingsAlertNotification()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && (
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" ||
                    ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "A"
                ) && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                SettingsInfo obj = iGstSvc.GetDetails_Settings(OrganizationCode, true);

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);

                return View("~/Views/MasterPages/SettingsAlertNotification.cshtml", obj);
            }
        }

        [Route("settingssave")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Settings(string an_AlertText_GSTDate, string an_AlertText_PaidMembership, string an_isAllowedAlert_GSTDate, string an_isAllowedAlert_PaidMembership,
            string c_isAllowedMultyLanguage, string InfoType, string mc_CurrencyList, string mc_isAllowedMutyCurrency,
            string OrganizationCode, string p_BankAccountHolder, string p_BankAccountIBankName,
            string p_BankAccountIBranchName, string p_BankAccountIFSCCode, string p_BankAccountIMCRCode, string p_BankAccountNumber,
            string p_isAllowedOnlinePayment, string p_PaypalAccountID,
            string CompanyName, string Email, string Mobile, string Address, string City, string State, string Country,
            string Website, string CIN, string PAN, string DefaultEmail, string SMTP)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                SettingsInfo objSettings = new SettingsInfo();

                objSettings.an_AlertText_GSTDate = an_AlertText_GSTDate;
                objSettings.an_AlertText_PaidMembership = an_AlertText_PaidMembership;
                objSettings.an_isAllowedAlert_GSTDate = an_isAllowedAlert_GSTDate.Trim().ToUpper() == "Y";
                objSettings.an_isAllowedAlert_PaidMembership = an_isAllowedAlert_PaidMembership.Trim().ToUpper() == "Y";
                objSettings.c_isAllowedMultyLanguage = c_isAllowedMultyLanguage.Trim().ToUpper() == "Y";
                objSettings.InfoType = InfoType;
                objSettings.mc_CurrencyList = mc_CurrencyList;
                objSettings.mc_isAllowedMutyCurrency = mc_isAllowedMutyCurrency.Trim().ToUpper() == "Y";
                objSettings.OrganizationCode = OrganizationCode;
                objSettings.p_BankAccountHolder = p_BankAccountHolder;
                objSettings.p_BankAccountIBankName = p_BankAccountIBankName;
                objSettings.p_BankAccountIBranchName = p_BankAccountIBranchName;
                objSettings.p_BankAccountIFSCCode = p_BankAccountIFSCCode;
                objSettings.p_BankAccountIMCRCode = p_BankAccountIMCRCode;
                objSettings.p_BankAccountNumber = p_BankAccountNumber;
                objSettings.p_isAllowedOnlinePayment = p_isAllowedOnlinePayment.Trim().ToUpper() == "Y";
                objSettings.p_PaypalAccountID = p_PaypalAccountID;

                objSettings.c_CompanyName = CompanyName;
                objSettings.c_Email = Email;
                objSettings.c_Mobile = Mobile;
                objSettings.c_Address = Address;
                objSettings.c_City = City;
                objSettings.c_State = State;
                objSettings.c_Country = Country;
                objSettings.c_Website = Website;
                objSettings.c_SMTP = SMTP;
                objSettings.c_CIN = CIN;
                objSettings.c_PAN = PAN;
                objSettings.c_DefaultEmail = DefaultEmail;
                objSettings.c_SMTP = SMTP;

                if (iGstSvc.Save_Settings(false, objSettings, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }


        [Route("applicablecurrencies")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_CurrencyOrganization()
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            ViewBag.Organizations = CommonMethods.ListOrganizations(OrganizationCode);

            using (Currency_Svc.CurrencyServiceClient iGstSvc = new Currency_Svc.CurrencyServiceClient())
            {
                return View("~/Views/MasterPages/SettingsApplicableCurrency.cshtml", iGstSvc.GetDetails_CurrencyOrganization("", "", OrganizationCode));
            }
        }

        [Route("managecurrencyforuser")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_CurrencyOrganization(string isOnlyDelete, string OrganizationproductId, string CurrencyId, string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Currency_Svc.CurrencyServiceClient iGstSvc = new Currency_Svc.CurrencyServiceClient())
            {
                // Modified on [3rd August 2019] by [Partha] cause [Giving NULL reference exception 
                // when Organisation Code is null] - Start
                if (OrganizationproductId == null)
                {
                    OrganizationproductId = "";
                }
                // End: Modified on [3rd August 2019]
                CurrencyOrganiztionInfo objProduct = new CurrencyOrganiztionInfo();
                objProduct.OrganizationproductId = OrganizationproductId.Trim().Length == 0 ? 0 : Convert.ToInt32(OrganizationproductId.Trim());
                objProduct.OrganizationCode = OrganizationCode;
                objProduct.CurrencyId = CurrencyId;
                
                if (iGstSvc.Save_CurrencyOrganization(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
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