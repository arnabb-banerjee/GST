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
    public interface ICurrencyService
    {
        [OperationContract]
        List<CurrencyOrganiztionInfo> GetDetails_BUCurrencies(string OrganizationproductId, string CurrencyId, string OrganizationCode);

        [OperationContract]
        List<CurrencyOrganiztionInfo> GetDetails_CurrencyOrganization(string OrganizationproductId, string CurrencyId, string OrganizationCode);

        [OperationContract]
        bool Save_CurrencyOrganization(bool isOnlyDelete, CurrencyOrganiztionInfo obj, UserInfo objUserInfo, out string errormsg);
    }
}
