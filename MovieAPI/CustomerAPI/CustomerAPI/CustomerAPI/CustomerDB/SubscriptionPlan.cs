using System;
using System.Collections.Generic;

namespace CustomerAPI.CustomerDB
{
    public partial class SubscriptionPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public int PlanDays { get; set; }
        public int PlanPrice { get; set; }
    }
}
