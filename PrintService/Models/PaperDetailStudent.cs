using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PaperDetailStudent
{
    public string StudentId { get; set; } = null!;

    public int PaperTypeId { get; set; }

    public string? Amount { get; set; }

    public virtual PaperType PaperType { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
