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
    public partial class BillService : IBillService
    {
        #region Bill Related
        public List<InvoiceInfo> GetList_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            return wsBills.GetList_Bill(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }

        public InvoiceInfo GetDetails_Bill(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled)
        {
            return wsBills.GetDetails_Bill(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }

        public bool Save_Bill(bool isOnlyDelete, InvoiceInfo objBillInfo, string UserCode, out string errormsg)
        {
            return wsBills.Save_Bill(isOnlyDelete, objBillInfo, UserCode, out errormsg);
        }
        #endregion

        #region Customer Related
        public List<CustomerInfo> GetList_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            return wsCustomer.GetList_Customer(UserType, CusID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }

        public CustomerInfo GetDetails_Customer(string UserType, string CusID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId)
        {
            return wsCustomer.GetDetails_Customer(UserType, CusID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }

        public System.Data.DataSet Upload_Customer(string UserType, bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg)
        {
            return wsCustomer.Upload_Customer(UserType, isOvereWrite, ds, OrganizationCode, UserCode, out bReturn, out errormsg);
        }

        public bool Save_Customer(string UserType, bool isOnlyDelete, CustomerInfo objCustomerInfo, string UserCode, out string errormsg)
        {
            return wsCustomer.Save_Customer(UserType, isOnlyDelete, objCustomerInfo, UserCode, out errormsg);
        }
        
        public bool Save_CustomerImage(bool isOnlyDelete, CustomerImageInfo obj, string UserCode, out string errormsg)
        {
            return wsCustomer.Save_CustomerImage(isOnlyDelete, obj, UserCode, out errormsg);
        }

        public List<CustomerImageInfo> GetList_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain)
        {
            return wsCustomer.GetList_CustomerImage(ImageId, CustomerID, IsActive, IsMain);
        }

        public CustomerImageInfo GetDetails_CustomerImage(string ImageId, string CustomerID, string IsActive, string IsMain)
        {
            return wsCustomer.GetDetails_CustomerImage(ImageId, CustomerID, IsActive, IsMain);
        }
        #endregion
    }
}
