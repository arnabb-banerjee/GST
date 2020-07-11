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
    public partial class StaticValueController : Controller
    {
        string ErrorMessage = "";

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_StaticValue(string Key)
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["StaticValue"] == null)
                {
                    List<StaticValuInfo> list = new Master_Svc.MasterServiceClient().GetList_StaticValue("");
                    System.Web.HttpContext.Current.Application["StaticValue"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(((List<StaticValuInfo>)System.Web.HttpContext.Current.Application["StaticValue"]), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }

            return Json(new List<StaticValuInfo>(), JsonRequestBehavior.AllowGet);
        }
    }
}