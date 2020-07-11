﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iGST.Expense_Svc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Expense_Svc.IExpenseService")]
    public interface IExpenseService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/GetList_ExpenseOrganizationResponse")]
        System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo> GetList_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/GetList_ExpenseOrganizationResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo>> GetList_ExpenseOrganizationAsync(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_ExpenseOrganizationResponse")]
        System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo> GetDetails_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_ExpenseOrganizationResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo>> GetDetails_ExpenseOrganizationAsync(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/Save_ExpenseOrganizationResponse")]
        iGST.Expense_Svc.Save_ExpenseOrganizationResponse Save_ExpenseOrganization(iGST.Expense_Svc.Save_ExpenseOrganizationRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_ExpenseOrganization", ReplyAction="http://tempuri.org/IExpenseService/Save_ExpenseOrganizationResponse")]
        System.Threading.Tasks.Task<iGST.Expense_Svc.Save_ExpenseOrganizationResponse> Save_ExpenseOrganizationAsync(iGST.Expense_Svc.Save_ExpenseOrganizationRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_Expense", ReplyAction="http://tempuri.org/IExpenseService/GetList_ExpenseResponse")]
        System.Collections.Generic.List<BusinessObjects.InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_Expense", ReplyAction="http://tempuri.org/IExpenseService/GetList_ExpenseResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.InvoiceInfo>> GetList_ExpenseAsync(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_Expense", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_ExpenseResponse")]
        BusinessObjects.InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_Expense", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_ExpenseResponse")]
        System.Threading.Tasks.Task<BusinessObjects.InvoiceInfo> GetDetails_ExpenseAsync(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_Expense", ReplyAction="http://tempuri.org/IExpenseService/Save_ExpenseResponse")]
        iGST.Expense_Svc.Save_ExpenseResponse Save_Expense(iGST.Expense_Svc.Save_ExpenseRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_Expense", ReplyAction="http://tempuri.org/IExpenseService/Save_ExpenseResponse")]
        System.Threading.Tasks.Task<iGST.Expense_Svc.Save_ExpenseResponse> Save_ExpenseAsync(iGST.Expense_Svc.Save_ExpenseRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_Supplier", ReplyAction="http://tempuri.org/IExpenseService/GetList_SupplierResponse")]
        System.Collections.Generic.List<BusinessObjects.SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetList_Supplier", ReplyAction="http://tempuri.org/IExpenseService/GetList_SupplierResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.SupplierInfo>> GetList_SupplierAsync(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_Supplier", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_SupplierResponse")]
        BusinessObjects.SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/GetDetails_Supplier", ReplyAction="http://tempuri.org/IExpenseService/GetDetails_SupplierResponse")]
        System.Threading.Tasks.Task<BusinessObjects.SupplierInfo> GetDetails_SupplierAsync(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Upload_Supplier", ReplyAction="http://tempuri.org/IExpenseService/Upload_SupplierResponse")]
        iGST.Expense_Svc.Upload_SupplierResponse Upload_Supplier(iGST.Expense_Svc.Upload_SupplierRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Upload_Supplier", ReplyAction="http://tempuri.org/IExpenseService/Upload_SupplierResponse")]
        System.Threading.Tasks.Task<iGST.Expense_Svc.Upload_SupplierResponse> Upload_SupplierAsync(iGST.Expense_Svc.Upload_SupplierRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_Supplier", ReplyAction="http://tempuri.org/IExpenseService/Save_SupplierResponse")]
        iGST.Expense_Svc.Save_SupplierResponse Save_Supplier(iGST.Expense_Svc.Save_SupplierRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExpenseService/Save_Supplier", ReplyAction="http://tempuri.org/IExpenseService/Save_SupplierResponse")]
        System.Threading.Tasks.Task<iGST.Expense_Svc.Save_SupplierResponse> Save_SupplierAsync(iGST.Expense_Svc.Save_SupplierRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_ExpenseOrganization", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_ExpenseOrganizationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOnlyDelete;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public BusinessObjects.ProductOrganiztionInfo obj;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public BusinessObjects.UserInfo objUserInfo;
        
        public Save_ExpenseOrganizationRequest() {
        }
        
        public Save_ExpenseOrganizationRequest(bool isOnlyDelete, BusinessObjects.ProductOrganiztionInfo obj, BusinessObjects.UserInfo objUserInfo) {
            this.isOnlyDelete = isOnlyDelete;
            this.obj = obj;
            this.objUserInfo = objUserInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_ExpenseOrganizationResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_ExpenseOrganizationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Save_ExpenseOrganizationResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string errormsg;
        
        public Save_ExpenseOrganizationResponse() {
        }
        
        public Save_ExpenseOrganizationResponse(bool Save_ExpenseOrganizationResult, string errormsg) {
            this.Save_ExpenseOrganizationResult = Save_ExpenseOrganizationResult;
            this.errormsg = errormsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_Expense", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_ExpenseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOnlyDelete;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public BusinessObjects.InvoiceInfo objBillInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public BusinessObjects.UserInfo objUserInfo;
        
        public Save_ExpenseRequest() {
        }
        
        public Save_ExpenseRequest(bool isOnlyDelete, BusinessObjects.InvoiceInfo objBillInfo, BusinessObjects.UserInfo objUserInfo) {
            this.isOnlyDelete = isOnlyDelete;
            this.objBillInfo = objBillInfo;
            this.objUserInfo = objUserInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_ExpenseResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_ExpenseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Save_ExpenseResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string errormsg;
        
        public Save_ExpenseResponse() {
        }
        
        public Save_ExpenseResponse(bool Save_ExpenseResult, string errormsg) {
            this.Save_ExpenseResult = Save_ExpenseResult;
            this.errormsg = errormsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Upload_Supplier", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Upload_SupplierRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOvereWrite;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public System.Data.DataSet ds;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string OrganizationCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string UserCode;
        
        public Upload_SupplierRequest() {
        }
        
        public Upload_SupplierRequest(bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode) {
            this.isOvereWrite = isOvereWrite;
            this.ds = ds;
            this.OrganizationCode = OrganizationCode;
            this.UserCode = UserCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Upload_SupplierResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Upload_SupplierResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Data.DataSet Upload_SupplierResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public bool bReturn;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string errormsg;
        
        public Upload_SupplierResponse() {
        }
        
        public Upload_SupplierResponse(System.Data.DataSet Upload_SupplierResult, bool bReturn, string errormsg) {
            this.Upload_SupplierResult = Upload_SupplierResult;
            this.bReturn = bReturn;
            this.errormsg = errormsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_Supplier", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_SupplierRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOnlyDelete;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public BusinessObjects.SupplierInfo objSupplierInfo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string UserCode;
        
        public Save_SupplierRequest() {
        }
        
        public Save_SupplierRequest(bool isOnlyDelete, BusinessObjects.SupplierInfo objSupplierInfo, string UserCode) {
            this.isOnlyDelete = isOnlyDelete;
            this.objSupplierInfo = objSupplierInfo;
            this.UserCode = UserCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_SupplierResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_SupplierResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Save_SupplierResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string errormsg;
        
        public Save_SupplierResponse() {
        }
        
        public Save_SupplierResponse(bool Save_SupplierResult, string errormsg) {
            this.Save_SupplierResult = Save_SupplierResult;
            this.errormsg = errormsg;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExpenseServiceChannel : iGST.Expense_Svc.IExpenseService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExpenseServiceClient : System.ServiceModel.ClientBase<iGST.Expense_Svc.IExpenseService>, iGST.Expense_Svc.IExpenseService {
        
        public ExpenseServiceClient() {
        }
        
        public ExpenseServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExpenseServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpenseServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpenseServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo> GetList_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId) {
            return base.Channel.GetList_ExpenseOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo>> GetList_ExpenseOrganizationAsync(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId) {
            return base.Channel.GetList_ExpenseOrganizationAsync(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        
        public System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo> GetDetails_ExpenseOrganization(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId) {
            return base.Channel.GetDetails_ExpenseOrganization(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.ProductOrganiztionInfo>> GetDetails_ExpenseOrganizationAsync(string OrganizationproductId, string ProductID, string ProductName, string OrganizationCode, string CountryId, bool IsActive, string LanguageId) {
            return base.Channel.GetDetails_ExpenseOrganizationAsync(OrganizationproductId, ProductID, ProductName, OrganizationCode, CountryId, IsActive, LanguageId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Expense_Svc.Save_ExpenseOrganizationResponse iGST.Expense_Svc.IExpenseService.Save_ExpenseOrganization(iGST.Expense_Svc.Save_ExpenseOrganizationRequest request) {
            return base.Channel.Save_ExpenseOrganization(request);
        }
        
        public bool Save_ExpenseOrganization(bool isOnlyDelete, BusinessObjects.ProductOrganiztionInfo obj, BusinessObjects.UserInfo objUserInfo, out string errormsg) {
            iGST.Expense_Svc.Save_ExpenseOrganizationRequest inValue = new iGST.Expense_Svc.Save_ExpenseOrganizationRequest();
            inValue.isOnlyDelete = isOnlyDelete;
            inValue.obj = obj;
            inValue.objUserInfo = objUserInfo;
            iGST.Expense_Svc.Save_ExpenseOrganizationResponse retVal = ((iGST.Expense_Svc.IExpenseService)(this)).Save_ExpenseOrganization(inValue);
            errormsg = retVal.errormsg;
            return retVal.Save_ExpenseOrganizationResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Expense_Svc.Save_ExpenseOrganizationResponse> Save_ExpenseOrganizationAsync(iGST.Expense_Svc.Save_ExpenseOrganizationRequest request) {
            return base.Channel.Save_ExpenseOrganizationAsync(request);
        }
        
        public System.Collections.Generic.List<BusinessObjects.InvoiceInfo> GetList_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled) {
            return base.Channel.GetList_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.InvoiceInfo>> GetList_ExpenseAsync(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled) {
            return base.Channel.GetList_ExpenseAsync(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }
        
        public BusinessObjects.InvoiceInfo GetDetails_Expense(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled) {
            return base.Channel.GetDetails_Expense(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.InvoiceInfo> GetDetails_ExpenseAsync(string InvoiceID, string BranchID, string CusID, string OrganizationCode, string InvoiceDateFrom, string InvoiceDateTo, string IsReturned, string IsCancelled) {
            return base.Channel.GetDetails_ExpenseAsync(InvoiceID, BranchID, CusID, OrganizationCode, InvoiceDateFrom, InvoiceDateTo, IsReturned, IsCancelled);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Expense_Svc.Save_ExpenseResponse iGST.Expense_Svc.IExpenseService.Save_Expense(iGST.Expense_Svc.Save_ExpenseRequest request) {
            return base.Channel.Save_Expense(request);
        }
        
        public bool Save_Expense(bool isOnlyDelete, BusinessObjects.InvoiceInfo objBillInfo, BusinessObjects.UserInfo objUserInfo, out string errormsg) {
            iGST.Expense_Svc.Save_ExpenseRequest inValue = new iGST.Expense_Svc.Save_ExpenseRequest();
            inValue.isOnlyDelete = isOnlyDelete;
            inValue.objBillInfo = objBillInfo;
            inValue.objUserInfo = objUserInfo;
            iGST.Expense_Svc.Save_ExpenseResponse retVal = ((iGST.Expense_Svc.IExpenseService)(this)).Save_Expense(inValue);
            errormsg = retVal.errormsg;
            return retVal.Save_ExpenseResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Expense_Svc.Save_ExpenseResponse> Save_ExpenseAsync(iGST.Expense_Svc.Save_ExpenseRequest request) {
            return base.Channel.Save_ExpenseAsync(request);
        }
        
        public System.Collections.Generic.List<BusinessObjects.SupplierInfo> GetList_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId) {
            return base.Channel.GetList_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.SupplierInfo>> GetList_SupplierAsync(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId) {
            return base.Channel.GetList_SupplierAsync(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }
        
        public BusinessObjects.SupplierInfo GetDetails_Supplier(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId) {
            return base.Channel.GetDetails_Supplier(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.SupplierInfo> GetDetails_SupplierAsync(string SupID, string BranchId, string OrganizationCode, bool IsActive, string UserID, string LanguageId) {
            return base.Channel.GetDetails_SupplierAsync(SupID, BranchId, OrganizationCode, IsActive, UserID, LanguageId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Expense_Svc.Upload_SupplierResponse iGST.Expense_Svc.IExpenseService.Upload_Supplier(iGST.Expense_Svc.Upload_SupplierRequest request) {
            return base.Channel.Upload_Supplier(request);
        }
        
        public System.Data.DataSet Upload_Supplier(bool isOvereWrite, System.Data.DataSet ds, string OrganizationCode, string UserCode, out bool bReturn, out string errormsg) {
            iGST.Expense_Svc.Upload_SupplierRequest inValue = new iGST.Expense_Svc.Upload_SupplierRequest();
            inValue.isOvereWrite = isOvereWrite;
            inValue.ds = ds;
            inValue.OrganizationCode = OrganizationCode;
            inValue.UserCode = UserCode;
            iGST.Expense_Svc.Upload_SupplierResponse retVal = ((iGST.Expense_Svc.IExpenseService)(this)).Upload_Supplier(inValue);
            bReturn = retVal.bReturn;
            errormsg = retVal.errormsg;
            return retVal.Upload_SupplierResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Expense_Svc.Upload_SupplierResponse> Upload_SupplierAsync(iGST.Expense_Svc.Upload_SupplierRequest request) {
            return base.Channel.Upload_SupplierAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Expense_Svc.Save_SupplierResponse iGST.Expense_Svc.IExpenseService.Save_Supplier(iGST.Expense_Svc.Save_SupplierRequest request) {
            return base.Channel.Save_Supplier(request);
        }
        
        public bool Save_Supplier(bool isOnlyDelete, BusinessObjects.SupplierInfo objSupplierInfo, string UserCode, out string errormsg) {
            iGST.Expense_Svc.Save_SupplierRequest inValue = new iGST.Expense_Svc.Save_SupplierRequest();
            inValue.isOnlyDelete = isOnlyDelete;
            inValue.objSupplierInfo = objSupplierInfo;
            inValue.UserCode = UserCode;
            iGST.Expense_Svc.Save_SupplierResponse retVal = ((iGST.Expense_Svc.IExpenseService)(this)).Save_Supplier(inValue);
            errormsg = retVal.errormsg;
            return retVal.Save_SupplierResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Expense_Svc.Save_SupplierResponse> Save_SupplierAsync(iGST.Expense_Svc.Save_SupplierRequest request) {
            return base.Channel.Save_SupplierAsync(request);
        }
    }
}
