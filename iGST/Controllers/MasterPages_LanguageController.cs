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
    public partial class LanguageController : Controller
    {
        string ErrorMessage = "";

        [Route("languagelist")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_LanguageDropdownList()
        {
            if (System.Web.HttpContext.Current.Application["LanguageList"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    List<LanguageInfo> list = iGstSvc.GetList_Language("", "", true);
                    System.Web.HttpContext.Current.Application["LanguageList"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json((List<LanguageInfo>)System.Web.HttpContext.Current.Application["LanguageList"], JsonRequestBehavior.AllowGet);
            }

        }

        [Route("languagecountrydropdownlist")]
        //[ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_LanguageCountryDropdownList()
        {
            if (System.Web.HttpContext.Current.Session["LanguageCountrtyList"] == null)
            {
                string CountryID = "0";
                if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserID != null && ((UserInfo)Session["UserDetails"]).Country != null && ((UserInfo)Session["UserDetails"]).Country.Trim().Length > 0)
                {
                    CountryID = ((UserInfo)Session["UserDetails"]).Country;
                }

                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    List<LanguageCountryInfo> list = iGstSvc.GetList_LanguageCountry("", "", CountryID, "", true);

                    if (list == null)
                    {
                        list = new List<LanguageCountryInfo>();
                    }

                    LanguageCountryInfo obLanguage = new LanguageCountryInfo();
                    obLanguage.LanguageId = "0";
                    obLanguage.LanguageName = "English";
                    list.Insert(0, obLanguage);

                    obLanguage = null;

                    System.Web.HttpContext.Current.Session["LanguageCountrtyList"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json((List<LanguageCountryInfo>)System.Web.HttpContext.Current.Session["LanguageCountrtyList"], JsonRequestBehavior.AllowGet);
            }

        }

        [Route("languages")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Language()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/LanguageList.cshtml", iGstSvc.GetList_Language("", "", true));
            }
        }

        [Route("language")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Language(string LanguageID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/LanguageDetails.cshtml", iGstSvc.GetDetails_Language(LanguageID, "", true));
            }
        }

        [Route("languagesave")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Language(string isOnlyDelete, string LanguageId, string LanguageName, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                LanguageInfo objLanguage = new LanguageInfo();
                objLanguage.LanguageId = LanguageId;
                objLanguage.LanguageName = LanguageName;
                objLanguage.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_Language(isOnlyDelete.Trim().ToUpper() == "Y", objLanguage, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("multiplelanguages")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_DataValueLanguageWise(string LanguageIds, string MasterTablePrefixs)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/LanguageDataValueList.cshtml", iGstSvc.GetList_Language("", "", true));
            }
        }

        [Route("multiplelanguage")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_DataValueLanguageWise(string LanguageIds, string MasterTablePrefixs)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/LanguageDataValueDetails.cshtml", iGstSvc.GetList_DataValueLanguageWise(MasterTablePrefixs, LanguageIds, true));
            }
        }

        [Route("multiplelanguagesave")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_DataValueLanguageWise(string isOnlyDelete, string LanguageId, string value, string MasterTablePrefix, string MasterIDField, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                LanguageValueInfo objLanguage = new LanguageValueInfo();
                objLanguage.LanguageId = LanguageId;
                objLanguage.value = value;
                objLanguage.MasterTablePrefix = MasterTablePrefix;
                objLanguage.MasterIDField = MasterIDField;
                objLanguage.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_DataValueLanguageWise(isOnlyDelete.Trim().ToUpper() == "Y", objLanguage, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }

                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("LanguageChange")]
        public ActionResult LanguageChange(string selectedLanguage)
        {
            Session["Language"] = selectedLanguage;
            System.Web.HttpContext.Current.Application["Category"] = null;
            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        [Route("languagecountrylist")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_LanguageCountry()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/LanguageCountryList.cshtml", iGstSvc.GetList_LanguageCountry("", "", "", "", true));
            }
        }

        [Route("languagecountrydetails")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_LanguageCountry(string ID, string LanguageID, string CountryID, string Visibility, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/LanguageCountryDetails.cshtml", iGstSvc.GetDetails_LanguageCountry(ID, LanguageID, CountryID, Visibility, IsActive.Trim() == "Y"));
            }
        }

        [Route("languagecountrysave")]
        [ValidateUserSession(ActionName = "Language")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_LanguageCountry(string isOnlyDelete, string Id, string LanguageId, string CountryId, string Visibility, string Priority, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                LanguageCountryInfo objLanguage = new LanguageCountryInfo();
                objLanguage.Id = Id == "" ? 0 : Convert.ToInt32(Id);
                objLanguage.LanguageId = LanguageId;
                objLanguage.CountryId = CountryId;
                objLanguage.Visibility = Visibility.Trim().ToUpper() == "Y";
                objLanguage.Proirity = Priority == "" ? 0 : Convert.ToInt32(Priority);

                if (iGstSvc.Save_LanguageCountry(isOnlyDelete.Trim().ToUpper() == "Y", objLanguage, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }

                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
    }
}