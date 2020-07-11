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
    public partial class OrganizationController : Controller
    {
        string ErrorMessage = "";

        #region Organization
        [Route("organizationlist")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_OrganizationDropdownList()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return Json(iGstSvc.GetList_OrganizationDropdownList(OrganizationCode, "", ""), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Organization Accountant
        [Route("orgaccountants")]
        public ActionResult GetList_OrganizationAccountant()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/OrganizationAccountanList.cshtml", new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationAccountant("", OrganizationCode, "", false));
            }
        }

        [Route("orgaccountant")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_OrganizationAccountant()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                return PartialView("~/Views/MasterPages/OrganizationAccountanDetails.cshtml", iGstSvc.GetDetails_OrganizationAccountant("", OrganizationCode, "", false));
            }
        }

        [Route("orgaccountantsave")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_OrganizationAccountant(string isOnlyDelete, string ID, string AccountantCode, string OrganizationCode)
        {
            using (Auth_Svc.UserAuthenticationServiceClient iGstSvc = new Auth_Svc.UserAuthenticationServiceClient())
            {
                if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
                {
                    OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
                }

                OrganizationAccountantInfo obj = new OrganizationAccountantInfo();
                obj.OrganizationCode = OrganizationCode;
                obj.AccountantCode = AccountantCode;
                obj.ID = ID;

                if (iGstSvc.Save_OrganizationAccountant(isOnlyDelete.Trim().ToUpper() == "Y", obj, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}