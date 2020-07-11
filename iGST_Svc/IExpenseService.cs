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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IExpenseService
    {
        #region Expense Related
        // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
        [OperationContract]
        List<ProductOrganiztionInfo> GetList_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        // Modified on [30th August 2019] by [Partha]

        [OperationContract]
        List<ProductOrganiztionInfo> GetDetails_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);

        [OperationContract]
        bool Save_ExpenseOrganization(bool isOnlyDelete, ProductOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg);





        [OperationContract]
        List<InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);

        [OperationContract]
        InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);

        [OperationContract]
        bool Save_Expense(bool isOnlyDelete, InvoiceInfo objBillInfo, UserInfo objUserInfo, out string errormsg);
        #endregion

        #region Supplier Related
        [OperationContract]
        List<SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);

        [OperationContract]
        SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);

        [OperationContract]
        System.Data.DataSet Upload_Supplier(bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg);

        [OperationContract]
        bool Save_Supplier(bool isOnlyDelete, SupplierInfo objSupplierInfo, string UserCode, out string errormsg);
        #endregion
    }
}
