using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class BankInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string BankID { get; set; }
        public string Name { get; set; }
        public string CorpID { get; set; }
        public string Address { get; set; }
        public string IFSCCode { get; set; }
        public string MCRCode { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }

    }
}
