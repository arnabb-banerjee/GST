using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessObjects;

namespace iGST_Svc
{
    [KnownType(typeof(UserInfo))]
    public partial class ExpenseService : IExpenseService
    {
        #region Expense Related
        // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
        public List<ProductOrganiztionInfo> GetList_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            return wsExpense.GetList_ExpenseOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        // Modified on [30th August 2019] by [Partha]

        public List<ProductOrganiztionInfo> GetDetails_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId)
        {
            return wsExpense.GetDetails_ExpenseOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }

        public bool Save_ExpenseOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wsExpense.Save_ExpenseOrganization(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public List<InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            return wsExpense.GetList_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }

        public InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            return wsExpense.GetDetails_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }

        public bool Save_Expense(bool isOnlyDelete, InvoiceInfo objBillInfo, UserInfo objUserInfo, out string errormsg)
        {
            return wsExpense.Save_Expense(isOnlyDelete, objBillInfo, objUserInfo, out errormsg);
        }
        #endregion

        #region Supplier Related
        public List<SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            return wsSupplier.GetList_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }

        public SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            return wsSupplier.GetDetails_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }

        public System.Data.DataSet Upload_Supplier(bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg)
        {
            return wsSupplier.Upload_Supplier(isOvereWrite, ds, OrganizationCode, UserCode, out bReturn, out errormsg);
        }

        public bool Save_Supplier(bool isOnlyDelete, SupplierInfo objSupplierInfo, string UserCode, out string errormsg)
        {
            return wsSupplier.Save_Supplier(isOnlyDelete, objSupplierInfo, UserCode, out errormsg);
        }
        #endregion
    }
}
