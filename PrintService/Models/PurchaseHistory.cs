using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class PurchaseHistory
{
    public int PurchaseId { get; set; }

    public string? StudentId { get; set; }

    public int? PagesPurchased { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual Student? Student { get; set; }
}
