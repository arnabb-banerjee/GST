using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class ProductInfo
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string ProductName { get; set; }
        public bool IsExpense { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public string ServiceOrGoods { get; set; }
        public string HSNCode { get; set; }
        public string SACCode { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
        public string DatauniqueID { get; set; }
    }

    [Serializable]
    public class ProductOrganiztionInfo : IEnumerable<ProductOrganiztionInfo>
    {
        private IEnumerable<ProductOrganiztionInfo> SelfList;

        public int OrganizationproductId { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string HSNSACCode { get; set; }
        public string ServiceOrGoods { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string IsActive { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string TotalSold { get; set; }
        public string TotalAvailable { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public int Unit { get; set; }
        public int Class { get; set; }
        public double AbatementPercentage { get; set; }
        public int ServiceType { get; set; }
        public double SalePrice { get; set; }
        public bool isInclusiveTax { get; set; }
        public double AvailableQty { get; set; }
        public string IncomeAccount { get; set; }
        public int SupplierId { get; set; }
        public int PreferredSupplierId { get; set; }
        public double ReverseCharge { get; set; }
        public double PurchaseTax { get; set; }
        public double SaleTax { get; set; }
        public bool isSelected { get; set; }
        public string ImageId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Byte[] FileData { get; set; }
        // Modified on [30th August 2019] by [Partha] cause [Adding isExpense parameter]
        public string isExpense { get; set; }
        // Modified on [30th August 2019] by [Partha]
        public IEnumerator<ProductOrganiztionInfo> GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SelfList.GetEnumerator();
        }
    }

    [Serializable]
    //public class ProductOrganiztionImageInfo : IEnumerable<ProductOrganiztionImageInfo>
    public class ProductOrganiztionImageInfo 
    {
        //private IEnumerable<ProductOrganiztionImageInfo> SelfList;

        public int ImageId { get; set; }
        public int OrganizationproductId { get; set; }
        public string ProductId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int Seq { get; set; }
        public string Ismain { get; set; }
        public string IsActive { get; set; }

        //public IEnumerator<ProductOrganiztionImageInfo> GetEnumerator()
        //{
        //    return SelfList.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return SelfList.GetEnumerator();
        //}
    }

}

