using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public int? AccountId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int? AccountBalance { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<PrintingLog> PrintingLogs { get; set; } = new List<PrintingLog>();

    public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
}
