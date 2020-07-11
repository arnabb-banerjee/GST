using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class TaxMasterInfo
    {
        public string TaxDefinationID { get; set; }
        public string TaxName { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

    [Serializable]
    public class TaxCountryMapInfo
    {
        public string TaxDefinationID { get; set; }
        public string TaxName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string ActivityType { get; set; }
        public string ApplicableType { get; set; }
        public string DatauniqueID { get; set; }
        public bool IsExist { get; set; }
    }

    [Serializable]
    public class TaxCountryCategoryMapInfo
    {
        public string TaxDefinationID { get; set; }
        public string TaxName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Percentage { get; set; }
        public string ActivityType { get; set; }
        public string ApplicableType { get; set; }
        public string DatauniqueID { get; set; }
        public bool IsExist { get; set; }
    }

    [Serializable]
    public class ApplicableType
    {
        public ApplicableType(string value, string text) { Value = value; Text = text; }
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
