using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Spsoconfiguration
{
    public int ConfigId { get; set; }

    public int? DefaultPagesPerSemester { get; set; }

    public string? DefaultPaperSize { get; set; }

    public string? PermittedFileTypes { get; set; }

    public DateTime? NextDefaultPagesDate { get; set; }

    public DateTime? DateApply { get; set; }
}
