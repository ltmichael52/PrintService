using System;
using System.Collections.Generic;

namespace PrintService.Models;

public partial class Printer
{
    public int PrinterId { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Description { get; set; }

    public string? CampusName { get; set; }

    public string? BuildingName { get; set; }

    public string? RoomNumber { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<PrintingLog> PrintingLogs { get; set; } = new List<PrintingLog>();
}
