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
    public partial class RoleService : IRoleService
    {
        #region Role Related
        public List<RoleInfo> GetList_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            return wscalls.GetList_Role(RoleID, BranchId, UserID, IsActive);
        }

        public RoleInfo GetDetails_Role(string RoleID, string BranchId, string UserID, bool IsActive)
        {
            return wscalls.GetDetails_Role(RoleID, BranchId, UserID, IsActive);
        }

        public bool Save_Role(bool isOnlyDelete, RoleInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wscalls.Save_Role(isOnlyDelete, obj, objUserInfo, out errormsg);
        }

        public RoleInfo Get_Effective_Role_ForAUser(string BranchId, string UserID)
        {
            return wscalls.Get_Effective_Role_ForAUser(BranchId, UserID);
        }
        #endregion
    }
}
