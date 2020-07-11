using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects;
using Common;
using System.Data;
using System.Web.Script.Serialization;

namespace iGST.Controllers
{
    public class ValidateUserSessionAttribute : ActionFilterAttribute
    {
        public string ActionName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool flag = false;
            if (HttpContext.Current.Session["UserDetails"] != null &&
                ((UserInfo)HttpContext.Current.Session["UserDetails"]).UserCode != null &&
                ((UserInfo)HttpContext.Current.Session["UserDetails"]).UserCode.Replace(" ", "").Replace("&nbsp;", "").Trim().Length > 0
            )
            {
                //if (ActionName == "Language")
                //{
                //    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                //        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                //        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                //        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                //        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                //    )
                //    {
                //        flag = true;
                //    }
                //}
                //else 
                if (ActionName == "Customer")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Customer_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Customer_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Customer_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Customer_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Customer_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Terms")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Terms_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Terms_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Terms_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Terms_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Terms_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Bank")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Tax")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Bill_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Branch")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Branch_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Branch_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Branch_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Branch_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Branch_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Category")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Category_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Category_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Category_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Category_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Category_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Product")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Product_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Product_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Product_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Product_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Product_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Role")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Role_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Role_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Role_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Role_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Role_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Function")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Function_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Function_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Function_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Function_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Function_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "UserRegistered")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.UserRegistered_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.UserRegistered_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.UserRegistered_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.UserRegistered_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.UserRegistered_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else if (ActionName == "Language")
                {
                    if (((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_View ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Add ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Edit ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Delete ||
                        ((UserInfo)HttpContext.Current.Session["UserDetails"]).EffectiveRole.Language_Audit
                    )
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }


            if (!flag)
            {
                filterContext.Result = new RedirectResult("~/MasterPages/GoToLogin");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

    }


}