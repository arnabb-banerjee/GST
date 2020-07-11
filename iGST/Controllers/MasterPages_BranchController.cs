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
    public partial class MasterBranchController : Controller
    {
        string ErrorMessage = "";

        [Route("branches")]
        [ValidateUserSession(ActionName = "Branch")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Branch()
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/BranchList.cshtml", iGstSvc.GetList_Branch("", OrganizationCode, "", true));
            }
        }

        [Route("branch")]
        [ValidateUserSession(ActionName = "Branch")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Branch(string BranchID)
        {
            string OrganizationCode = "";
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/BranchDetails.cshtml", iGstSvc.GetDetails_Branch(BranchID, OrganizationCode, "", true));
            }
        }

        [Route("branchsave")]
        [ValidateUserSession(ActionName = "Branch")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Branch(string isOnlyDelete, string OrganizationCode, string BranchId, string BranchName, string Street1, string Street2, string City, string State, string Country, string PIN, string IsMainBranch, string IsActive)
        {
            if (Session["UserDetails"] != null && ((UserInfo)Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)Session["UserDetails"]).OrganizationCode != null)
            {
                OrganizationCode = ((UserInfo)Session["UserDetails"]).OrganizationCode.Trim();
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                BranchInfo objBranch = new BranchInfo();

                objBranch.BranchID = BranchId;
                objBranch.BranchName = BranchName;
                objBranch.Street1 = Street1;
                objBranch.Street2 = Street2;
                objBranch.City = City;
                objBranch.State = State;
                objBranch.Country = Country;
                objBranch.PIN = PIN;
                objBranch.IsMainBranch = IsMainBranch;
                objBranch.OrganizationCode = OrganizationCode;
                objBranch.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_Branch(isOnlyDelete.Trim().ToUpper() == "Y", objBranch, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }
    }
}