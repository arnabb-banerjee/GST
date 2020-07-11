using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class OrganizationDashBoardInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public int Months { get; set; }
        public int NoOfInvoice { get; set; }
        public double PricePerUnit { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTax { get; set; }
        public double AmountIncludeTax { get; set; }
        public string Type { get; set; }
    }
}