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
    public partial class ReportController : Controller
    {
        string ErrorMessage = "";

        public ActionResult Get_ReportData(string t)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            if (t.Trim().ToUpper() == "E")
            {
                using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
                {
                    ViewBag.ReportType = "E";
                    ViewBag.ReportData = iGstSvc.GetList_Expense("", "", "", OrganizationCode, "", "", "", "");
                }
            }
            else if (t.Trim().ToUpper() == "S")
            {
                using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
                {
                    ViewBag.ReportType = "S";
                    ViewBag.ReportData = iGstSvc.GetList_Bill("", "", "", OrganizationCode, "", "", "", "");
                }
            }
            else
            {
                ViewBag.ReportType = null;
                ViewBag.ReportData = null;
            }

            return View("~/Views/MasterPages/Reports.cshtml");
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