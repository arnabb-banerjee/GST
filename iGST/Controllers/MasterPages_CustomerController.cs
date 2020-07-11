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
    public partial class CustomerController : Controller
    {
        string ErrorMessage = "";
        
        [Route("customerddlist")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_CustomerForDropdownlist(string UserType)
        {
            if (Session["CustomerList"] == null)
            {
                string OrganizationCode = "";
                if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
                {
                    OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
                }

                using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
                {
                    List<CustomerInfo> list = iGstSvc.GetList_Customer(UserType.Trim().ToUpper(), "", "", OrganizationCode, false, "", "");
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json((List<CustomerInfo>)Session["CustomerList"], JsonRequestBehavior.AllowGet);
            }
        }

        [Route("customers")]
        [ValidateUserSession(ActionName = "Customer")]
        public ActionResult GetList_Customer()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                return View("~/Views/MasterPages/CustomerList.cshtml", iGstSvc.GetList_Customer("C", "", "", OrganizationCode, false, "", ""));
            }
        }

        [Route("customers/{org}")]
        [ValidateUserSession(ActionName = "Customer")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Customer(string CustomerID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                CustomerInfo obj = iGstSvc.GetDetails_Customer("C", CustomerID, "", OrganizationCode, false, "", "");

                ViewBag.Organizations = CommonMethods.ListOrganizations(obj.OrganizationCode);

                ViewBag.Countries_Basic = CommonMethods.ListCountry(obj.Country);
                ViewBag.Countries_Ship = CommonMethods.ListCountry(obj.Shipping_Country);
                ViewBag.Countries_Bill = CommonMethods.ListCountry(obj.Billing_Country);

                ViewBag.State_Basic = CommonMethods.ListStates(obj.Country, obj.State);
                ViewBag.State_Ship = CommonMethods.ListStates(obj.Shipping_Country, obj.Shipping_State);
                ViewBag.State_Bill = CommonMethods.ListStates(obj.Billing_Country, obj.Billing_State);

                ListStaticValue_RegtypeDeliveryPaymentTitleSafix(obj.GSTRegistrationType, obj.PrefferedDeliveryMethod, obj.PrefferedPaymentMethod, obj.Title, obj.Safix);

                return PartialView("~/Views/MasterPages/CustomerDetails.cshtml", obj);
            }
        }

        [ValidateUserSession(ActionName = "Customer")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Customer(string UserType, string isDelete, string CusID,
                string Title, string First_Name, string Middle_Name, string Last_Name, string DOB, string Sex, string Safix, string ParentCusID, string BillingWith, string IsActive, string OrganizationCode,
                string EmailID, string Mobile, string Street1, string Street2, string City, string State, string Country, string Website,
                string GSTRegistrationType, string GSTIN, string IsSubCustomer, string TaxRegNo, string CSTRegNo, string PANNo, string Terms,
                string PrefferedPaymentMethod, string PrefferedDeliveryMethod,
                string Shipping_Street1, string Shipping_Street2, string Shipping_City, string Shipping_State, string Shipping_Country,
                string Billing_Street1, string Billing_Street2, string Billing_City, string Billing_State, string Billing_Country,
                string OpeningBalance, string AsOfDate,
                string Notes)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                CustomerInfo objCustomerInfo = new CustomerInfo();
                objCustomerInfo.CusID = CusID;
                objCustomerInfo.Title = Title;
                objCustomerInfo.First_Name = First_Name;
                objCustomerInfo.Middle_Name = Middle_Name;
                objCustomerInfo.Last_Name = Last_Name;
                objCustomerInfo.DOB = DOB;
                objCustomerInfo.Sex = Sex;
                objCustomerInfo.Safix = Safix;
                objCustomerInfo.ParentCusID = ParentCusID;
                objCustomerInfo.BillingWith = BillingWith;
                objCustomerInfo.IsActive = IsActive == "Y";
                objCustomerInfo.OrganizationCode = OrganizationCode;
                objCustomerInfo.EmailID = EmailID;
                objCustomerInfo.Mobile = Mobile;
                objCustomerInfo.Street1 = Street1;
                objCustomerInfo.Street2 = Street2;
                objCustomerInfo.City = City;
                objCustomerInfo.State = State;
                objCustomerInfo.Country = Country;
                objCustomerInfo.Website = Website;
                objCustomerInfo.GSTRegistrationType = GSTRegistrationType;
                objCustomerInfo.GSTIN = GSTIN;
                objCustomerInfo.IsSubCustomer = IsSubCustomer == "Y";
                objCustomerInfo.TaxRegNo = TaxRegNo;
                objCustomerInfo.CSTRegNo = CSTRegNo;
                objCustomerInfo.PANNo = PANNo;
                objCustomerInfo.Terms = Terms;
                objCustomerInfo.PrefferedPaymentMethod = PrefferedPaymentMethod;
                objCustomerInfo.PrefferedDeliveryMethod = PrefferedDeliveryMethod;
                objCustomerInfo.Shipping_Street1 = Shipping_Street1;
                objCustomerInfo.Shipping_Street2 = Shipping_Street2;
                objCustomerInfo.Shipping_City = Shipping_City;
                objCustomerInfo.Shipping_State = Shipping_State;
                objCustomerInfo.Shipping_Country = Shipping_Country;
                objCustomerInfo.Billing_Street1 = Billing_Street1;
                objCustomerInfo.Billing_Street2 = Billing_Street2;
                objCustomerInfo.Billing_City = Billing_City;
                objCustomerInfo.Billing_State = Billing_State;
                objCustomerInfo.Billing_Country = Billing_Country;
                objCustomerInfo.OpeningBalance = OpeningBalance;
                objCustomerInfo.AsOfDate = AsOfDate;
                objCustomerInfo.Notes = Notes;

                if (iGstSvc.Save_Customer(UserType.Trim().ToUpper(), isDelete == "Y", objCustomerInfo, ((UserInfo)Session["UserDetails"]).UserCode, out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("uploadcustomerimage")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_CustomerImageList(string isOnlyDelete, string ImageId, string CustomerId, string IsMain, string IsActive, HttpPostedFileBase[] files)
        {
            string fileName = "", path = "";
            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                CustomerImageInfo objCustomerImage = null;

                if (files == null && isOnlyDelete.Trim().ToUpper() == "Y")
                {
                    objCustomerImage = new CustomerImageInfo();

                    objCustomerImage.CustomerId = CustomerId.Trim().Length == 0 ? 0 : Convert.ToInt32(CustomerId);
                    objCustomerImage.ImageId = ImageId.Trim().Length == 0 ? 0 : Convert.ToInt32(ImageId);
                    objCustomerImage.IsActive = IsActive.Trim().ToUpper();
                    objCustomerImage.Ismain = IsMain.Trim().ToUpper();
                    objCustomerImage.IsActive = IsActive.Trim().ToUpper();

                    if (iGstSvc.Save_CustomerImage(isOnlyDelete.Trim().ToUpper() == "Y", objCustomerImage, ((UserInfo)Session["UserDetails"]).UserCode, out ErrorMessage))
                    {
                        //If delete success then file should be deleted
                        if (isOnlyDelete.Trim().ToUpper() == "Y")
                        {
                            try
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                }
                            }
                            catch (Exception ex) { ErrorMessage = ErrorMessage + ex.Message; }
                        }

                        return Json("Ok", JsonRequestBehavior.AllowGet);
                    }
                }
                if (files != null)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        objCustomerImage = new CustomerImageInfo();

                        if (ImageId.Trim().Replace("0", "").Length == 0)
                        {
                            fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                            path = System.IO.Path.Combine(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadDataPath"].ToString()), fileName);
                            file.SaveAs(path);
                            objCustomerImage.FileName = fileName;
                            objCustomerImage.FileType = file.ContentType;
                        }


                        objCustomerImage.CustomerId = Convert.ToInt32(CustomerId);
                        objCustomerImage.ImageId = ImageId.Trim().Length == 0 ? 0 : Convert.ToInt32(ImageId);
                        objCustomerImage.IsActive = IsActive.Trim().ToUpper();
                        objCustomerImage.Ismain = IsMain.Trim().ToUpper();
                        objCustomerImage.IsActive = IsActive.Trim().ToUpper();

                        if (iGstSvc.Save_CustomerImage(isOnlyDelete.Trim().ToUpper() == "Y", objCustomerImage, ((UserInfo)Session["UserDetails"]).UserCode, out ErrorMessage))
                        {
                            //If delete success then file should be deleted
                            if (isOnlyDelete.Trim().ToUpper() == "Y")
                            {
                                try
                                {
                                    if (System.IO.File.Exists(path))
                                    {
                                        System.IO.File.Delete(path);
                                    }
                                }
                                catch (Exception ex) { ErrorMessage = ErrorMessage + ex.Message; }

                                if (ErrorMessage.Trim().Length > 0 && ImageId.Trim().Replace("0", "").Length == 0)
                                {
                                    try
                                    {
                                        if (System.IO.File.Exists(path))
                                        {
                                            System.IO.File.Delete(path);
                                        }
                                    }
                                    catch (Exception ex) { ErrorMessage = ErrorMessage + ex.Message; }
                                }

                                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }

                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }

            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("getimageforcustomer")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain)
        {
            using (Bill_Svc.BillServiceClient iGstSvc = new Bill_Svc.BillServiceClient())
            {
                List<CustomerImageInfo> list = iGstSvc.GetList_CustomerImage(ImageId, CustomerID, "", IsMain);

                if (list != null && list.Count > 0 && list[0].ImageId > 0)
                {
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("NO", JsonRequestBehavior.AllowGet);
                }
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