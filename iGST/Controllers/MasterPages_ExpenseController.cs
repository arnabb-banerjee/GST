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
    public partial class ExpenseController : Controller
    {
        string ErrorMessage = "";

        [Route("applicableexpense")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ExpenseOrganization()
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

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                ViewBag.Organizations = CommonMethods.ListOrganizations("");

                return View("~/Views/MasterPages/ExpenseOrganizationList.cshtml", new Expense_Svc.ExpenseServiceClient().GetList_ExpenseOrganization("", "", "", OrganizationCode, "", true, Session["Language"].ToString()));
            }
        }

        [Route("editexpensedetails")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ExpenseOrganizationAddEdit(string OrganizationproductId)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            string OrganizationCode = "";
            int CountryId = 0;

            int.TryParse(((UserInfo)Session["UserDetails"]).Country, out CountryId);

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
            {
                // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
                var list = iGstSvc.GetList_ExpenseOrganization(OrganizationproductId, "", "", OrganizationCode, "", true, Session["Language"].ToString());
                // END: Modified on[30th August 2019] by[Partha]
                var isListNotnull = list != null && list[0] != null;
                var orgcode = isListNotnull && list[0].OrganizationCode != null ? list[0].OrganizationCode : "";

                if (string.IsNullOrEmpty(OrganizationCode))
                {
                    ViewBag.Organizations = CommonMethods.ListOrganizations(orgcode);
                }

                ViewBag.Suppliers = CommonMethods.ListSupplier(orgcode, list[0].SupplierId.ToString());
                ViewBag.PreferedSuppliers = CommonMethods.ListSupplier(orgcode, list[0].PreferredSupplierId.ToString());
                ViewBag.IncomeAccounts = CommonMethods.ListBanks(orgcode, isListNotnull && list[0].IncomeAccount != null ? list[0].IncomeAccount : "");
                ViewBag.Categories = CommonMethods.ListCategory(isListNotnull && list[0].CategoryId != null ? list[0].CategoryId : "", CountryId.ToString());
                ViewBag.ServiceUnits = CommonMethods.ListServiceUnit(list[0].Unit.ToString());
                ViewBag.ServiceTypes = CommonMethods.ListServiceType(list[0].ServiceType.ToString());
                ViewBag.ServiceClasses = CommonMethods.ListServiceClass(list[0].Class.ToString());

                return PartialView("~/Views/MasterPages/ExpenseOrganizationAddEdit.cshtml", list[0]);
            }
        }

        [Route("viewexpensedetails/{org}")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ExpenseOrganization(string org)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            org = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                org = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
            {
                return PartialView("~/Views/MasterPages/ExpenseOrganizationDetails.cshtml", iGstSvc.GetDetails_ExpenseOrganization("", "", "", org, "", true, Session["Language"].ToString()));
            }
        }

        [Route("manageexpenseforuser")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ExpenseOrganization(string isOnlyDelete, string OrganizationproductId, string ProductId, string CategoryId, string CountryId, string OrganizationCode, string IsActive,
                string Name, string Description, string SKU, string Unit, string Class, string AbatementPercentage, string ServiceType,
                string SalePrice, string isInclusiveTax, string AvailableQty, string IncomeAccount, string SupplierId,
                string PreferredSupplierId, string ReverseCharge, string PurchaseTax, string SaleTax)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
            {
                // Modified on [3rd August 2019] by [Partha] cause [Giving NULL reference exception 
                // when Organisation Code is null] - Start
                if (OrganizationproductId == null)
                {
                    OrganizationproductId = "";
                }
                // End: Modified on [3rd August 2019]
                ProductOrganiztionInfo objProduct = new ProductOrganiztionInfo();
                objProduct.OrganizationproductId = OrganizationproductId.Trim().Length == 0 ? 0 : Convert.ToInt32(OrganizationproductId.Trim());
                objProduct.ProductId = ProductId;
                objProduct.CategoryId = CategoryId;
                objProduct.CountryId = CountryId;
                objProduct.OrganizationCode = OrganizationCode;
                objProduct.IsActive = IsActive.Trim().ToUpper();

                objProduct.Name = Name;
                objProduct.Description = Description;
                objProduct.ServiceType = ServiceType.Trim().Length > 0 ? Convert.ToInt32(ServiceType) : 0;
                objProduct.SKU = SKU;
                objProduct.Unit = Unit.Trim().Length == 0 ? 0 : Convert.ToInt32(Unit);
                objProduct.Class = Class.Trim().Length == 0 ? 0 : Convert.ToInt32(Class);
                objProduct.AbatementPercentage = AbatementPercentage.Trim().Length == 0 ? 0 : Convert.ToInt32(AbatementPercentage);
                objProduct.SalePrice = SalePrice.Trim().Length == 0 ? 0 : Convert.ToInt32(SalePrice);
                objProduct.isInclusiveTax = isInclusiveTax.Trim().ToUpper() == "Y";
                objProduct.AvailableQty = AvailableQty.Trim().Length == 0 ? 0 : Convert.ToInt32(AvailableQty);
                objProduct.IncomeAccount = IncomeAccount;
                objProduct.SupplierId = SupplierId.Trim().Length == 0 ? 0 : Convert.ToInt32(SupplierId);
                objProduct.PreferredSupplierId = PreferredSupplierId.Trim().Length == 0 ? 0 : Convert.ToInt32(PreferredSupplierId);
                objProduct.ReverseCharge = ReverseCharge.Trim().Length == 0 ? 0 : Convert.ToInt32(ReverseCharge);
                objProduct.PurchaseTax = PurchaseTax.Trim().Length == 0 ? 0 : Convert.ToInt32(PurchaseTax);
                objProduct.SaleTax = SaleTax.Trim().Length == 0 ? 0 : Convert.ToInt32(SaleTax);


                if (iGstSvc.Save_ExpenseOrganization(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("expenses")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Expense()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
            {
                return View("~/Views/MasterPages/ExpenseList.cshtml", iGstSvc.GetList_Expense("", "", "", OrganizationCode, "", "", "", ""));
            }
        }

        [Route("expense")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            bool isRegisteredUser = false;
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                isRegisteredUser = true;
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
            {
                InvoiceInfo obj = iGstSvc.GetDetails_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
                ViewBag.Customers = CommonMethods.ListSupplier(obj.OrganizationCode, obj.CusID);

                if (!isRegisteredUser)
                {
                    ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);
                }

                return PartialView("~/Views/MasterPages/ExpenseDetails.cshtml", obj);
            }
        }

        [Route("saveexpense")]
        [ValidateUserSession(ActionName = "Bill")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Expense(string isOnlyDelete, string OrganizationCode, string BillId, string CusID, string BranchID, string BillDate, string DueDate, string ShipAddress, string ShipCity, string ShipCountry, string ShipState, string xmlstring, string ChangedCurrency, string ConversionRate, string PrevConversionRate, string IsReturned, string Iscancelled, string XMLExpenseData, string PaidAmount, string DueAmount, string SumAmount)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Expense_Svc.ExpenseServiceClient iGstSvc = new Expense_Svc.ExpenseServiceClient())
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
                // Modified on [18 Oct 2019] to add expense fieled - Start
                objBill.TravellingExpenses = XMLExpenseData;
                // Modified on [18 Oct 2019] to add expense fieled - End
                objBill.AmountPayable = PaidAmount;
                objBill.AmountDue = DueAmount;
                objBill.PrevConversionRate = PrevConversionRate;
                objBill.SumAmount = SumAmount;
                objBill.OrganizationCode = OrganizationCode;

                if (iGstSvc.Save_Expense(isOnlyDelete.Trim().ToUpper() == "Y", objBill, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        // Added By Partha on [27th August 2019] for user Expenses
        [Route("myexpense")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_MyExpense()
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

            using (Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient())
            {
                // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
                return View("~/Views/MasterPages/MyExpenseList.cshtml", iGstSvc.GetList_ProductOrganization("", "", "", OrganizationCode, "", true, Session["Language"].ToString()));
                // END: Modified on[30th August 2019] by[Partha]
            }
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