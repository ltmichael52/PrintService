using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class CompileHistory
{
    public int Id { get; set; }

    public DateTime CompileDate { get; set; }

    public string? Content { get; set; }

    public string CodeLanguage { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
