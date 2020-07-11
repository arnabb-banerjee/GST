using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class RoleInfo
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }

        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
        public bool IsOnlyForModerateUsers { get; set; }
        public bool isForMembershipView { get; set; }
        public bool isSelected { get; set; } /*Required for Funcion page*/

        public bool Category_Audit { get; set; }
        public bool Category_View { get; set; }
        public bool Category_Add { get; set; }
        public bool Category_Edit { get; set; }
        public bool Category_Delete { get; set; }

        public bool Product_Audit { get; set; }
        public bool Product_View { get; set; }
        public bool Product_Add { get; set; }
        public bool Product_Edit { get; set; }
        public bool Product_Delete { get; set; }

        public bool Role_Audit { get; set; }
        public bool Role_View { get; set; }
        public bool Role_Add { get; set; }
        public bool Role_Edit { get; set; }
        public bool Role_Delete { get; set; }

        public bool Function_Audit { get; set; }
        public bool Function_View { get; set; }
        public bool Function_Add { get; set; }
        public bool Function_Edit { get; set; }
        public bool Function_Delete { get; set; }

        public bool UserRegistered_Audit { get; set; }
        public bool UserRegistered_View { get; set; }
        public bool UserRegistered_Add { get; set; }
        public bool UserRegistered_Edit { get; set; }
        public bool UserRegistered_Delete { get; set; }

        public bool UserModerate_Audit { get; set; }
        public bool UserModerate_View { get; set; }
        public bool UserModerate_Add { get; set; }
        public bool UserModerate_Edit { get; set; }
        public bool UserModerate_Delete { get; set; }

        public bool Organization_Audit { get; set; }
        public bool Organization_View { get; set; }
        public bool Organization_Add { get; set; }
        public bool Organization_Edit { get; set; }
        public bool Organization_Delete { get; set; }

        public bool Branch_Audit { get; set; }
        public bool Branch_View { get; set; }
        public bool Branch_Add { get; set; }
        public bool Branch_Edit { get; set; }
        public bool Branch_Delete { get; set; }

        public bool Customer_Audit { get; set; }
        public bool Customer_View { get; set; }
        public bool Customer_Add { get; set; }
        public bool Customer_Edit { get; set; }
        public bool Customer_Delete { get; set; }

        public bool Bill_Audit { get; set; }
        public bool Bill_View { get; set; }
        public bool Bill_Add { get; set; }
        public bool Bill_Edit { get; set; }
        public bool Bill_Delete { get; set; }

        public bool Language_Audit { get; set; }
        public bool Language_View { get; set; }
        public bool Language_Add { get; set; }
        public bool Language_Edit { get; set; }
        public bool Language_Delete { get; set; }

        public bool DefineLanguage_Audit { get; set; }
        public bool DefineLanguage_View { get; set; }
        public bool DefineLanguage_Add { get; set; }
        public bool DefineLanguage_Edit { get; set; }
        public bool DefineLanguage_Delete { get; set; }

        public bool StaticValue_Audit { get; set; }
        public bool StaticValue_View { get; set; }
        public bool StaticValue_Add { get; set; }
        public bool StaticValue_Edit { get; set; }
        public bool StaticValue_Delete { get; set; }

        public bool Terms_Audit { get; set; }
        public bool Terms_View { get; set; }
        public bool Terms_Add { get; set; }
        public bool Terms_Edit { get; set; }
        public bool Terms_Delete { get; set; }
    }
}
