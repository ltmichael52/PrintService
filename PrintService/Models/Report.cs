using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string? ReportType { get; set; }

    public DateTime? ReportDate { get; set; }

    public string? Content { get; set; }
}
