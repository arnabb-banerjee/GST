using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace iGST
{
    public partial class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("home", "home/", new { controller = "Dashboard", action = "GetDashboard_Organization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("index", "index/", new { controller = "Dashboard", action = "GetDashboard_Organization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("construction", "construction/", new { controller = "MasterPages", action = "ConstructionPage" }, new[] { "iGST.Controllers" });
            routes.MapRoute("Error", "error/", new { controller = "MasterPages", action = "Error" }, new[] { "iGST.Controllers" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "MasterPages", action = "Index", id = UrlParameter.Optional }
            );
        }

        //public static void RoutesConstruction(RouteCollection routes)
        //{
        //    routes.MapRoute("construction", "construction/", new { controller = "MasterPages", action = "ConstructionPage" }, new[] { "iGST.Controllers" });
        //}

        public static void RoutesBank(RouteCollection routes)
        {
            routes.MapRoute("banks", "banks/{OrganizationCode}", new { controller = "BankMaster", action = "GetList_Bank", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("bank", "bank/", new { controller = "BankMaster", action = "GetDetails_Bank", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("banksave", "banksave/", new { controller = "BankMaster", action = "Save_Bank", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("banktransactions", "banktransactions/{OrganizationCode}", new { controller = "BankMaster", action = "GetList_BankTransaction", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("bankbanktransactiondetails", "bankbanktransactiondetails/", new { controller = "BankMaster", action = "GetDetails_BankTransaction" }, new[] { "iGST.Controllers" });
            routes.MapRoute("banktransactionsave", "banktransactionsave/", new { controller = "BankMaster", action = "Save_BankTransaction" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesBranch(RouteCollection routes)
        {
            routes.MapRoute("branches", "branches/{OrganizationCode}", new { controller = "MasterBranch", action = "GetList_Branch", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("branch", "branch/", new { controller = "MasterBranch", action = "GetDetails_Branch", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("branchsave", "branchsave/", new { controller = "MasterBranch", action = "Save_Branch", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
        }
        public static void RoutesInvoice(RouteCollection routes)
        {
            routes.MapRoute("invoices", "invoices/{OrganizationCode}", new { controller = "Invoice", action = "GetList_Bill", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("invoice", "invoice/{org}", new { controller = "Invoice", action = "GetDetails_Bill" }, new[] { "iGST.Controllers" });
            routes.MapRoute("saveinvoice", "saveinvoice/", new { controller = "Invoice", action = "Save_Bill" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesCustomer(RouteCollection routes)
        {
            routes.MapRoute("customersave", "customersave/", new { controller = "Customer", action = "Save_Customer" }, new[] { "iGST.Controllers" });
            routes.MapRoute("customers", "customers/{OrganizationCode}", new { controller = "Customer", action = "GetList_Customer", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("customer/", "customer/{org}", new { controller = "Customer", action = "GetDetails_Customer" }, new[] { "iGST.Controllers" });
            routes.MapRoute("customerddlist", "customerddlist/", new { controller = "Customer", action = "GetList_CustomerForDropdownlist" }, new[] { "iGST.Controllers" });
            routes.MapRoute("uploadcustomerimage", "uploadcustomerimage/", new { controller = "Customer", action = "Save_CustomerImageList" }, new[] { "iGST.Controllers" });
            routes.MapRoute("getimageforcustomer", "getimageforcustomer/", new { controller = "Customer", action = "GetList_CustomerImage" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesCountryState(RouteCollection routes)
        {
            routes.MapRoute("countrylistdropdown", "countrylistdropdown/", new { controller = "MasterCountryState", action = "GetList_Country" }, new[] { "iGST.Controllers" });
            routes.MapRoute("statelistdropdown", "statelistdropdown/", new { controller = "MasterCountryState", action = "GetStatesByCountryId" }, new[] { "iGST.Controllers" });
            routes.MapRoute("GetList_Country", "MasterPages/GetList_Country/", new { controller = "MasterCountryState", action = "GetList_Country" }, new[] { "iGST.Controllers" });
            routes.MapRoute("GetStatesByCountryId", "MasterPages/GetStatesByCountryId/", new { controller = "MasterCountryState", action = "GetStatesByCountryId" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesExpense(RouteCollection routes)
        {
            routes.MapRoute("expenses", "expenses/{OrganizationCode}", new { controller = "Expense", action = "GetList_Expense", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            //routes.MapRoute("expense", "expense/{OrganizationCode}", new { controller = "Expense", action = "GetDetails_Expense", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            //routes.MapRoute("saveexpense", "saveexpense/{OrganizationCode}", new { controller = "Expense", action = "Save_Expense" }, new[] { "iGST.Controllers" });
            routes.MapRoute("expense", "expense/{OrganizationCode}", new { controller = "Expense", action = "GetDetails_Expense", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("saveexpense", "saveexpense/", new { controller = "Expense", action = "Save_Expense" }, new[] { "iGST.Controllers" });

            // Added on [27th August 2019] by [Partha] to Display Expense menu item, Add/Modification/Delete of menu item.
            //routes.MapRoute("myexpense", "myexpense", new { controller = "Expense", action = "GetList_MyExpense", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            // END : Modified on [3rd August 2019]
        }
        public static void RoutesCategory(RouteCollection routes)
        {
            routes.MapRoute("getgstcategory", "getgstcategory/", new { controller = "Products", action = "Get_GstCategory" }, new[] { "iGST.Controllers" });
            routes.MapRoute("categorydropdownlist", "categorydropdownlist/", new { controller = "MasterCategory", action = "GetList_categorydropdownlist" }, new[] { "iGST.Controllers" });
            routes.MapRoute("categoriesdropdown", "categoriesdropdown/", new { controller = "MasterCategory", action = "GetList_CategoryForDropdown" }, new[] { "iGST.Controllers" });
            routes.MapRoute("categoriesdropdownstr", "categoriesdropdownstr/", new { controller = "MasterCategory", action = "GetList_CategoryForDropdownCountrywise" }, new[] { "iGST.Controllers" });
            routes.MapRoute("categories", "categories/", new { controller = "MasterCategory", action = "GetList_Category" }, new[] { "iGST.Controllers" });
            routes.MapRoute("category", "category/", new { controller = "MasterCategory", action = "GetDetails_Category" }, new[] { "iGST.Controllers" });
            routes.MapRoute("categorysave", "categorysave/", new { controller = "MasterCategory", action = "Save_Category" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesFunction(RouteCollection routes)
        {
            routes.MapRoute("functions", "functions/", new { controller = "MasterFunctions", action = "GetList_Function" }, new[] { "iGST.Controllers" });
            routes.MapRoute("function", "function/", new { controller = "MasterFunctions", action = "GetDetails_Function" }, new[] { "iGST.Controllers" });
            routes.MapRoute("functionsave", "functionsave/", new { controller = "MasterFunctions", action = "Save_Function" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesUsers(RouteCollection routes)
        {
            #region Business Users
            routes.MapRoute("businessusers", "businessusers/{OrganizationCode}", new { controller = "Users", action = "GetList_UserRegistered", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("businessuser", "businessuser/", new { controller = "Users", action = "GetDetails_UserRegistered" }, new[] { "iGST.Controllers" });
            routes.MapRoute("users", "users/", new { controller = "Users", action = "GetList_UserModerate" }, new[] { "iGST.Controllers" });
            #endregion

            #region Accountants
            routes.MapRoute("accountants", "accountants/{OrganizationCode}", new { controller = "Users", action = "GetList_Accountant", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("accountant", "accountant/", new { controller = "Users", action = "GetDetails_Accountant" }, new[] { "iGST.Controllers" });
            routes.MapRoute("orgaccountantsave", "orgaccountantsave/", new { controller = "Users", action = "Save_OrganizationAccountant" }, new[] { "iGST.Controllers" });
            #endregion

            #region Moderate
            routes.MapRoute("moderate", "moderate/", new { controller = "Users", action = "GetDetails_UserModerate" }, new[] { "iGST.Controllers" });
            routes.MapRoute("moderatesave", "moderatesave/", new { controller = "Users", action = "Save_UserModerate" }, new[] { "iGST.Controllers" });
            #endregion

            #region Employees
            routes.MapRoute("employees", "employees/{OrganizationCode}", new { controller = "Users", action = "GetList_UserEmpolyee", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("employee", "employee/", new { controller = "Users", action = "GetDetails_UserEmployee" }, new[] { "iGST.Controllers" }); 
            #endregion
        }

        public static void RoutesReport(RouteCollection routes)
        {
            routes.MapRoute("reports", "reports/{t}", new { controller = "Report", action = "Get_ReportData", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
        }

        public static void RoutesRoles(RouteCollection routes)
        {
            routes.MapRoute("roles", "roles/", new { controller = "UserRoles", action = "GetList_Role" }, new[] { "iGST.Controllers" });
            routes.MapRoute("role", "role/", new { controller = "UserRoles", action = "GetDetails_Role" }, new[] { "iGST.Controllers" });
            routes.MapRoute("rolesave", "rolesave/", new { controller = "UserRoles", action = "Save_Role" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesSupplier(RouteCollection routes)
        {
            routes.MapRoute("suppliers", "suppliers/{OrganizationCode}", new { controller = "Supplier", action = "GetList_Supplier", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("supplier", "supplier/{org}", new { controller = "Supplier", action = "GetDetails_Supplier" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesTax(RouteCollection routes)
        {
            routes.MapRoute("taxmasters", "taxmasters/", new { controller = "Tax", action = "GetList_TaxMaster" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxmaster", "taxmaster/", new { controller = "Tax", action = "GetDetails_TaxMaster" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxmastersave", "taxmastersave/", new { controller = "Tax", action = "Save_TaxMaster" }, new[] { "iGST.Controllers" });

            routes.MapRoute("taxcountries", "taxcountries/", new { controller = "Tax", action = "GetList_TaxCountryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxcountry", "taxcountry/", new { controller = "Tax", action = "GetDetails_TaxCountryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxcountrysave", "taxcountrysave/", new { controller = "Tax", action = "Save_TaxCountryMap" }, new[] { "iGST.Controllers" });

            routes.MapRoute("taxcountrycategories", "taxcountrycategories/", new { controller = "Tax", action = "GetList_TaxCountryCategoryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxcountrycategory", "taxcountrycategory/", new { controller = "Tax", action = "GetDetails_TaxCountryCategoryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxcountrycategorysave", "taxcountrycategorysave/", new { controller = "Tax", action = "Save_TaxCountryCategoryMap" }, new[] { "iGST.Controllers" });

            // Added by Partha on 10/07/2019
            routes.MapRoute("taxexpensecountrycategories", "taxexpensecountrycategories/", new { controller = "Tax", action = "GetList_TaxExpenseCountryCategoryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxexpensecountrycategory", "taxexpensecountrycategory/", new { controller = "Tax", action = "GetDetails_TaxExpenseCountryCategoryMap" }, new[] { "iGST.Controllers" });
            routes.MapRoute("taxexpensecountrycategorysave", "taxexpensecountrycategorysave/", new { controller = "Tax", action = "Save_TaxExpenseCountryCategoryMap" }, new[] { "iGST.Controllers" });

            // End: 10/07/2019
        }
        public static void RoutesTerm(RouteCollection routes)
        {
            routes.MapRoute("terms", "terms/{OrganizationCode}", new { controller = "MasterTerm", action = "GetList_Terms", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("term", "term/", new { controller = "MasterTerm", action = "GetDetails_Terms", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("termsave", "termsave/", new { controller = "MasterTerm", action = "Save_Terms", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
        }
        public static void RoutesUpload(RouteCollection routes)
        {
            routes.MapRoute("upload", "upload/", new { controller = "Upload", action = "ExcelUpload" }, new[] { "iGST.Controllers" });
            routes.MapRoute("UploadFile", "UploadFile/", new { controller = "Upload", action = "UploadFile" }, new[] { "iGST.Controllers" });
            routes.MapRoute("openimageupload", "openimageupload/", new { controller = "Upload", action = "UploadImageOpen" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesLogin(RouteCollection routes)
        {
            routes.MapRoute("login", "login/", new { controller = "NormalLogin", action = "GoToLogin" }, new[] { "iGST.Controllers" });
            routes.MapRoute("myaccount", "myaccount/", new { controller = "NormalLogin", action = "GoToDashboard" }, new[] { "iGST.Controllers" });
            routes.MapRoute("processlogin", "processlogin/", new { controller = "NormalLogin", action = "User_Login" }, new[] { "iGST.Controllers" });
            routes.MapRoute("logout", "logout/", new { controller = "NormalLogin", action = "LogOff" }, new[] { "iGST.Controllers" });
            routes.MapRoute("facebooklogin", "facebooklogin/", new { controller = "NormalLogin", action = "facebooklogin" }, new[] { "iGST.Controllers" });
            routes.MapRoute("forgotpassword", "forgotpassword/", new { controller = "NormalLogin", action = "User_ForgotPassword" }, new[] { "iGST.Controllers" });
            routes.MapRoute("changepassword", "changepassword/", new { controller = "NormalLogin", action = "User_ChangePassword" }, new[] { "iGST.Controllers" });
            routes.MapRoute("passwordsave", "passwordsave/", new { controller = "NormalLogin", action = "User_PasswordSave" }, new[] { "iGST.Controllers" });
            routes.MapRoute("registration", "registration/", new { controller = "Users", action = "User_Registration" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesStaticValue(RouteCollection routes)
        {
            routes.MapRoute("StaticValueList", "MasterPages/GetList_StaticValue/", new { controller = "StaticValue", action = "GetList_StaticValue" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesServices(RouteCollection routes)
        {
            routes.MapRoute("serviceunits", "serviceunits/", new { controller = "MasterServices", action = "GetList_ServiceUnit" }, new[] { "iGST.Controllers" });
            routes.MapRoute("serviceunit", "serviceunit/{org}", new { controller = "MasterServices", action = "GetDetails_ServiceUnit" }, new[] { "iGST.Controllers" });
            routes.MapRoute("serviceunitsave", "serviceunitsave/", new { controller = "MasterServices", action = "Save_ServiceUnit" }, new[] { "iGST.Controllers" });
            routes.MapRoute("servicetypes", "servicetypes/", new { controller = "MasterServices", action = "GetList_ServiceType" }, new[] { "iGST.Controllers" });
            routes.MapRoute("servicetype", "servicetype/{org}", new { controller = "MasterServices", action = "GetDetails_ServiceType" }, new[] { "iGST.Controllers" });
            routes.MapRoute("servicetypesave", "servicetypesave/", new { controller = "MasterServices", action = "Save_ServiceType" }, new[] { "iGST.Controllers" });
            routes.MapRoute("serviceclasses", "serviceclasses/", new { controller = "MasterServices", action = "GetList_ServiceClass" }, new[] { "iGST.Controllers" });
            routes.MapRoute("serviceclass", "serviceclass/{org}", new { controller = "MasterServices", action = "GetDetails_ServiceClass" }, new[] { "iGST.Controllers" });
            routes.MapRoute("serviceclasssave", "serviceclasssave/", new { controller = "MasterServices", action = "Save_ServiceClass" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesSettigs(RouteCollection routes)
        {
            routes.MapRoute("settingcompany", "settingcompany/", new { controller = "Settigs", action = "GetDetails_SettingsCompany" }, new[] { "iGST.Controllers" });
            routes.MapRoute("settingpayment", "settingpayment/", new { controller = "Settigs", action = "GetDetails_SettingsPayment" }, new[] { "iGST.Controllers" });
            routes.MapRoute("settingcurrency", "settingcurrency/", new { controller = "Settigs", action = "GetDetails_SettingsCurrency" }, new[] { "iGST.Controllers" });
            routes.MapRoute("settingalertnotification", "settingalertnotification/", new { controller = "Settigs", action = "GetDetails_SettingsAlertNotification" }, new[] { "iGST.Controllers" });
            routes.MapRoute("settingssave", "settingssave/", new { controller = "Settigs", action = "Save_Settings" }, new[] { "iGST.Controllers" });
            routes.MapRoute("applicablecurrencies", "applicablecurrencies/", new { controller = "Settigs", action = "GetDetails_CurrencyOrganization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("managecurrencyforuser", "managecurrencyforuser/", new { controller = "Settigs", action = "Save_CurrencyOrganization" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesProducts(RouteCollection routes)
        {
            routes.MapRoute("products", "products/{OrganizationCode}", new { controller = "Products", action = "GetList_Product", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("product", "product/{OrganizationCode}", new { controller = "Products", action = "GetDetails_Product", OrganizationCode = UrlParameter.Optional }, new[] { "iGST.Controllers" });
            routes.MapRoute("productsave", "productsave/", new { controller = "Products", action = "Save_Product" }, new[] { "iGST.Controllers" });
            routes.MapRoute("productddlist", "productddlist/", new { controller = "Products", action = "GetList_ProductForDropdownlist" }, new[] { "iGST.Controllers" });

            // Modified on [4th September 2019] by [Partha] cause [Adding new pages] - Start
            routes.MapRoute("applicableexpense", "applicableexpense/", new { controller = "Expense", action = "GetList_ExpenseOrganization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("editexpensedetails", "editexpensedetails/{org}", new { controller = "Expense", action = "GetDetails_ExpenseOrganizationAddEdit" }, new[] { "iGST.Controllers" });
            routes.MapRoute("viewexpensedetails", "viewexpensedetails/{org}", new { controller = "Expense", action = "GetDetails_ExpenseOrganization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("manageexpenseforuser", "manageexpenseforuser/", new { controller = "Expense", action = "Save_ExpenseOrganization" }, new[] { "iGST.Controllers" });
            // End: Modified on [4th September 2019]

            routes.MapRoute("servicesproducts", "servicesproducts/", new { controller = "Products", action = "GetList_ProductOrganization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("editproductdetails", "editproductdetails/{org}", new { controller = "Products", action = "GetDetails_ProductOrganizationAddEdit" }, new[] { "iGST.Controllers" });
            routes.MapRoute("viewproductdetails", "viewproductdetails/{org}", new { controller = "Products", action = "GetDetails_ProductOrganization" }, new[] { "iGST.Controllers" });
            routes.MapRoute("manageproductforuser", "manageproductforuser/", new { controller = "Products", action = "Save_ProductOrganization" }, new[] { "iGST.Controllers" });

            routes.MapRoute("getgst", "getgst/", new { controller = "Products", action = "Get_Gst" }, new[] { "iGST.Controllers" });
            routes.MapRoute("uploadroductorganizationimage", "uploadroductorganizationimage/", new { controller = "Products", action = "Save_ProductOrganizationImage" }, new[] { "iGST.Controllers" });
            routes.MapRoute("getimagefororganizationproduct", "getimagefororganizationproduct/", new { controller = "Products", action = "GetList_ProductOrganizationImage" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesOrganization(RouteCollection routes)
        {
            routes.MapRoute("organizationlist", "organizationlist/", new { controller = "Organization", action = "GetList_OrganizationDropdownList" }, new[] { "iGST.Controllers" });
            routes.MapRoute("orgaccountants", "orgaccountants/", new { controller = "Organization", action = "GetList_OrganizationAccountant" }, new[] { "iGST.Controllers" });
            routes.MapRoute("orgaccountant", "orgaccountant/", new { controller = "Organization", action = "GetDetails_OrganizationAccountant" }, new[] { "iGST.Controllers" });
        }
        public static void RoutesLanguage(RouteCollection routes)
        {
            routes.MapRoute("languages", "languages/", new { controller = "Language", action = "GetList_Language" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagelist", "languagelist/", new { controller = "Language", action = "GetList_LanguageDropdownList" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagesave", "languagesave/", new { controller = "Language", action = "Save_Language" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagecountrylist", "languagecountrylist/", new { controller = "Language", action = "GetList_LanguageCountry" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagecountrydetails", "languagecountrydetails/", new { controller = "Language", action = "GetDetails_LanguageCountry" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagecountrysave", "languagecountrysave/", new { controller = "Language", action = "Save_LanguageCountry" }, new[] { "iGST.Controllers" });
            routes.MapRoute("languagecountrydropdownlist", "languagecountrydropdownlist/", new { controller = "Language", action = "GetList_LanguageCountryDropdownList" }, new[] { "iGST.Controllers" });
            routes.MapRoute("multiplelanguages", "multiplelanguages/", new { controller = "Language", action = "GetList_DataValueLanguageWise" }, new[] { "iGST.Controllers" });
            routes.MapRoute("multiplelanguage", "multiplelanguage/", new { controller = "Language", action = "GetDetails_DataValueLanguageWise" }, new[] { "iGST.Controllers" });
            routes.MapRoute("multiplelanguagesave", "multiplelanguagesave/", new { controller = "Language", action = "Save_DataValueLanguageWise" }, new[] { "iGST.Controllers" });
            routes.MapRoute("LanguageChange", "LanguageChange/", new { controller = "Language", action = "LanguageChange" }, new[] { "iGST.Controllers" });
        }
    }
}
