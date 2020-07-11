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
    public partial class MasterCategoryController : Controller
    {
        string ErrorMessage = "";

        [Route("categorydropdownlist")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_categorydropdownlist(string IsExpenseType, string CategoryID, string option, string CountryID)
        {
            if (System.Web.HttpContext.Current.Application["ExpenseCategory"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = 0;
                    }

                    int d = 0;
                    int.TryParse(CountryID, out d);

                    List<CategoryInfo> list = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown(IsExpenseType, d, CategoryID, option, Session["Language"].ToString());

                    System.Web.HttpContext.Current.Application["ExpenseCategory"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(((List<CategoryInfo>)System.Web.HttpContext.Current.Application["ExpenseCategory"]), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("categoriesdropdown")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_CategoryForDropdown(string CategoryID, string option, string CountryID)
        {
            if (System.Web.HttpContext.Current.Application["Category"] == null)
            {
                using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
                {
                    if (Session["Language"] == null)
                    {
                        Session["Language"] = 0;
                    }

                    int d = 0;
                    int.TryParse(CountryID, out d);

                    List<CategoryInfo> list = iGstSvc.GetList_CategoryForDropdown("", d, CategoryID, option, Session["Language"].ToString());

                    System.Web.HttpContext.Current.Application["Category"] = list;
                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(((List<CategoryInfo>)System.Web.HttpContext.Current.Application["Category"]), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("categoriesdropdownstr")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_CategoryForDropdownCountrywise(string CategoryID, string option, string CountryID)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                if (Session["Language"] == null)
                {
                    Session["Language"] = 0;
                }

                int d = 0;
                int.TryParse(CountryID, out d);

                return Json(CommonMethods.ListCategory("", CountryID.ToString(), true), JsonRequestBehavior.AllowGet);
            }
        }

        [Route("categories")]
        [ValidateUserSession(ActionName = "Category")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetList_Category()
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 0;
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                return View("~/Views/MasterPages/CategoryList.cshtml", iGstSvc.GetList_Category("", "", "", "", true, "", Session["Language"].ToString()));
            }
        }

        [Route("category")]
        [ValidateUserSession(ActionName = "Category")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDetails_Category(string CategoryID)
        {
            if (Session["Language"] == null)
            {
                Session["Language"] = 1;
            }

            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                CategoryInfo obj = iGstSvc.GetDetails_Category(CategoryID, "", "", true, Session["Language"].ToString());
                ViewBag.Countries = CommonMethods.ListCountry(obj.CountryId);
                return PartialView("~/Views/MasterPages/CategoryDetails.cshtml", obj);
            }
        }

        [Route("categorysave")]
        [ValidateUserSession(ActionName = "Category")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save_Category(string isOnlyDelete, string CountryId, string CategoryId, string ParentCategoryId, string WillCarryContent, string CategoryName, string ServiceOrGoods,
                string HsnSacCode, string IsActive, string IsExpenseType)
        {
            using (Master_Svc.MasterServiceClient iGstSvc = new Master_Svc.MasterServiceClient())
            {
                CategoryInfo objCategory = new CategoryInfo();
                objCategory.CategoryId = CategoryId;
                objCategory.CountryId = CountryId;
                objCategory.ParentCategoryId = ParentCategoryId;
                objCategory.CategoryName = CategoryName;
                objCategory.ServiceOrGoods = ServiceOrGoods;
                objCategory.WillCarryContent = WillCarryContent;
                objCategory.HSNCode = ServiceOrGoods.Trim().ToUpper() == "G" ? HsnSacCode : "";
                objCategory.SACCode = ServiceOrGoods.Trim().ToUpper() == "S" ? HsnSacCode : "";
                objCategory.IsActive = IsActive.Trim().ToUpper() == "Y";
                objCategory.IsExpenseType = IsExpenseType.Trim().ToUpper() == "Y";

                if (iGstSvc.Save_Category(isOnlyDelete.Trim().ToUpper() == "Y", objCategory, ((UserInfo)Session["UserDetails"]).UserCode, out ErrorMessage))
                {
                    System.Web.HttpContext.Current.Application["Category"] = null;
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(ErrorMessage, JsonRequestBehavior.AllowGet);
        }
    }
}