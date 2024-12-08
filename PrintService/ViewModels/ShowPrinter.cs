namespace PrintService.ViewModels
{
    public class ShowPrinter
    {
        public int PrinterId { get; set; }
        public string? PrinterModel { get; set; }
        public string? CampusName {  get; set; }
        public string? BuildingName { get; set; }
        public string? RoomName { get; set; }
        public bool? IsActive { get; set; }
        public int AmountInQueue {  get; set; }
        public int A3Amount {  get; set; }
        public int A4Amount { get; set; }
    }
}
