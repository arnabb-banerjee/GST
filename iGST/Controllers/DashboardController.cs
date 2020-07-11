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
    public partial class DashboardController : Controller
    {
        string ErrorMessage = "";

        [Route("dashboardorg")]
        public ActionResult GetDashboard_Organization(string FromDate, string ToDate)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
                {
                    return View("~/Views/MasterPages/Index.cshtml", new Auth_Svc.UserAuthenticationServiceClient().GetDashboard_Organization(((UserInfo)Session["UserDetails"]).OrganizationCode, FromDate, ToDate, ""));
                }
            }
            else
            {
                return View("~/Views/MasterPages/Index.cshtml");
            }
        }
    }
}