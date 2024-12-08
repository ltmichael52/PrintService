using System.ComponentModel.DataAnnotations;

namespace PrintService.ViewModels
{
    public class PrintConfig
    {
        public int PrinterID { get; set; }
        public string? PrinterModel { get; set; }
        public int A3Amount {  get; set; }
        public int A4Amount { get; set; }
        public string? CampusName {  get; set; }
        public string? BuildingName {  get; set; }
        public string? Room {  get; set; }

        [Required(ErrorMessage = "File is required.")]
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "PaperTypeID is required.")]
        public int PaperTypeID { get; set; }

        public bool Colored { get; set; }

        [Required(ErrorMessage = "Number of Copies is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Copies must be at least 1.")]
        public int NumberOfCopies { get; set; }

        [Required(ErrorMessage = "IsDoubledSide field is required.")]
        public bool IsDoubledSide { get; set; }


    }
}
