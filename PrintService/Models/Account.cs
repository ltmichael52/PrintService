using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int TypeAccount { get; set; }

    public virtual Student? Student { get; set; }
}
