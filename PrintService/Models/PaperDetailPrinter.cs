using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PaperDetailPrinter
{
    public int PrinterId { get; set; }

    public int PaperTypeId { get; set; }

    public int Amount { get; set; }

    public virtual PaperType PaperType { get; set; } = null!;

    public virtual Printer Printer { get; set; } = null!;
}
