using System.ComponentModel;

namespace OTTUI.Models
{
    public class Customer
    {
        [DisplayName("Customer ID")]
        public int CustId { get; set; }
        [DisplayName("Customer Name")]
        public string CustName { get; set; }
        [DisplayName("Customer Email-ID")]
        
        public string CustEmail { get; set; }
        [DisplayName("Customer Password")]
        public string CustPassword { get; set; }
        [DisplayName("Customer Plan")]
        public int CustPlan { get; set; }
        [DisplayName("Customer Payment Date")]
        public DateTime PayDate { get; set; }
    }
}
