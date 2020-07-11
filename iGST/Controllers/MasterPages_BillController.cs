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
    public partial class InvoiceController : Controller
    {
        string ErrorMessage = "";

        [Route("invoices")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Bill()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                ViewBag.Organizations = CommonMethods.ListOrganizations(OrganizationCode);
                return View("~/Views/MasterPages/BillList.cshtml", iGstSvc.GetList_Bill("", "", "", OrganizationCode, "", "", "", ""));
            }
        }

        [Route("invoice/{org}")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Bill(string InvoiceID, string BranchID, string CusID, string org, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                org = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                InvoiceInfo obj = iGstSvc.GetDetails_Bill(InvoiceID, BranchID, CusID, org, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
                ViewBag.Customers = CommonMethods.ListCustomer(org, obj.CusID);

                ViewBag.Organizations = CommonMethods.ListOrganizations(org);
                ViewBag.Currencies = CommonMethods.ListApplicableCurrencies(obj.ChangedCurrency);

                return PartialView("~/Views/MasterPages/BillDetails.cshtml", obj);
            }
        }

        [Route("saveinvoice")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Bill(string isOnlyDelete, string OrganizationCode, string BillId, string CusID, string BranchID, string BillDate, string DueDate, string ShipAddress, string ShipCity, string ShipCountry, string ShipState, string xmlstring, string ChangedCurrency, string ConversionRate, string PrevConversionRate, string IsReturned, string Iscancelled, string PaidAmount, string DueAmount, string SumAmount)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                InvoiceInfo objBill = new InvoiceInfo();

                objBill.InvoiceID = BillId;
                objBill.BranchID = BranchID;
                objBill.CusID = CusID;
                objBill.ShipAddress = ShipAddress;
                objBill.ShipCity = ShipCity;
                objBill.ShipState = ShipState;
                objBill.ShipCountry = ShipCountry;
                objBill.ProductIDs = xmlstring;
                objBill.ChangedCurrency = ChangedCurrency;
                objBill.ConversionRate = ConversionRate;
                objBill.IsReturned = IsReturned.Trim().ToUpper() == "Y";
                objBill.IsCancelled = Iscancelled.Trim().ToUpper() == "Y";

                objBill.AmountPayable = PaidAmount;
                objBill.AmountDue = DueAmount;
                objBill.PrevConversionRate = PrevConversionRate;
                objBill.SumAmount = SumAmount;
                objBill.OrganizationCode = OrganizationCode;

                if (iGstSvc.Save_Bill(isOnlyDelete.Trim().ToUpper() == "Y", objBill, ((UserInfo)Session["UserDetails"]).UserCode, out ErrorMessage))
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