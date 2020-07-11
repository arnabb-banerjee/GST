﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iGST.Role_Svc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Role_Svc.IRoleService")]
    public interface IRoleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetList_Role", ReplyAction="http://tempuri.org/IRoleService/GetList_RoleResponse")]
        System.Collections.Generic.List<BusinessObjects.RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetList_Role", ReplyAction="http://tempuri.org/IRoleService/GetList_RoleResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.RoleInfo>> GetList_RoleAsync(string RoleID, string BranchId, string UserID, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetDetails_Role", ReplyAction="http://tempuri.org/IRoleService/GetDetails_RoleResponse")]
        BusinessObjects.RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/GetDetails_Role", ReplyAction="http://tempuri.org/IRoleService/GetDetails_RoleResponse")]
        System.Threading.Tasks.Task<BusinessObjects.RoleInfo> GetDetails_RoleAsync(string RoleID, string BranchId, string UserID, bool IsActive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/Save_Role", ReplyAction="http://tempuri.org/IRoleService/Save_RoleResponse")]
        iGST.Role_Svc.Save_RoleResponse Save_Role(iGST.Role_Svc.Save_RoleRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/Save_Role", ReplyAction="http://tempuri.org/IRoleService/Save_RoleResponse")]
        System.Threading.Tasks.Task<iGST.Role_Svc.Save_RoleResponse> Save_RoleAsync(iGST.Role_Svc.Save_RoleRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/Get_Effective_Role_ForAUser", ReplyAction="http://tempuri.org/IRoleService/Get_Effective_Role_ForAUserResponse")]
        BusinessObjects.RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRoleService/Get_Effective_Role_ForAUser", ReplyAction="http://tempuri.org/IRoleService/Get_Effective_Role_ForAUserResponse")]
        System.Threading.Tasks.Task<BusinessObjects.RoleInfo> Get_Effective_Role_ForAUserAsync(string BranchId, string UserID);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_Role", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_RoleRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool isOnlyDelete;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public BusinessObjects.RoleInfo obj;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public BusinessObjects.UserInfo objUserInfo;
        
        public Save_RoleRequest() {
        }
        
        public Save_RoleRequest(bool isOnlyDelete, BusinessObjects.RoleInfo obj, BusinessObjects.UserInfo objUserInfo) {
            this.isOnlyDelete = isOnlyDelete;
            this.obj = obj;
            this.objUserInfo = objUserInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Save_RoleResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class Save_RoleResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Save_RoleResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string errormsg;
        
        public Save_RoleResponse() {
        }
        
        public Save_RoleResponse(bool Save_RoleResult, string errormsg) {
            this.Save_RoleResult = Save_RoleResult;
            this.errormsg = errormsg;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoleServiceChannel : iGST.Role_Svc.IRoleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoleServiceClient : System.ServiceModel.ClientBase<iGST.Role_Svc.IRoleService>, iGST.Role_Svc.IRoleService {
        
        public RoleServiceClient() {
        }
        
        public RoleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<BusinessObjects.RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive) {
            return base.Channel.GetList_Role(RoleID, BranchId, UserID, IsActive);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<BusinessObjects.RoleInfo>> GetList_RoleAsync(string RoleID, string BranchId, string UserID, bool IsActive) {
            return base.Channel.GetList_RoleAsync(RoleID, BranchId, UserID, IsActive);
        }
        
        public BusinessObjects.RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive) {
            return base.Channel.GetDetails_Role(RoleID, BranchId, UserID, IsActive);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.RoleInfo> GetDetails_RoleAsync(string RoleID, string BranchId, string UserID, bool IsActive) {
            return base.Channel.GetDetails_RoleAsync(RoleID, BranchId, UserID, IsActive);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iGST.Role_Svc.Save_RoleResponse iGST.Role_Svc.IRoleService.Save_Role(iGST.Role_Svc.Save_RoleRequest request) {
            return base.Channel.Save_Role(request);
        }
        
        public bool Save_Role(bool isOnlyDelete, BusinessObjects.RoleInfo obj, BusinessObjects.UserInfo objUserInfo, out string errormsg) {
            iGST.Role_Svc.Save_RoleRequest inValue = new iGST.Role_Svc.Save_RoleRequest();
            inValue.isOnlyDelete = isOnlyDelete;
            inValue.obj = obj;
            inValue.objUserInfo = objUserInfo;
            iGST.Role_Svc.Save_RoleResponse retVal = ((iGST.Role_Svc.IRoleService)(this)).Save_Role(inValue);
            errormsg = retVal.errormsg;
            return retVal.Save_RoleResult;
        }
        
        public System.Threading.Tasks.Task<iGST.Role_Svc.Save_RoleResponse> Save_RoleAsync(iGST.Role_Svc.Save_RoleRequest request) {
            return base.Channel.Save_RoleAsync(request);
        }
        
        public BusinessObjects.RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID) {
            return base.Channel.Get_Effective_Role_ForAUser(BranchId, UserID);
        }
        
        public System.Threading.Tasks.Task<BusinessObjects.RoleInfo> Get_Effective_Role_ForAUserAsync(string BranchId, string UserID) {
            return base.Channel.Get_Effective_Role_ForAUserAsync(BranchId, UserID);
        }
    }
}
