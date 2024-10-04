using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class EfmigrationHistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
