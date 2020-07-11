using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class LanguageInfo
    {
        public string LanguageId { get; set; }
        public string LanguageName { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

    [Serializable]
    public class LanguageValueInfo : IEnumerable<LanguageValueInfo>
    {
        private IEnumerable<LanguageValueInfo> SelfList;
        public string LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string MasterIDField { get; set; }
        public string MasterFieldValue { get; set; }
        public string MasterTableName { get; set; }
        public string MasterTablePrefix { get; set; }
        public bool IsActive { get; set; }
        public string value { get; set; }

        public IEnumerator<LanguageValueInfo> GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }
    }

    [Serializable]
    public class LanguageCountryInfo
    {
        public LanguageCountryInfo()
        {
            this.Id = 0;
            this.LanguageId = "0";
            this.Proirity = 0;
            this.CountryId = "";
            this.Visibility = true;
            this.IsActive = true;
        }

        public int Id { get; set; }
        public string LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public bool Visibility { get; set; }
        public int Proirity { get; set; }
        public bool IsActive { get; set; }
    }
}
