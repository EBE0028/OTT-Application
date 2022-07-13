using System;
using System.Collections.Generic;

namespace CustomerAPI.CustomerDB
{
    public partial class Customer
    {
        public int CustId { get; set; }
        public string CustName { get; set; } = null!;
        public string CustEmail { get; set; } = null!;
        public string CustPassword { get; set; } = null!;
        public int CustPlan { get; set; }
        public DateTime PayDate { get; set; }
    }
}
