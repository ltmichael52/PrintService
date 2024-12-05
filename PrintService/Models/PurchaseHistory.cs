using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class PurchaseHistory
{
    public int PurchaseId { get; set; }

    public string StudentId { get; set; } = null!;

    public DateTime? PurchasedDate { get; set; }

    public decimal? TotalPurchased { get; set; }

    public virtual ICollection<PurchaseHistoryDetail> PurchaseHistoryDetails { get; set; } = new List<PurchaseHistoryDetail>();

    public virtual Student Student { get; set; } = null!;
}
