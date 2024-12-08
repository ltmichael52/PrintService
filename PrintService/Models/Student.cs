namespace PrintService.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public decimal? AccountBalance { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<PaperDetailStudent> PaperDetailStudents { get; set; } = new List<PaperDetailStudent>();

    public virtual ICollection<PrintingLog> PrintingLogs { get; set; } = new List<PrintingLog>();

    public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    public virtual ICollection<RechargeHistory> RechargeHistories { get; set; } = new List<RechargeHistory>();

    public virtual Account StudentNavigation { get; set; } = null!;
}
