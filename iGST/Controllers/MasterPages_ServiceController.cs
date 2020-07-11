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
    public partial class MasterServicesController : Controller
    {
        string ErrorMessage = "";

        #region ServiceClass Related
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceClassForDropdown()
        {
            if (System.Web.HttpContext.Current.Application["ServiceClass"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = -1;
                    }

                    return Json(iGstSvc.GetList_ServiceClass("", "", true), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(((List<ServiceClassInfo>)System.Web.HttpContext.Current.Application["ServiceClass"]), JsonRequestBehavior.AllowGet);
        }

        [Route("serviceclasses")]
        [ValidateUserSession(ActionName = "ServiceClass")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceClass()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/ServiceClassList.cshtml", iGstSvc.GetList_ServiceClass("", "", true));
            }
        }

        [Route("serviceclass")]
        [ValidateUserSession(ActionName = "ServiceClass")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ServiceClass(string ServiceClassID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/ServiceClassDetails.cshtml", iGstSvc.GetDetails_ServiceClass(ServiceClassID, "", true));
            }
        }

        [Route("serviceclasssave")]
        [ValidateUserSession(ActionName = "ServiceClass")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ServiceClass(string isOnlyDelete, string ServiceClassId, string ServiceClassName, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                ServiceClassInfo objServiceClass = new ServiceClassInfo();
                objServiceClass.ServiceClassId = ServiceClassId;
                objServiceClass.ServiceClassName = ServiceClassName;
                objServiceClass.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_ServiceClass(isOnlyDelete.Trim().ToUpper() == "Y", objServiceClass, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ServiceType Related
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceTypeForDropdown()
        {
            if (System.Web.HttpContext.Current.Application["ServiceType"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = -1;
                    }

                    return Json(iGstSvc.GetList_ServiceType("", "", true), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(((List<ServiceTypeInfo>)System.Web.HttpContext.Current.Application["ServiceType"]), JsonRequestBehavior.AllowGet);
        }

        [Route("servicetypes")]
        [ValidateUserSession(ActionName = "ServiceType")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceType()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/ServiceTypeList.cshtml", iGstSvc.GetList_ServiceType("", "", true));
            }
        }

        [Route("servicetype")]
        [ValidateUserSession(ActionName = "ServiceType")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ServiceType(string ServiceTypeID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/ServiceTypeDetails.cshtml", iGstSvc.GetDetails_ServiceType(ServiceTypeID, "", true));
            }
        }

        [Route("servicetypesave")]
        [ValidateUserSession(ActionName = "ServiceType")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ServiceType(string isOnlyDelete, string ServiceTypeId, string ServiceTypeName, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                ServiceTypeInfo objServiceType = new ServiceTypeInfo();
                objServiceType.ServiceTypeId = ServiceTypeId;
                objServiceType.ServiceTypeName = ServiceTypeName;
                objServiceType.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_ServiceType(isOnlyDelete.Trim().ToUpper() == "Y", objServiceType, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ServiceUnit Related
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceUnitForDropdown()
        {
            if (System.Web.HttpContext.Current.Application["ServiceUnit"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = -1;
                    }

                    return Json(iGstSvc.GetList_ServiceUnit("", "", true), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(((List<ServiceUnitInfo>)System.Web.HttpContext.Current.Application["ServiceUnit"]), JsonRequestBehavior.AllowGet);
        }


        [Route("serviceunits")]
        [ValidateUserSession(ActionName = "ServiceUnit")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_ServiceUnit()
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/ServiceUnitList.cshtml", iGstSvc.GetList_ServiceUnit("", "", true));
            }
        }

        [Route("serviceunit")]
        [ValidateUserSession(ActionName = "ServiceUnit")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_ServiceUnit(string ServiceUnitID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return PartialView("~/Views/MasterPages/ServiceUnitDetails.cshtml", iGstSvc.GetDetails_ServiceUnit(ServiceUnitID, "", true));
            }
        }

        [Route("serviceunitsave")]
        [ValidateUserSession(ActionName = "ServiceUnit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_ServiceUnit(string isOnlyDelete, string ServiceUnitId, string ServiceUnitName, string IsActive)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                ServiceUnitInfo objServiceUnit = new ServiceUnitInfo();
                objServiceUnit.ServiceUnitId = ServiceUnitId;
                objServiceUnit.ServiceUnitName = ServiceUnitName;
                objServiceUnit.IsActive = IsActive.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_ServiceUnit(isOnlyDelete.Trim().ToUpper() == "Y", objServiceUnit, ((UserInfo)Session["UserDetails"]), out ErrorMessage))
                {
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}