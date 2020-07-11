using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class FunctionInfo
    {
        public string FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string IsForModerate { get; set; }
        public string IsForMembership { get; set; }
        public string IsDesignation { get; set; }
        public string Roles { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
        public bool isSelected { get; set; }
        public bool IsDefaultForRegisteredUser { get; set; }
        public bool IsDefaultForModerateUser { get; set; }
        public List<RoleInfo> RoleList { get; set; }
    }
}
