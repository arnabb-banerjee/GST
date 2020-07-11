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
    public interface IRoleService
    {
        #region Role Related
        [OperationContract]
        List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive);

        [OperationContract]
        RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive);

        [OperationContract]
        bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg);

        [OperationContract]
        RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID);
        #endregion
    }
}
