using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PrintingLog
{
    public int LogId { get; set; }

    public int? StudentId { get; set; }

    public int? PrinterId { get; set; }

    public int? DocumentId { get; set; }

    public int? PaperTypeId { get; set; }

    public bool? IsColored { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? Copies { get; set; }

    public bool? IsDoubleSided { get; set; }

    public int? Status { get; set; }

    public virtual Document? Document { get; set; }

    public virtual PaperType? PaperType { get; set; }

    public virtual Printer? Printer { get; set; }

    public virtual Student? Student { get; set; }
}
