using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class InvoiceInfo
    {
        public InvoiceInfo()
        {
            BranchID = "0";
            OrganizationCode = "";
            InvoiceID = "0";
            InvoiceNumber = "";
            InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            InvoiceDueDate = "";
            TotalAmount = "0";
            AmountAfterDiscount = "0";
            AmountPayable = "0";
            AmountDue = "0";
            SumAmount = "0";
            ConversionRate = "1";
            PrevConversionRate = "1";
            ChangedCurrency = "";
            IsReturned = false;
            IsCancelled = false;
            CusID = "0";
            ShipAddress = "";
            ShipCity = "";
            ShipCountry = "0";
            ShipState = "0";
            CustomerName = "";
            OrganizationName = "";
            BranchName = "";
            CusEmail = "";
            CustomerName = "";
            DatauniqueID = "";
            IsActive = true;


            InvoiceProductList = new List<InvoiceProductInfo>();
            InvoicePaymentList = new List<InvoicePaymentInfo>();
            ExpenseBreakupList = new List<ExpenseBreakupInfo>();
        }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDueDate { get; set; }
        public string TotalAmount { get; set; }
        public string SumAmount { get; set; }
        public string AmountAfterDiscount { get; set; }
        public string AmountPayable { get; set; }
        public string AmountDue { get; set; }

        /*Values from child table*/
        public string AmountIncludeTax { get; set; }
        public string TaxOnProduct { get; set; }
        public string AmountExcludeTax { get; set; }

        /*Values from child table*/

        public string ActivityType { get; set; }
        public string DatauniqueID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsReturned { get; set; }
        public string ReturnedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public string CusID { get; set; }
        public string CustomerName { get; set; }
        public string CusAddress { get; set; }
        public string CusCity { get; set; }
        public string CusCountry { get; set; }
        public string CusEmail { get; set; }
        public string CusState { get; set; }

        public string SupID { get; set; }
        public string SupplierName { get; set; }
        public string SupAddress { get; set; }
        public string SupCity { get; set; }
        public string SupCountry { get; set; }
        public string SupEmail { get; set; }
        public string SupState { get; set; }

        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipCountry { get; set; }

        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCountry { get; set; }
        public string BranchState { get; set; }

        public string ProductIDs { get; set; }
        public string Quantities { get; set; }
        public string Prices { get; set; }
        public string Taxes { get; set; }
        // Start: Added on 15/10/2019. When expenses occured for travelling
        public string TravellingExpenses { get; set; }
        // End: Added on 15/10/2019. When expenses occured for travelling
        public string ChangedCurrency { get; set; }
        public string ConversionRate { get; set; }
        public string PrevConversionRate { get; set; }

        public List<InvoiceProductInfo> InvoiceProductList { get; set; }
        public List<InvoicePaymentInfo> InvoicePaymentList { get; set; }

        // Start: Added on 15/10/2019. When expenses occured for travelling
        public List<ExpenseBreakupInfo> ExpenseBreakupList { get; set; }
        // End: Added on 15/10/2019. When expenses occured for travelling
    }

    public class InvoiceProductInfo
    {
        public InvoiceProductInfo()
        {
            InvoiceID = "0.0";
            PricePerUnit = "0.0";
            Quantity = "0.0";
            TotalAmount = "0.0";
            TaxOnProduct = "0.0";
            TotalAmountIncludeTax = "0.0";
        }
        public string InvoiceProductID { get; set; }
        public string InvoiceID { get; set; }
        public string DataUniqueID { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ServiceGood { get; set; }
        public string SACHCNCode { get; set; }
        public string PricePerUnit { get; set; }
        public string Quantity { get; set; }
        public string TotalAmount { get; set; }
        public string TaxOnProduct { get; set; }
        public string TotalAmountIncludeTax { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CategoryId { get; set; }
        public bool isBreakupNeed { get; set; }
    }

    public class InvoicePaymentInfo
    {
        public string InvoiceID { get; set; }
        public string DataUniqueID { get; set; }
        public string PaymentID { get; set; }
        public string PaidAmount { get; set; }
        public string PaidOn { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
    
    // Start: Added on 15/10/2019. When expenses occured for travelling
    public class ExpenseBreakupInfo
    {
        public string BreakupId { get; set; }
        public string InvoiceProductID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Remarks { get; set; }
    }
    // End: Added on 15/10/2019. When expenses occured for travelling
}
