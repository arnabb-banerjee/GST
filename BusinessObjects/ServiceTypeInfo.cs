using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class ServiceTypeInfo
    {
        public string ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

}
