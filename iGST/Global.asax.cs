using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace iGST
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RoutesBank(RouteTable.Routes);
            RouteConfig.RoutesBranch(RouteTable.Routes);
            RouteConfig.RoutesCategory(RouteTable.Routes);
            RouteConfig.RoutesCountryState(RouteTable.Routes);
            RouteConfig.RoutesCustomer(RouteTable.Routes);
            RouteConfig.RoutesLanguage(RouteTable.Routes);
            RouteConfig.RoutesExpense(RouteTable.Routes);
            RouteConfig.RoutesFunction(RouteTable.Routes);
            RouteConfig.RoutesInvoice(RouteTable.Routes);
            RouteConfig.RoutesLogin(RouteTable.Routes);
            RouteConfig.RoutesOrganization(RouteTable.Routes);
            RouteConfig.RoutesProducts(RouteTable.Routes);
            RouteConfig.RoutesReport(RouteTable.Routes);
            RouteConfig.RoutesRoles(RouteTable.Routes);
            RouteConfig.RoutesServices(RouteTable.Routes);
            RouteConfig.RoutesSettigs(RouteTable.Routes);
            RouteConfig.RoutesStaticValue(RouteTable.Routes);
            RouteConfig.RoutesSupplier(RouteTable.Routes);
            RouteConfig.RoutesTax(RouteTable.Routes);
            RouteConfig.RoutesTerm(RouteTable.Routes);
            RouteConfig.RoutesUpload(RouteTable.Routes);
            RouteConfig.RoutesUsers(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //RouteConfig.RoutesConstruction(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
