using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int TypeAccount { get; set; }

    public virtual Student? Student { get; set; }
}
