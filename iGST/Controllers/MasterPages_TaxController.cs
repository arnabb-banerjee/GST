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
    public partial class TaxController : Controller
    {
        string ErrorMessage = "";

        [Route("taxmasters")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_TaxMaster()
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/TaxMasterList.cshtml", iGstSvc.GetList_TaxMaster(0, ""));
            }
        }

        [Route("taxmaster")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_TaxMaster(string TaxDefinationID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return PartialView("~/Views/MasterPages/TaxMasterDetails.cshtml", iGstSvc.GetDetails_TaxMaster(0, TaxDefinationID));
            }
        }

        [Route("taxmastersave")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_TaxMaster(string isOnlyDelete, string TaxDefinationID, string TaxName)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                TaxMasterInfo objBank = new TaxMasterInfo();

                objBank.TaxDefinationID = TaxDefinationID;
                objBank.TaxName = TaxName;

                if (iGstSvc.Save_TaxMaster(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("taxcountries")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_TaxCountryMap()
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/TaxCountryMapList.cshtml", iGstSvc.GetList_TaxCountryMap(0, "", ""));
            }
        }

        [Route("taxcountry")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_TaxCountryMap(string TaxDefinationID, string CountryId)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                TaxCountryMapInfo obj = iGstSvc.GetDetails_TaxCountryMap(0, TaxDefinationID, CountryId);
                ViewBag.Taxes = CommonMethods.ListTaxes(obj.TaxDefinationID);
                ViewBag.Countries = CommonMethods.ListCountry(obj.CountryId);

                return PartialView("~/Views/MasterPages/TaxCountryMapDetails.cshtml", obj);
            }
        }

        [Route("taxcountrysave")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_TaxCountryMap(string isOnlyDelete, string TaxDefinationID, string CountryId, string ApplicableType)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                TaxCountryMapInfo objBank = new TaxCountryMapInfo();

                objBank.TaxDefinationID = TaxDefinationID;
                objBank.CountryId = CountryId;
                objBank.ApplicableType = ApplicableType;

                if (iGstSvc.Save_TaxCountryMap(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        [Route("taxcountrycategories")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_TaxCountryCategoryMap()
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/TaxCountryCategoryMapDetails.cshtml", iGstSvc.GetList_TaxCountryCategoryMap(0, "", "", ""));
            }
        }

        [Route("taxcountrycategory")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_TaxCountryCategoryMap(string TaxDefinationID, string CountryId, string CategoryId)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return PartialView("~/Views/MasterPages/TaxCountryCategoryMapDetails.cshtml", iGstSvc.GetDetails_TaxCountryCategoryMap(0, TaxDefinationID, CountryId, CategoryId));
            }
        }

        [Route("taxcountrycategorysave")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_TaxCountryCategoryMap(string isOnlyDelete, string TaxDefinationID, string CountryId, string CategoryId, string ApplicableType, string Percentage)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                TaxCountryCategoryMapInfo objBank = new TaxCountryCategoryMapInfo();

                objBank.TaxDefinationID = TaxDefinationID;
                objBank.CountryId = CountryId;
                objBank.CategoryId = CategoryId;
                objBank.ApplicableType = ApplicableType;
                objBank.Percentage = Percentage;

                if (iGstSvc.Save_TaxCountryCategoryMap(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        // Added by Partha on 10/07/2019
        [Route("taxexpensecountrycategories")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_TaxExpenseCountryCategoryMap()
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/TaxExpenseCountryCategoryMapDetails.cshtml", iGstSvc.GetList_TaxExpenseCountryCategoryMap(0, "", "", ""));
            }
        }

        [Route("taxexpensecountrycategory")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_TaxExpenseCountryCategoryMap(string TaxDefinationID, string CountryId, string CategoryId)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return PartialView("~/Views/MasterPages/TaxExpenseCountryCategoryMapDetails.cshtml", iGstSvc.GetDetails_TaxExpenseCountryCategoryMap(0, TaxDefinationID, CountryId, CategoryId));
            }
        }

        [Route("taxexpensecountrycategorysave")]
        [ValidateUserSession(ActionName = "Tax")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_TaxExpenseCountryCategoryMap(string isOnlyDelete, string TaxDefinationID, string CountryId, string CategoryId, string ApplicableType, string Percentage)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                TaxCountryCategoryMapInfo objBank = new TaxCountryCategoryMapInfo();

                objBank.TaxDefinationID = TaxDefinationID;
                objBank.CountryId = CountryId;
                objBank.CategoryId = CategoryId;
                objBank.ApplicableType = ApplicableType;
                objBank.Percentage = Percentage;

                if (iGstSvc.Save_TaxExpenseCountryCategoryMap(isOnlyDelete.Trim().ToUpper() == "Y", objBank, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        // End: 10/07/2019
    }
}