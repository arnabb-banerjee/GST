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
    public partial class ProductsController : Controller
    {
        string ErrorMessage = "";
        Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient();

        [Route("productddlist")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ProductForDropdownlist()
        {
            if (Session["ProductList"] != null && ((List<ProductInfo>)Session["ProductList"]).Count > 0)
            {
                return Json((List<ProductInfo>)Session["ProductList"], JsonRequestBehavior.AllowGet);
            }
            else
            {
                using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = 0;
                    }

                    if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
                    {
                        string OrganizationCode = "";
                        OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();

                        List<ProductInfo> list = new Product_Svc.ProductServiceClient().GetList_ProductDropdownlist("", "", false, OrganizationCode, "", false, Session["Language"].ToString());

                        if (list != null && list.Count > 0)
                        {
                            Session["ProductList"] = list;
                            return Json(list, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //return Json(new HttpStatusCodeResult(410, "You have to select your products from Product/ service page."), JsonRequestBehavior.AllowGet);
                            return new HttpStatusCodeResult(410, "You have to select your products from Product/ service page.");
                        }
                    }
                    else if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "M")
                    {
                        List<ProductInfo> list = new Product_Svc.ProductServiceClient().GetList_ProductDropdownlist("", "", false, "", "", false, Session["Language"].ToString());

                        if (list != null && list.Count > 0)
                        {
                            Session["ProductList"] = list;
                            return Json(list, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //return Json(new HttpStatusCodeResult(410, "There some problem arrised in product listing."), JsonRequestBehavior.AllowGet);
                            return new HttpStatusCodeResult(410, "There some problem arrised in product listing.");
                        }
                    }
                    else
                    {
                        List<ProductInfo> list = new Product_Svc.ProductServiceClient().GetList_ProductDropdownlist("", "", false, "", "", false, Session["Language"].ToString());

                        if (list != null && list.Count > 0)
                        {
                            Session["ProductList"] = list;
                            return Json(list, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new HttpStatusCodeResult(410, "There some problem arrised in product listing."), JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
        }

        [Route("products")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Product(string IsExpense)
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
                return View("~/Views/MasterPages/ProductList.cshtml", iGstSvc.GetList_Product("", "", "", OrganizationCode, "", true, Session["Language"].ToString()));
            }
        }

        [Route("product")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Product(string ProductID)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 1;
            }

            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            ProductInfo obj = iGstSvc.GetDetails_Product(ProductID, "", "", OrganizationCode, "", true, Session["Language"].ToString());

            int CountryId = 0;

            int.TryParse(((UserInfo)Session["UserDetails"]).Country, out CountryId);

            ViewBag.Categories = CommonMethods.ListCategory(obj.CategoryId, CountryId.ToString());
            ViewBag.Countries = CommonMethods.ListCountry(obj.CountryId);

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return PartialView("~/Views/MasterPages/ProductDetails.cshtml", obj);
            }
        }

        [Route("productsave")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Product(string isOnlyDelete, string ProductId, string CategoryId, string CountryId, string ProductName, string IsActive)
        {
            using (Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient())
            {
                ProductInfo objProduct = new ProductInfo();
                objProduct.ProductId = ProductId;
                objProduct.CategoryId = CategoryId;
                objProduct.CountryId = CountryId;
                objProduct.ProductName = ProductName;
                objProduct.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_Product(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("servicesproducts")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ProductOrganization()
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
                ViewBag.Organizations = CommonMethods.ListOrganizations("");

                return View("~/Views/MasterPages/ProductOrganizationList.cshtml", iGstSvc.GetList_ProductOrganization("", "", "", OrganizationCode, "", true, Session["Language"].ToString()));
            }
        }

        [Route("editproductdetails/{org}")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ProductOrganizationAddEdit(string OrganizationproductId, string org)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                org = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
                var list = new Product_Svc.ProductServiceClient().GetList_ProductOrganization(OrganizationproductId, "", "", org, "", true, Session["Language"].ToString());
                // END: Modified on[30th August 2019] by[Partha]

                if (string.IsNullOrEmpty(org) && (list != null && list[0] != null) && list[0].OrganizationCode != null)
                {
                    org = list[0].OrganizationCode;
                }


                ViewBag.Organizations = CommonMethods.ListOrganizations(org);

                ViewBag.Customers = CommonMethods.ListSupplier(org, list[0].SupplierId.ToString());
                ViewBag.PreferedSuppliers = CommonMethods.ListSupplier(org, list[0].SupplierId.ToString());
                ViewBag.IncomeAccounts = CommonMethods.ListBanks(org, (list != null && list[0] != null) && list[0].IncomeAccount != null ? list[0].IncomeAccount : "");
                ViewBag.Categories = CommonMethods.ListCategory((list != null && list[0] != null) && list[0].CategoryId != null ? list[0].CategoryId : "", "0");
                ViewBag.ServiceUnits = CommonMethods.ListServiceUnit(list[0].Unit.ToString());
                ViewBag.ServiceTypes = CommonMethods.ListServiceType(list[0].ServiceType.ToString());
                ViewBag.ServiceClasses = CommonMethods.ListServiceClass(list[0].Class.ToString());

                return PartialView("~/Views/MasterPages/ProductOrganizationAddEdit.cshtml", list[0]);
            }
        }

        [Route("viewproductdetails/{org}")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ProductOrganization(string org)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                org = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient())
            {
                List<ProductOrganiztionInfo> list = iGstSvc.GetDetails_ProductOrganization("", "", "", org, "", true, Session["Language"].ToString());

                if (list != null && list.Count() > 0)
                {
                    ViewBag.Organizations = CommonMethods.ListOrganizations(org);
                }

                return PartialView("~/Views/MasterPages/ProductOrganizationDetails.cshtml", list);
            }
        }

        [Route("manageproductforuser")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ProductOrganization(string isOnlyDelete, string OrganizationproductId, string ProductId, string CategoryId, string CountryId, string OrganizationCode, string IsActive,
                string Name, string Description, string SKU, string Unit, string Class, string AbatementPercentage, string ServiceType,
                string SalePrice, string isInclusiveTax, string AvailableQty, string IncomeAccount, string SupplierId,
                string PreferredSupplierId, string ReverseCharge, string PurchaseTax, string SaleTax)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient())
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


                if (iGstSvc.Save_ProductOrganization(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }


        [Route("getgst")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Get_Gst(string ProductId, string ShipStateId, string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return Json(iGstSvc.Get_Gst(ProductId, ShipStateId, OrganizationCode), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("getgstcategory")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Get_GstCategory(string CategoryId, string ShipStateId, string OrganizationCode)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return Json(iGstSvc.Get_GstCategory(CategoryId, ShipStateId, OrganizationCode), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("uploadroductorganizationimage")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ProductOrganizationImage(string isOnlyDelete, string ImageId, string OrganizationproductId, string ProductId, string IsMain, string IsActive, HttpPostedFileBase[] files)
        {
            string fileName = "", path = "";
            using (Product_Svc.ProductServiceClient iGstSvc = new Product_Svc.ProductServiceClient())
            {
                ProductOrganiztionImageInfo objProduct = null;

                if (files == null && isOnlyDelete.Trim().ToUpper() == "Y")
                {
                    objProduct = new ProductOrganiztionImageInfo();

                    objProduct.OrganizationproductId = OrganizationproductId.Trim().Length == 0 ? 0 : Convert.ToInt32(OrganizationproductId.Trim());
                    objProduct.ProductId = ProductId;
                    objProduct.ImageId = ImageId.Trim().Length == 0 ? 0 : Convert.ToInt32(ImageId);
                    objProduct.IsActive = IsActive.Trim().ToUpper();
                    objProduct.Ismain = IsMain.Trim().ToUpper();
                    objProduct.IsActive = IsActive.Trim().ToUpper();

                    if (iGstSvc.Save_ProductOrganizationImage(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
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
                        objProduct = new ProductOrganiztionImageInfo();

                        if (ImageId.Trim().Replace("0", "").Length == 0)
                        {
                            fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                            path = System.IO.Path.Combine(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadDataPath"].ToString()), fileName);
                            file.SaveAs(path);
                            objProduct.FileName = fileName;
                            objProduct.FileType = file.ContentType;
                        }


                        objProduct.OrganizationproductId = OrganizationproductId.Trim().Length == 0 ? 0 : Convert.ToInt32(OrganizationproductId.Trim());
                        objProduct.ProductId = ProductId;
                        objProduct.ImageId = ImageId.Trim().Length == 0 ? 0 : Convert.ToInt32(ImageId);
                        objProduct.IsActive = IsActive.Trim().ToUpper();
                        objProduct.Ismain = IsMain.Trim().ToUpper();
                        objProduct.IsActive = IsActive.Trim().ToUpper();

                        if (iGstSvc.Save_ProductOrganizationImage(isOnlyDelete.Trim().ToUpper() == "Y", objProduct, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
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
                }

                return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("getimagefororganizationproduct")]
        [ValidateUserSession(ActionName = "Product")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ProductOrganizationImage(string ImageId, string OrganizationproductId, string ProductID, string IsActive, string IsMain)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                List<ProductOrganiztionImageInfo> list = new Product_Svc.ProductServiceClient().GetList_ProductOrganizationImage(ImageId, OrganizationproductId, "", "", IsMain);

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