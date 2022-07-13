using System;
using System.Collections.Generic;

namespace SubscriptionPlans.SubscriptionDB
{
    public partial class SubPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public int PlanPrice { get; set; }
        public int PlanDays { get; set; }
    }
}
