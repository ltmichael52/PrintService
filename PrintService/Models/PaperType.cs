using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class PaperType
{
    public int PaperTypeId { get; set; }

    public string? PaperName { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<PaperDetailPrinter> PaperDetailPrinters { get; set; } = new List<PaperDetailPrinter>();

    public virtual ICollection<PaperDetailStudent> PaperDetailStudents { get; set; } = new List<PaperDetailStudent>();

    public virtual ICollection<PrintingLog> PrintingLogs { get; set; } = new List<PrintingLog>();

    public virtual ICollection<PurchaseHistoryDetail> PurchaseHistoryDetails { get; set; } = new List<PurchaseHistoryDetail>();
}
