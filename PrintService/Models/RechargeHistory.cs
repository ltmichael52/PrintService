using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class RechargeHistory
{
    public string RechargeId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public decimal Amonut { get; set; }

    public string RechargeMethod { get; set; } = null!;

    public DateTime RechargedDate { get; set; }

    public virtual Student Student { get; set; } = null!;
}
