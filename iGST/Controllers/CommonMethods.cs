using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BusinessObjects;
using System.Data;

namespace iGST.Controllers
{
    public class CommonMethods
    {
        public static string ListOrganizations(string SelectedOrganization)
        {
            List<OrganizationInfo> Organizations = null;
            System.Text.StringBuilder sbOrganizationList = new System.Text.StringBuilder();
            sbOrganizationList.AppendLine("<option value=\"\">Select one</option>");

            if (System.Web.HttpContext.Current.Session["Organization"] == null)
            {
                try
                {
                    SelectedOrganization = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();

                    if (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R")
                    {
                        if (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null &&
                            ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim().Length > 0)
                        {
                            Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim(), "", "");

                            if (Organizations != null && Organizations.Count > 0)
                            {
                                System.Web.HttpContext.Current.Session["Organization"] = Organizations;

                                foreach (OrganizationInfo country in Organizations)
                                {
                                    if (!string.IsNullOrEmpty(SelectedOrganization) && country.OrganizationCode.Trim() == SelectedOrganization.Trim())
                                        sbOrganizationList.AppendLine("<option selected=\"selected\" value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                                }
                            }

                        }
                    }
                    else if (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "M")
                    {
                        Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList("", "", "");

                        if (Organizations != null && Organizations.Count > 0)
                        {
                            System.Web.HttpContext.Current.Session["Organization"] = Organizations;

                            foreach (OrganizationInfo country in Organizations)
                            {
                                if (!string.IsNullOrEmpty(SelectedOrganization) && country.OrganizationCode.Trim() == SelectedOrganization.Trim())
                                    sbOrganizationList.AppendLine("<option selected=\"selected\" value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                                else
                                    sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                            }
                        }
                    }
                    else if (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "A")
                    {
                        Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList("", "", "");

                        if (Organizations != null && Organizations.Count > 0)
                        {
                            System.Web.HttpContext.Current.Session["Organization"] = Organizations;

                            foreach (OrganizationInfo country in Organizations)
                            {
                                if (!string.IsNullOrEmpty(SelectedOrganization) && country.OrganizationCode.Trim() == SelectedOrganization.Trim())
                                    sbOrganizationList.AppendLine("<option selected=\"selected\" value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                                else
                                    sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                            }
                        }
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["Organization"] = null;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                    System.Web.HttpContext.Current.Session["Organization"] = null;
                    Organizations = new List<OrganizationInfo>();
                }
            }
            else
            {
                Organizations = (List<OrganizationInfo>)System.Web.HttpContext.Current.Session["Organization"];

                if (Organizations != null && Organizations.Count > 0)
                {
                    System.Web.HttpContext.Current.Session["Organization"] = Organizations;

                    foreach (OrganizationInfo country in Organizations)
                    {
                        if (!string.IsNullOrEmpty(SelectedOrganization) && country.OrganizationCode.Trim() == SelectedOrganization.Trim())
                            sbOrganizationList.AppendLine("<option selected=\"selected\" value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                        else
                            sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                    }
                }

            }

            return sbOrganizationList.ToString();
        }

        public static string ListCountry(string SelectedContry)
        {
            List<CountryInfo> countries = null;
            System.Text.StringBuilder sbCountryList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["Country"] == null)
                {
                    countries = new Master_Svc.MasterServiceClient().GetList_Country();
                    System.Web.HttpContext.Current.Application["Country"] = countries;
                }
                else
                {
                    countries = (List<CountryInfo>)System.Web.HttpContext.Current.Application["Country"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Country ist", "");
                countries = new List<CountryInfo>();
            }

            sbCountryList.AppendLine("<option value=\"\">Select one</option>");

            if (countries != null && countries.Count > 0)
            {
                foreach (CountryInfo country in countries)
                {
                    if (!string.IsNullOrEmpty(SelectedContry) && country.CountryID.Trim() == SelectedContry.Trim())
                        sbCountryList.AppendLine("<option value=\"" + country.CountryID + "\" selected=\"selected\">" + country.CountryName + "</option>");
                    else
                        sbCountryList.AppendLine("<option value=\"" + country.CountryID + "\">" + country.CountryName + "</option>");
                }
            }

            return sbCountryList.ToString();
        }

        public static string ListStates(string SelectedContry, string SelectedState)
        {
            List<StateInfo> states = null;
            System.Text.StringBuilder sbStateList = new System.Text.StringBuilder();

            try
            {
                states = new Master_Svc.MasterServiceClient().GetList_State(SelectedContry);
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Country ist", "");
                states = new List<StateInfo>();
            }

            sbStateList.AppendLine("<option value=\"\">Select one</option>");

            if (states != null && states.Count > 0)
            {
                foreach (StateInfo state in states)
                {
                    if (!string.IsNullOrEmpty(SelectedState) && state.StateID.Trim() == SelectedState.Trim())
                        sbStateList.AppendLine("<option value=\"" + state.StateID + "\" selected=\"selected\">" + state.StateName + "</option>");
                    else
                        sbStateList.AppendLine("<option value=\"" + state.StateID + "\">" + state.StateName + "</option>");
                }
            }

            return sbStateList.ToString();
        }

        public static string ListApplicableCurrencies(string SelectedCurrency)
        {
            List<CurrencyOrganiztionInfo> Currencies = null;
            System.Text.StringBuilder sbCurrencies = new System.Text.StringBuilder();

            try
            {
                string OrganizationCode = "";
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    OrganizationCode = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["Currencies"] == null)
                {
                    Currencies = new Currency_Svc.CurrencyServiceClient().GetDetails_BUCurrencies("", "", OrganizationCode);
                    System.Web.HttpContext.Current.Session["Currencies"] = Currencies;
                }
                else
                {
                    Currencies = (List<CurrencyOrganiztionInfo>)System.Web.HttpContext.Current.Session["Currencies"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Currencies = new List<CurrencyOrganiztionInfo>();
            }

            sbCurrencies.AppendLine("<option value=\"\">Select one</option>");

            if (Currencies != null && Currencies.Count > 0)
            {
                foreach (CurrencyOrganiztionInfo obj in Currencies)
                {
                    if (!string.IsNullOrEmpty(SelectedCurrency) && obj.CurrencyId.Trim() == SelectedCurrency.Trim())
                        sbCurrencies.AppendLine("<option value=\"" + obj.CurrencyId + "\" selected=\"selected\">" + obj.CurrencyName + "</option>");
                    else
                        sbCurrencies.AppendLine("<option value=\"" + obj.CurrencyId + "\">" + obj.CurrencyName + "</option>");
                }
            }

            return sbCurrencies.ToString();
        }

        public static string ListTaxes(string SelectedItem)
        {
            List<TaxMasterInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["Taxmasterlist"] == null)
                {
                    Suppliers = new iGst_Svc.GSTServiceClient().GetList_TaxMaster(0, "");
                    System.Web.HttpContext.Current.Application["Taxmasterlist"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<TaxMasterInfo>)System.Web.HttpContext.Current.Application["Taxmasterlist"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<TaxMasterInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (TaxMasterInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.TaxDefinationID.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.TaxDefinationID + "\" selected=\"selected\">" + country.TaxName + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.TaxDefinationID + "\">" + country.TaxName + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListCategory(string SelectedItem, string CountryID, bool doNotUseCatche = false)
        {
            List<CategoryInfo> Categories = null;
            System.Text.StringBuilder sbCategoryList = new System.Text.StringBuilder();
            int d = 0;

            int.TryParse(CountryID, out d);

            if (doNotUseCatche)
            {
                System.Web.HttpContext.Current.Application["Category"] = null;
                Categories = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown("", d, SelectedItem, "", "");
            }
            else
            {
                try
                {
                    if (System.Web.HttpContext.Current.Application["Category"] == null)
                    {

                        Categories = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown("", d, SelectedItem, "", "");
                        System.Web.HttpContext.Current.Application["Category"] = Categories;
                    }
                    else
                    {
                        Categories = (List<CategoryInfo>)System.Web.HttpContext.Current.Application["Category"];
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                    Categories = new List<CategoryInfo>();
                }
            }

            sbCategoryList.AppendLine("<option value=\"\">Select one</option>");

            if (Categories != null && Categories.Count > 0)
            {
                var parentlist = (from cat in Categories
                                  where cat.ParentCategoryId.Trim() == ""
                                  select cat).ToList().OrderBy(m=>m.CategoryName);

                foreach (CategoryInfo obj in parentlist)
                {
                    sbCategoryList.AppendLine("<optgroup label=" + obj.CategoryName + "></option>");

                    foreach (CategoryInfo category in Categories)
                    {
                        if (category.ParentCategoryId.Trim() == obj.CategoryId.Trim())
                            if (!string.IsNullOrEmpty(SelectedItem) && category.CategoryId.Trim() == SelectedItem.Trim())
                                sbCategoryList.AppendLine("<option value=\"" + category.CategoryId + "\" selected=\"selected\">" + category.CategoryName + "</option>");
                            else
                                sbCategoryList.AppendLine("<option value=\"" + category.CategoryId + "\">" + category.CategoryName + "</option>");
                    }
                }
            }

            return sbCategoryList.ToString();
        }

        public static string ListCategory(string isExpensive, string SelectedItem, string CountryID)
        {
            List<CategoryInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            int d = 0;

            int.TryParse(CountryID, out d);

            try
            {
                if (System.Web.HttpContext.Current.Application["Category"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown(isExpensive, d, SelectedItem, "", "");
                    System.Web.HttpContext.Current.Application["Category"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<CategoryInfo>)System.Web.HttpContext.Current.Application["Category"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<CategoryInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (CategoryInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.CategoryId.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.CategoryId + "\" selected=\"selected\">" + country.CategoryName + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.CategoryId + "\">" + country.CategoryName + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListServiceUnit(string SelectedItem)
        {
            List<ServiceUnitInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["ServiceUnit"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_ServiceUnit("", "", true);
                    System.Web.HttpContext.Current.Application["ServiceUnit"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<ServiceUnitInfo>)System.Web.HttpContext.Current.Application["ServiceUnit"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<ServiceUnitInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (ServiceUnitInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.ServiceUnitId.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceUnitId + "\" selected=\"selected\">" + country.ServiceUnitName + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceUnitId + "\">" + country.ServiceUnitName + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListServiceType(string SelectedItem)
        {
            List<ServiceTypeInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["ServiceType"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_ServiceType("", "", true);
                    System.Web.HttpContext.Current.Application["ServiceType"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<ServiceTypeInfo>)System.Web.HttpContext.Current.Application["ServiceType"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<ServiceTypeInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (ServiceTypeInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.ServiceTypeId.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceTypeId + "\" selected=\"selected\">" + country.ServiceTypeName + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceTypeId + "\">" + country.ServiceTypeName + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListServiceClass(string SelectedItem)
        {
            List<ServiceClassInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["ServiceClass"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_ServiceClass("", "", true);
                    System.Web.HttpContext.Current.Application["ServiceClass"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<ServiceClassInfo>)System.Web.HttpContext.Current.Application["ServiceClass"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<ServiceClassInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (ServiceClassInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.ServiceClassId.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceClassId + "\" selected=\"selected\">" + country.ServiceClassName + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.ServiceClassId + "\">" + country.ServiceClassName + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListBanks(string SelectedOrganization, string SelectedItem)
        {
            List<BankInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    SelectedOrganization = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["Banks"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_Bank(SelectedItem, SelectedOrganization, true);
                    System.Web.HttpContext.Current.Session["Banks"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<BankInfo>)System.Web.HttpContext.Current.Session["Banks"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<BankInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (BankInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.BankID.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.BankID + "\" selected=\"selected\">" + country.Name + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.BankID + "\">" + country.Name + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListSupplier(string SelectedOrganization, string SelectedItem)
        {
            List<CustomerInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    SelectedOrganization = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["Suppliers"] == null)
                {
                    Suppliers = new Bill_Svc.BillServiceClient().GetList_Customer("S", SelectedItem, "", SelectedOrganization, false, "", "");
                    System.Web.HttpContext.Current.Session["Suppliers"] = Suppliers;
                }
                else
                {
                    Suppliers = (List<CustomerInfo>)System.Web.HttpContext.Current.Session["Suppliers"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Suppliers = new List<CustomerInfo>();
            }

            sbSupplierList.AppendLine("<option value=\"\">Select one</option>");

            if (Suppliers != null && Suppliers.Count > 0)
            {
                foreach (CustomerInfo country in Suppliers)
                {
                    if (!string.IsNullOrEmpty(SelectedItem) && country.CusID.Trim() == SelectedItem.Trim())
                        sbSupplierList.AppendLine("<option value=\"" + country.CusID + "\" selected=\"selected\">" + (country.First_Name + " " + country.Last_Name) + "</option>");
                    else
                        sbSupplierList.AppendLine("<option value=\"" + country.CusID + "\">" + (country.First_Name + " " + country.Last_Name) + "</option>");
                }
            }

            return sbSupplierList.ToString();
        }

        public static string ListCustomer(string SelectedOrganization, string SelectedCustomer)
        {
            List<CustomerInfo> Customers = null;
            System.Text.StringBuilder sbCustomerList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    SelectedOrganization = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["Customers"] == null)
                {
                    Customers = new Bill_Svc.BillServiceClient().GetList_Customer("C", SelectedCustomer, "", SelectedOrganization, false, "", "");
                    System.Web.HttpContext.Current.Session["Customers"] = Customers;
                }
                else
                {
                    Customers = (List<CustomerInfo>)System.Web.HttpContext.Current.Session["Customers"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Customers = new List<CustomerInfo>();
            }

            sbCustomerList.AppendLine("<option value=\"\">Select one</option>");

            if (Customers != null && Customers.Count > 0)
            {
                foreach (CustomerInfo country in Customers)
                {
                    if (!string.IsNullOrEmpty(SelectedCustomer) && country.CusID.Trim() == SelectedCustomer.Trim())
                        sbCustomerList.AppendLine("<option value=\"" + country.CusID + "\" selected=\"selected\">" + (country.First_Name + " " + country.Last_Name) + "</option>");
                    else
                        sbCustomerList.AppendLine("<option value=\"" + country.CusID + "\">" + (country.First_Name + " " + country.Last_Name) + "</option>");
                }
            }

            return sbCustomerList.ToString();
        }

        public static string ListOrganizationProducts(string SelectedOrganization, string SelectedOrganizationproductId)
        {
            List<ProductOrganiztionInfo> Organizations = null;
            System.Text.StringBuilder sbOrganizationList = new System.Text.StringBuilder();

            try
            {
                string OrganizationCode = "";
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    OrganizationCode = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["ProductOrganiztion"] == null)
                {
                    // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
                    Organizations = new Product_Svc.ProductServiceClient().GetList_ProductOrganization("", "", "", SelectedOrganization, "", true, "0");
                    // END: Modified on[30th August 2019] by[Partha]
                    System.Web.HttpContext.Current.Session["ProductOrganiztion"] = Organizations;
                }
                else
                {
                    Organizations = (List<ProductOrganiztionInfo>)System.Web.HttpContext.Current.Session["ProductOrganiztion"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Organizations = new List<ProductOrganiztionInfo>();
            }

            sbOrganizationList.AppendLine("<option value=\"\">Select one</option>");

            if (Organizations != null && Organizations.Count > 0)
            {
                foreach (ProductOrganiztionInfo country in Organizations)
                {
                    if (!string.IsNullOrEmpty(SelectedOrganizationproductId) && country.OrganizationproductId.ToString().Trim() == SelectedOrganizationproductId.Trim())
                        sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationproductId + "\" selected=\"selected\">" + country.ProductName + "</option>");
                    else
                        sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationproductId + "\">" + country.ProductName + "</option>");
                }
            }

            return sbOrganizationList.ToString();
        }

        public static string ListProducts(string OrganizationCode, string SelectedProduct)
        {
            List<ProductInfo> products = null;
            System.Text.StringBuilder sbProducList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Session["ProductList"] == null)
                {
                    if (System.Web.HttpContext.Current.Session["Language"] == null) System.Web.HttpContext.Current.Session["Language"] = 0;

                    if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                    {
                        OrganizationCode = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                    }

                    products = new Product_Svc.ProductServiceClient().GetList_Product("", "", "", OrganizationCode, "", false, System.Web.HttpContext.Current.Session["Language"].ToString());
                    System.Web.HttpContext.Current.Session["ProductList"] = products;
                }
                else
                {
                    products = (List<ProductInfo>)System.Web.HttpContext.Current.Session["ProductList"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Country ist", "");
                products = new List<ProductInfo>();
            }

            sbProducList.AppendLine("<option value=\"\">Select one</option>");

            if (products != null && products.Count > 0)
            {
                foreach (ProductInfo state in products)
                {
                    if (!string.IsNullOrEmpty(SelectedProduct) && state.ProductId.Trim() == SelectedProduct.Trim())
                        sbProducList.AppendLine("<option value=\"" + state.ProductId + "\" selected=\"selected\">" + state.ProductName + "</option>");
                    else
                        sbProducList.AppendLine("<option value=\"" + state.ProductId + "\">" + state.ProductName + "</option>");
                }
            }

            return sbProducList.ToString();
        }

        public static List<StaticValuInfo> ListStaticValue()
        {
            List<StaticValuInfo> countries = null;
            System.Text.StringBuilder sbCountryList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["StaticValue"] == null)
                {
                    countries = new Master_Svc.MasterServiceClient().GetList_StaticValue("");
                    System.Web.HttpContext.Current.Application["StaticValue"] = countries;
                    return countries;
                }
                else
                {
                    return (List<StaticValuInfo>)System.Web.HttpContext.Current.Application["StaticValue"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Country ist", "");
            }

            return new List<StaticValuInfo>();
        }
    }

}

