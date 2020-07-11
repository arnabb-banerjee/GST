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
    public partial class MasterCountryStateController : MasterPagesController
    {
        string ErrorMessage = "";
        Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient();

        [Route("countrylistdropdown")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Country()
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["Country"] == null)
                {
                    List<CountryInfo> countries = iGstSvc.GetList_Country();
                    System.Web.HttpContext.Current.Application["Country"] = countries;
                    return Json(countries, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json((List<CountryInfo>)System.Web.HttpContext.Current.Application["Country"], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Country ist", "");
            }

            return Json(new List<CountryInfo>(), JsonRequestBehavior.AllowGet);

            //((List<CountryInfo>)System.Web.HttpContext.Current.Application["Country"])
        }

        [Route("statelistdropdown")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetStatesByCountryId(string countryId)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return Json(iGstSvc.GetList_State(countryId), JsonRequestBehavior.AllowGet);
            }
        }
    }
}