using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class OrganizationInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
        public List<BranchInfo> BranchList { get; set; }
    }

    [Serializable]
    public class OrganizationAccountantInfo : IEnumerable<OrganizationAccountantInfo>
    {
        private IEnumerable<OrganizationAccountantInfo> SelfList;
        public string ID { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string AccountantCode { get; set; }
        public string AccountantName { get; set; }
        public string isSelected { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }

        public IEnumerator<OrganizationAccountantInfo> GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }
    }
}
