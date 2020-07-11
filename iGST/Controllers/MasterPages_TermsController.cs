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
    public partial class MasterTermController : Controller
    {
        string ErrorMessage = "";

        [Route("terms")]
        [ValidateUserSession(ActionName = "Terms")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Terms(string TermsID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/TermsList.cshtml", iGstSvc.GetList_Terms(TermsID, OrganizationCode));
            }
        }

        [Route("term")]
        [ValidateUserSession(ActionName = "Terms")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Terms(string TermsID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/TermsDetails.cshtml", iGstSvc.GetDetails_Terms(TermsID, OrganizationCode));
            }
        }

        [Route("termsave")]
        [ValidateUserSession(ActionName = "Terms")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Terms(string isDelete, string isActive, string TermsID, string Name,
                string DueInFixedNumberDays, string DueInCertainDayOfMonth, string DueInNextMonth, string Discount)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                TermsInfo objTermsInfo = new TermsInfo();

                objTermsInfo.Id = TermsID;
                objTermsInfo.Name = Name;
                objTermsInfo.IsActive = isActive == "Y";
                objTermsInfo.OrganizationCode = OrganizationCode;

                objTermsInfo.DueInFixedNumberDays = DueInFixedNumberDays;
                objTermsInfo.DueInFixedNumberDays = DueInFixedNumberDays;
                objTermsInfo.DueInNextMonth = DueInNextMonth;
                objTermsInfo.Discount = Discount;

                if (iGstSvc.Save_Terms(isDelete == "Y", objTermsInfo, (UserInfo)Session["UserDetails"], out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

    }
}