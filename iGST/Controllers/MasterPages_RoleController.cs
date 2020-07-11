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
    public partial class UserRolesController : Controller
    {
        string ErrorMessage = "";

        [Route("roles")]
        [ValidateUserSession(ActionName = "Role")]
        public ActionResult GetList_Role()
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return View("~/Views/MasterPages/RoleList.cshtml", iGstSvc.GetList_Role("", "", "", false));
            }
        }

        [ValidateUserSession(ActionName = "role")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Role(string RoleID)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                return PartialView("~/Views/MasterPages/RoleDetails.cshtml", iGstSvc.GetDetails_Role(RoleID, "", "", false));
            }
        }

        [Route("rolesave")]
        [ValidateUserSession(ActionName = "Role")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Role(string isDelete, string isActive, string isOnlyForModerateUsers, string RoleID, string RoleName,
                string chk_Customer_View, string chk_Customer_Add, string chk_Customer_Edit, string chk_Customer_Delete, string chk_Customer_Audit,
                string chk_Product_View, string chk_Product_Add, string chk_Product_Edit, string chk_Product_Delete, string chk_Product_Audit,
                string chk_Organization_View, string chk_Organization_Add, string chk_Organization_Edit, string chk_Organization_Delete, string chk_Organization_Audit,
                string chk_Branch_View, string chk_Branch_Add, string chk_Branch_Edit, string chk_Branch_Delete, string chk_Branch_Audit,
                string chk_Category_View, string chk_Category_Add, string chk_Category_Edit, string chk_Category_Delete, string chk_Category_Audit,
                string chk_Bill_View, string chk_Bill_Add, string chk_Bill_Edit, string chk_Bill_Delete, string chk_Bill_Audit,
                string chk_Language_View, string chk_Language_Add, string chk_Language_Edit, string chk_Language_Delete, string chk_Language_Audit,
                string chk_DefineLanguage_View, string chk_DefineLanguage_Add, string chk_DefineLanguage_Edit, string chk_DefineLanguage_Delete, string chk_DefineLanguage_Audit,
                string chk_StaticValue_View, string chk_StaticValue_Add, string chk_StaticValue_Edit, string chk_StaticValue_Delete, string chk_StaticValue_Audit,
                string chk_Terms_View, string chk_Terms_Add, string chk_Terms_Edit, string chk_Terms_Delete, string chk_Terms_Audit,
                string chk_UserRegistered_View, string chk_UserRegistered_Add, string chk_UserRegistered_Edit, string chk_UserRegistered_Delete, string chk_UserRegistered_Audit,
                string chk_UserModerate_View, string chk_UserModerate_Add, string chk_UserModerate_Edit, string chk_UserModerate_Delete, string chk_UserModerate_Audit,
                string chk_Role_View, string chk_Role_Add, string chk_Role_Edit, string chk_Role_Delete, string chk_Role_Audit,
                string chk_Function_View, string chk_Function_Add, string chk_Function_Edit, string chk_Function_Delete, string chk_Function_Audit,
                object objUserInfo)
        {
            using (iGst_Svc.GSTServiceClient iGstSvc = new iGst_Svc.GSTServiceClient())
            {
                RoleInfo objRoleInfo = new RoleInfo();

                objRoleInfo.RoleID = RoleID;
                objRoleInfo.RoleName = RoleName;
                objRoleInfo.IsActive = isActive == "Y";
                objRoleInfo.IsOnlyForModerateUsers = isOnlyForModerateUsers == "Y";

                objRoleInfo.Customer_View = chk_Customer_View == "Y";
                objRoleInfo.Customer_Add = chk_Customer_Add == "Y";
                objRoleInfo.Customer_Edit = chk_Customer_Edit == "Y";
                objRoleInfo.Customer_Delete = chk_Customer_Delete == "Y";
                objRoleInfo.Customer_Audit = chk_Customer_Audit == "Y";

                objRoleInfo.Product_View = chk_Product_View == "Y";
                objRoleInfo.Product_Add = chk_Product_Add == "Y";
                objRoleInfo.Product_Edit = chk_Product_Edit == "Y";
                objRoleInfo.Product_Delete = chk_Product_Delete == "Y";
                objRoleInfo.Product_Audit = chk_Product_Audit == "Y";

                objRoleInfo.Organization_View = chk_Organization_View == "Y";
                objRoleInfo.Organization_Add = chk_Organization_Add == "Y";
                objRoleInfo.Organization_Edit = chk_Organization_Edit == "Y";
                objRoleInfo.Organization_Delete = chk_Organization_Delete == "Y";
                objRoleInfo.Organization_Audit = chk_Organization_Audit == "Y";

                objRoleInfo.Branch_View = chk_Branch_View == "Y";
                objRoleInfo.Branch_Add = chk_Branch_Add == "Y";
                objRoleInfo.Branch_Edit = chk_Branch_Edit == "Y";
                objRoleInfo.Branch_Delete = chk_Branch_Delete == "Y";
                objRoleInfo.Branch_Audit = chk_Branch_Audit == "Y";

                objRoleInfo.Role_View = chk_Role_View == "Y";
                objRoleInfo.Role_Add = chk_Role_Add == "Y";
                objRoleInfo.Role_Edit = chk_Role_Edit == "Y";
                objRoleInfo.Role_Delete = chk_Role_Delete == "Y";
                objRoleInfo.Role_Audit = chk_Role_Audit == "Y";

                objRoleInfo.Function_View = chk_Function_View == "Y";
                objRoleInfo.Function_Add = chk_Function_Add == "Y";
                objRoleInfo.Function_Edit = chk_Function_Edit == "Y";
                objRoleInfo.Function_Delete = chk_Function_Delete == "Y";
                objRoleInfo.Function_Audit = chk_Function_Audit == "Y";

                objRoleInfo.Category_View = chk_Category_View == "Y";
                objRoleInfo.Category_Add = chk_Category_Add == "Y";
                objRoleInfo.Category_Edit = chk_Category_Edit == "Y";
                objRoleInfo.Category_Delete = chk_Category_Delete == "Y";
                objRoleInfo.Category_Audit = chk_Category_Audit == "Y";

                objRoleInfo.Bill_View = chk_Bill_View == "Y";
                objRoleInfo.Bill_Add = chk_Bill_Add == "Y";
                objRoleInfo.Bill_Edit = chk_Bill_Edit == "Y";
                objRoleInfo.Bill_Delete = chk_Bill_Delete == "Y";
                objRoleInfo.Bill_Audit = chk_Bill_Audit == "Y";

                objRoleInfo.Language_View = chk_Language_View == "Y";
                objRoleInfo.Language_Add = chk_Language_Add == "Y";
                objRoleInfo.Language_Edit = chk_Language_Edit == "Y";
                objRoleInfo.Language_Delete = chk_Language_Delete == "Y";
                objRoleInfo.Language_Audit = chk_Language_Audit == "Y";

                objRoleInfo.DefineLanguage_View = chk_DefineLanguage_View == "Y";
                objRoleInfo.DefineLanguage_Add = chk_DefineLanguage_Add == "Y";
                objRoleInfo.DefineLanguage_Edit = chk_DefineLanguage_Edit == "Y";
                objRoleInfo.DefineLanguage_Delete = chk_DefineLanguage_Delete == "Y";
                objRoleInfo.DefineLanguage_Audit = chk_DefineLanguage_Audit == "Y";

                objRoleInfo.StaticValue_View = chk_StaticValue_View == "Y";
                objRoleInfo.StaticValue_Add = chk_StaticValue_Add == "Y";
                objRoleInfo.StaticValue_Edit = chk_StaticValue_Edit == "Y";
                objRoleInfo.StaticValue_Delete = chk_StaticValue_Delete == "Y";
                objRoleInfo.StaticValue_Audit = chk_StaticValue_Audit == "Y";

                objRoleInfo.Terms_View = chk_Terms_View == "Y";
                objRoleInfo.Terms_Add = chk_Terms_Add == "Y";
                objRoleInfo.Terms_Edit = chk_Terms_Edit == "Y";
                objRoleInfo.Terms_Delete = chk_Terms_Delete == "Y";
                objRoleInfo.Terms_Audit = chk_Terms_Audit == "Y";

                objRoleInfo.UserRegistered_View = chk_UserRegistered_View == "Y";
                objRoleInfo.UserRegistered_Add = chk_UserRegistered_Add == "Y";
                objRoleInfo.UserRegistered_Edit = chk_UserRegistered_Edit == "Y";
                objRoleInfo.UserRegistered_Delete = chk_UserRegistered_Delete == "Y";
                objRoleInfo.UserRegistered_Audit = chk_UserRegistered_Audit == "Y";

                objRoleInfo.UserModerate_View = chk_UserModerate_View == "Y";
                objRoleInfo.UserModerate_Add = chk_UserModerate_Add == "Y";
                objRoleInfo.UserModerate_Edit = chk_UserModerate_Edit == "Y";
                objRoleInfo.UserModerate_Delete = chk_UserModerate_Delete == "Y";
                objRoleInfo.UserModerate_Audit = chk_UserModerate_Audit == "Y";

                if (iGstSvc.Save_Role(isDelete == "Y", objRoleInfo, (UserInfo)Session["UserDetails"], out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Effective_Role_ForAUser(string BranchId, string UserID)
        {
            return null;
        }
    }
}