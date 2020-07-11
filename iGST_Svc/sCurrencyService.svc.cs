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
    public partial class CurrencyService : ICurrencyService
    {
        public List<CurrencyOrganiztionInfo> GetDetails_BUCurrencies(string OrganizationproductId, string CurrencyId, string OrganizationCode)
        {
            return wsCurrency.GetDetails_BUCurrencies(OrganizationproductId, CurrencyId, OrganizationCode);
        }

        public List<CurrencyOrganiztionInfo> GetDetails_CurrencyOrganization(string OrganizationproductId, string CurrencyId, string OrganizationCode)
        {
            return wsCurrency.GetDetails_CurrencyOrganization(OrganizationproductId, CurrencyId, OrganizationCode);
        }

        public bool Save_CurrencyOrganization(bool isOnlyDelete, CurrencyOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg)
        {
            return wsCurrency.Save_CurrencyOrganization(isOnlyDelete, obj, objUserInfo, out errormsg);
        }
    }
}
