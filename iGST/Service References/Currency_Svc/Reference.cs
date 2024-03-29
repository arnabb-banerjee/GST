﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iGST.Currency_Svc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Currency_Svc.ICurrencyService")]
    public interface ICurrencyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/GetDetails_BUCurrencies", ReplyAction="http://tempuri.org/ICurrencyService/GetDetails_BUCurrenciesResponse")]
        System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo> GetDetails_BUCurrencies(string OrganizationproductId, string CurrencyId, string OrganizationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/GetDetails_BUCurrencies", ReplyAction="http://tempuri.org/ICurrencyService/GetDetails_BUCurrenciesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo>> GetDetails_BUCurrenciesAsync(string OrganizationproductId, string CurrencyId, string OrganizationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/GetDetails_CurrencyOrganization", ReplyAction="http://tempuri.org/ICurrencyService/GetDetails_CurrencyOrganizationResponse")]
        System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo> GetDetails_CurrencyOrganization(string OrganizationproductId, string CurrencyId, string OrganizationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/GetDetails_CurrencyOrganization", ReplyAction="http://tempuri.org/ICurrencyService/GetDetails_CurrencyOrganizationResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo>> GetDetails_CurrencyOrganizationAsync(string OrganizationproductId, string CurrencyId, string OrganizationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/Save_CurrencyOrganization", ReplyAction="http://tempuri.org/ICurrencyService/Save_CurrencyOrganizationResponse")]
        iGST.Currency_Svc.Save_CurrencyOrganizationResponse Save_CurrencyOrganization(iGST.Currency_Svc.Save_CurrencyOrganizationRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyService/Save_CurrencyOrganization", ReplyAction="http://tempuri.org/ICurrencyService/Save_CurrencyOrganizationResponse")]
        System.Threading.Tasks.Task<iGST.Currency_Svc.Save_CurrencyOrganizationResponse> Save_CurrencyOrganizationAsync(iGST.Currency_Svc.Save_CurrencyOrganizationRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_CurrencyOrganization", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_CurrencyOrganizationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOnlyDelete;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public BusinessObjects.CurrencyOrganiztionInfo obj;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public BusinessObjects.UserInfo objUserInfo;
        
        public Save_CurrencyOrganizationRequest() {
        }
        
        public Save_CurrencyOrganizationRequest(bool isOnlyDelete, BusinessObjects.CurrencyOrganiztionInfo obj, BusinessObjects.UserInfo objUserInfo) {
            this.isOnlyDelete = isOnlyDelete;
            this.obj = obj;
            this.objUserInfo = objUserInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_CurrencyOrganizationResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_CurrencyOrganizationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Save_CurrencyOrganizationResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string errormsg;
        
        public Save_CurrencyOrganizationResponse() {
        }
        
        public Save_CurrencyOrganizationResponse(bool Save_CurrencyOrganizationResult, string errormsg) {
            this.Save_CurrencyOrganizationResult = Save_CurrencyOrganizationResult;
            this.errormsg = errormsg;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICurrencyServiceChannel : iGST.Currency_Svc.ICurrencyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CurrencyServiceClient : System.ServiceModel.ClientBase<iGST.Currency_Svc.ICurrencyService>, iGST.Currency_Svc.ICurrencyService {
        
        public CurrencyServiceClient() {
        }
        
        public CurrencyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CurrencyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo> GetDetails_BUCurrencies(string OrganizationproductId, string CurrencyId, string OrganizationCode) {
            return base.Channel.GetDetails_BUCurrencies(OrganizationproductId, CurrencyId, OrganizationCode);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo>> GetDetails_BUCurrenciesAsync(string OrganizationproductId, string CurrencyId, string OrganizationCode) {
            return base.Channel.GetDetails_BUCurrenciesAsync(OrganizationproductId, CurrencyId, OrganizationCode);
        }
        
        public System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo> GetDetails_CurrencyOrganization(string OrganizationproductId, string CurrencyId, string OrganizationCode) {
            return base.Channel.GetDetails_CurrencyOrganization(OrganizationproductId, CurrencyId, OrganizationCode);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.CurrencyOrganiztionInfo>> GetDetails_CurrencyOrganizationAsync(string OrganizationproductId, string CurrencyId, string OrganizationCode) {
            return base.Channel.GetDetails_CurrencyOrganizationAsync(OrganizationproductId, CurrencyId, OrganizationCode);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Currency_Svc.Save_CurrencyOrganizationResponse iGST.Currency_Svc.ICurrencyService.Save_CurrencyOrganization(iGST.Currency_Svc.Save_CurrencyOrganizationRequest request) {
            return base.Channel.Save_CurrencyOrganization(request);
        }
        
        public bool Save_CurrencyOrganization(bool isOnlyDelete, BusinessObjects.CurrencyOrganiztionInfo obj, BusinessObjects.UserInfo objUserInfo, out string errormsg) {
            iGST.Currency_Svc.Save_CurrencyOrganizationRequest inValue = new iGST.Currency_Svc.Save_CurrencyOrganizationRequest();
            inValue.isOnlyDelete = isOnlyDelete;
            inValue.obj = obj;
            inValue.objUserInfo = objUserInfo;
            iGST.Currency_Svc.Save_CurrencyOrganizationResponse retVal = ((iGST.Currency_Svc.ICurrencyService)(this)).Save_CurrencyOrganization(inValue);
            errormsg = retVal.errormsg;
            return retVal.Save_CurrencyOrganizationResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Currency_Svc.Save_CurrencyOrganizationResponse> Save_CurrencyOrganizationAsync(iGST.Currency_Svc.Save_CurrencyOrganizationRequest request) {
            return base.Channel.Save_CurrencyOrganizationAsync(request);
        }
    }
}
