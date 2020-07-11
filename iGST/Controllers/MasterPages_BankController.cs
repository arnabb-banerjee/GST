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
    public partial class BankMasterController : Controller
    {
        string ErrorMessage = "";

        #region Bank Related
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_BankForDropdownlist(string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return Json(iGstSvc.GetList_Bank("", OrganizationCode, true), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("banks")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Bank()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/BankList.cshtml", iGstSvc.GetList_Bank("", OrganizationCode, true));
            }
        }

        [Route("bank")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Bank(string BankID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                BankInfo objBankInfo = iGstSvc.GetDetails_Bank(BankID, OrganizationCode, true);

                ViewBag.Organizations = CommonMethods.ListOrganizations(objBankInfo.OrganizationCode);
                ViewBag.OrganizationList = CommonMethods.ListOrganizations(objBankInfo.OrganizationCode == null ? "" : objBankInfo.OrganizationCode);

                return PartialView("~/Views/MasterPages/BankDetails.cshtml", objBankInfo);
            }
        }

        [Route("banksave")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Bank(string isOnlyDelete, string OrganizaionName, string BankId, string BankName, string CorpID, string Address, string IFSCCOde, string MCRCode, string IsActive)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizaionName = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                BankInfo objBank = new BankInfo();

                objBank.BankID = BankId;
                objBank.Name = BankName;
                objBank.CorpID = CorpID;
                objBank.IFSCCode = IFSCCOde;
                objBank.MCRCode = MCRCode;
                objBank.Address = Address;
                objBank.OrganizationCode = OrganizaionName;
                objBank.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_Bank(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("banktransactions")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_BankTransaction(string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/BankTransactionList.cshtml", iGstSvc.GetList_BankTransaction("", OrganizationCode, true));
            }
        }

        [Route("bankbanktransactiondetails")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_BankTransaction(string BankTransactionID, string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                BankTransactionInfo obj = iGstSvc.GetDetails_BankTransaction(BankTransactionID, OrganizationCode, true);
                ViewBag.OrganizationProducts = CommonMethods.ListOrganizationProducts(obj.OrganizationCode, obj.ProductIds);
                ViewBag.Customers = CommonMethods.ListCustomer(obj.OrganizationCode, obj.CustomerId);
                ViewBag.Banks = CommonMethods.ListBanks(obj.OrganizationCode, "");
                return PartialView("~/Views/MasterPages/BankTransactionDetails.cshtml", obj);
            }
        }

        [Route("banktransactionsave")]
        [ValidateUserSession(ActionName = "Bank")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_BankTransaction(string isOnlyDelete, string OrganizationCode, string Id, string Products, string CustomerId, string IsSellExpense, string Tax, string IsActive)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                BankTransactionInfo objBank = new BankTransactionInfo();

                objBank.Id = Id;
                objBank.ProductIds = Products;
                objBank.CustomerId = CustomerId;
                objBank.Tax = Tax;
                objBank.IsSellExpense = IsSellExpense;
                objBank.OrganizationCode = OrganizationCode;
                objBank.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_BankTransaction(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }
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