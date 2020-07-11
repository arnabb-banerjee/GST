using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public class BankTransactionInfo
    {
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string Id{ get; set; }
        public string DatauniqueID{ get; set; }
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
		public string ChqNo{ get; set; }
        public string Particulars{ get; set; }
        public string Debit{ get; set; }
        public string Credit{ get; set; }
        public string Balance{ get; set; }
        public string InitBr{ get; set; }
		public string ProductIds{ get; set; }
        public string Products{ get; set; }
        public string CustomerId{ get; set; }
        public string CustomerName { get; set; }
        public string IsSellExpense { get; set; }
        public string Tax{ get; set; }
		public string OrganizationID{ get; set; }
        public string CreatedOn{ get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public string ActivityType { get; set; }
        public bool IsActive { get; set; }
    }
}
