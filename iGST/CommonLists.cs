using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BusinessObjects;
using System.Web.Mvc;

namespace iGST
{
    public class CommonLists
    {
        #region Methods
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
        public static IEnumerable<SelectListItem> GetListTax(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
        }

        public static string ListCategory(string SelectedItem, string cOUNTRY)
        {
            List<CategoryInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["Category"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown("", SelectedItem, "", "");
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
        public static IEnumerable<SelectListItem> GetListCategory(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
        }

        public static string ListCategory(string isExpensive, string SelectedItem)
        {
            List<CategoryInfo> Suppliers = null;
            System.Text.StringBuilder sbSupplierList = new System.Text.StringBuilder();

            try
            {
                if (System.Web.HttpContext.Current.Application["Category"] == null)
                {
                    Suppliers = new Master_Svc.MasterServiceClient().GetList_CategoryForDropdown(isExpensive, SelectedItem, "", "");
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
        public static IEnumerable<SelectListItem> GetListCategory(string SelectedItemValue, string SelectedItem)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListServiceUnit(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListServiceType(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListServiceClass(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListBanks(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListSupplier(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListCustomer(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
        }

        public static string ListOrganizations(string SelectedOrganization)
        {
            List<OrganizationInfo> Organizations = null;
            System.Text.StringBuilder sbOrganizationList = new System.Text.StringBuilder();

            try
            {
                string OrganizationCode = "";
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                {
                    OrganizationCode = ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim();
                }

                if (System.Web.HttpContext.Current.Session["Organization"] == null)
                {
                    Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(OrganizationCode, "", "");
                    System.Web.HttpContext.Current.Session["Organization"] = Organizations;
                }
                else
                {
                    Organizations = (List<OrganizationInfo>)System.Web.HttpContext.Current.Session["Organization"];
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
                Organizations = new List<OrganizationInfo>();
            }

            sbOrganizationList.AppendLine("<option value=\"\">Select one</option>");

            if (Organizations != null && Organizations.Count > 0)
            {
                foreach (OrganizationInfo country in Organizations)
                {
                    if (!string.IsNullOrEmpty(SelectedOrganization) && country.OrganizationCode.Trim() == SelectedOrganization.Trim())
                        sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationCode + "\" selected=\"selected\">" + country.OrganizationName + "</option>");
                    else
                        sbOrganizationList.AppendLine("<option value=\"" + country.OrganizationCode + "\">" + country.OrganizationName + "</option>");
                }
            }

            return sbOrganizationList.ToString();
        }
        public static IEnumerable<SelectListItem> GetListOrganizations(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListOrganizationProducts(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListCountry(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListProducts(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListStates(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["OrganizationListItem"] == null)
                        {
                            List<OrganizationInfo> Organizations = new Auth_Svc.UserAuthenticationServiceClient().GetList_OrganizationDropdownList(

                                (string.IsNullOrEmpty(((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim()) ? ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode.Trim() : "")
                                , "", "");

                            list = (from items in Organizations
                                    select new SelectListItem
                                    {
                                        Text = items.OrganizationName,
                                        Value = items.OrganizationCode,
                                    });

                            System.Web.HttpContext.Current.Session["OrganizationListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["OrganizationListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
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
        public static IEnumerable<SelectListItem> GetListStaticValue(string SelectedItemValue)
        {
            IEnumerable<SelectListItem> list = null;

            try
            {
                if (System.Web.HttpContext.Current.Session["UserDetails"] != null)
                {
                    if (
                        (((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() == "R" && ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).OrganizationCode != null)
                        || ((UserInfo)System.Web.HttpContext.Current.Session["UserDetails"]).UserType.ToString().Trim().ToUpper() != "R"
                       )
                    {
                        if (System.Web.HttpContext.Current.Session["StaticValueListItem"] == null)
                        {
                            var countries = new Master_Svc.MasterServiceClient().GetList_StaticValue("");
                            System.Web.HttpContext.Current.Application["StaticValue"] = countries;

                            list = (from items in countries
                                    select new SelectListItem
                                    {
                                        Text = items.Value,
                                        Value = items.Key,
                                    });

                            System.Web.HttpContext.Current.Session["StaticValueListItem"] = list;
                        }
                        else
                        {
                            list = (IEnumerable<SelectListItem>)System.Web.HttpContext.Current.Session["StaticValueListItem"];
                        }
                    }
                }

                foreach (SelectListItem item in list)
                {
                    if (item.Value == SelectedItemValue) { item.Selected = true; }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrors_Comments(ex.Message + ex.InnerException + ex.StackTrace, "Controler", "Organization ist", "");
            }

            return list;
        }
        #endregion

    }
}