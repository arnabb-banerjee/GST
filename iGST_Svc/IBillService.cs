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
    public interface IBillService
    {
        #region Bill Related
        [OperationContract]
        List<InvoiceInfo> GetList_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);

        [OperationContract]
        InvoiceInfo GetDetails_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);

        [OperationContract]
        bool Save_Bill(bool isOnlyDelete, InvoiceInfo objBillInfo, string UserCode, out string errormsg);
        #endregion

        #region Customer Related
        [OperationContract]
        List<CustomerInfo> GetList_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);

        [OperationContract]
        CustomerInfo GetDetails_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);

        [OperationContract]
        System.Data.DataSet Upload_Customer(string UserType, bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg);

        [OperationContract]
        bool Save_Customer(string UserType, bool isOnlyDelete, CustomerInfo objCustomerInfo, string UserCode, out string errormsg);

        [OperationContract]
        bool Save_CustomerImage(bool isOnlyDelete, CustomerImageInfo obj, string UserCode, out string errormsg);

        [OperationContract]
        List<CustomerImageInfo> GetList_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain);

        [OperationContract]
        CustomerImageInfo GetDetails_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain);
        #endregion
    }
}
