using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class CategoryInfo
    {
        public CategoryInfo()
        {
            CategoryId = "0";
            CountryId = "0";
            CountryName = "0";
            ParentCategoryId = "0";
            CategoryName = "";
            ParentCategoryName = "";
            WillCarryContent = "N";
            LastModifiedOn = System.DateTime.Now;
            LastModifiedBy = "";
            ActivityType = "A";
            DatauniqueID = "0";
            ServiceOrGoods = "";
            HSNCode = "";
            SACCode = "";
            CGST = "";
            SGST = "";
            IGST = "";
            IsActive = false;
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string WillCarryContent { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
        public string ServiceOrGoods { get; set; }
        public string HSNCode { get; set; }
        public string SACCode { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string cssclass { get; set; }
        public bool IsExpenseType { get; set; }
    }
}
