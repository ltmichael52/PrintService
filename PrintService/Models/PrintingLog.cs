using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class PrintingLog
{
    public int LogId { get; set; }

    public string? StudentId { get; set; }

    public int? PrinterId { get; set; }

    public int? DocumentId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? PaperSize { get; set; }

    public int? PagesPrinted { get; set; }

    public int? Copies { get; set; }

    public bool? IsDoubleSided { get; set; }

    public virtual Document? Document { get; set; }

    public virtual Printer? Printer { get; set; }

    public virtual Student? Student { get; set; }
}
