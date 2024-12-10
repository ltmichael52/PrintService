using Microsoft.Identity.Client;

namespace PrintService.ViewModels
{
    public class PrintingLogViewModel
    {
        public int Id { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public bool? IsColored { get; set; }
        public bool? IsDoubled {  get; set; }
        public int? NumberOfCopies {  get; set; }
        public int? Status { get; set; }
        public string? PaperTypeName { get; set; }
        public string? DocumentName { get; set; }
       

    }
}
