using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class SettingsInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string InfoType { get; set; }
        #region Currency
        public bool mc_isAllowedMutyCurrency { get; set; }
        public string mc_CurrencyList { get; set; }
        #endregion

        #region Payment
        public bool p_isAllowedOnlinePayment { get; set; }
        public string p_BankAccountNumber { get; set; }
        public string p_BankAccountHolder { get; set; }
        public string p_BankAccountIFSCCode { get; set; }
        public string p_BankAccountIMCRCode { get; set; }
        public string p_BankAccountIBranchName { get; set; }
        public string p_BankAccountIBankName { get; set; }
        public string p_PaypalAccountID { get; set; }
        #endregion

        #region Company
        public bool c_isAllowedMultyLanguage { get; set; }
        public string c_CompanyName { get; set; }
        public string c_Email { get; set; }
        public string c_Mobile { get; set; }
        public string c_Address { get; set; }
        public string c_City { get; set; }
        public string c_State { get; set; }
        public string c_Country { get; set; }
        public string c_Website { get; set; }
        public string c_CIN { get; set; }
        public string c_PAN { get; set; }
        public string c_DefaultEmail { get; set; }
        public string c_SMTP { get; set; }

        #endregion

        #region Moderate section
        public bool an_isAllowedAlert_GSTDate { get; set; }
        public bool an_isAllowedAlert_PaidMembership { get; set; }
        public string an_AlertText_GSTDate { get; set; }
        public string an_AlertText_PaidMembership { get; set; }
        #endregion

        
    }

    [Serializable]
    public class CurrencyOrganiztionInfo : IEnumerable<CurrencyOrganiztionInfo>
    {
        private IEnumerable<CurrencyOrganiztionInfo> SelfList;

        public int OrganizationproductId { get; set; }
        public string CurrencyId { get; set; }
        public string OrganizationCode { get; set; }
        public string Symbol { get; set; }
        public bool isSelected { get; set; }
        public string CurrencyName { get; set; }
        // Modified on [30th August 2019] by [Partha]
        public IEnumerator<CurrencyOrganiztionInfo> GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }
    }


}

