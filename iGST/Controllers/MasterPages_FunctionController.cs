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
    public partial class MasterFunctionsController : Controller
    {
        string ErrorMessage = "";

        [Route("functions")]
        [ValidateUserSession(ActionName = "Function")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Function()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/FunctionList.cshtml", iGstSvc.GetList_Function("", "", true));
            }
        }

        [Route("function")]
        [ValidateUserSession(ActionName = "Function")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Function(string FunctionID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/FunctionDetails.cshtml", iGstSvc.GetDetails_Function(FunctionID, "", true));
            }
        }

        [Route("functionsave")]
        [ValidateUserSession(ActionName = "Function")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Function(string isOnlyDelete, string FunctionId, string FunctionName, string IsForModerate, string IsForMembership, string IsDesignation, string IsActive, string IsDefaultForModerateUser, string IsDefaultForRegisteredUser, string Roles)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                FunctionInfo objFunction = new FunctionInfo();

                objFunction.FunctionId = FunctionId;
                objFunction.FunctionName = FunctionName;
                objFunction.IsForModerate = IsForModerate;
                objFunction.IsForMembership = IsForMembership;
                objFunction.IsDesignation = IsDesignation;
                objFunction.IsDefaultForModerateUser = IsDefaultForModerateUser.Trim().ToUpper() == "Y";
                objFunction.IsDefaultForRegisteredUser = IsDefaultForRegisteredUser.Trim().ToUpper() == "Y";
                objFunction.IsActive = IsActive.Trim().ToUpper() == "Y";
                objFunction.Roles = Roles;

                if (iGstSvc.Save_Function(isOnlyDelete.Trim().ToUpper() == "Y", objFunction, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }

                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
    }
}