using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class PurchaseHistoryDetail
{
    public int PurchaseId { get; set; }

    public int PaperTypeId { get; set; }

    public int PaperPurchased { get; set; }

    public decimal PurchasedByType { get; set; }

    public virtual PaperType PaperType { get; set; } = null!;

    public virtual PurchaseHistory Purchase { get; set; } = null!;
}
