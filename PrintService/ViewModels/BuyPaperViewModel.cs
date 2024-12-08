namespace PrintService.ViewModels
{
    public class BuyPaperViewModel
    {
        public int A3Quantity { get; set; }
        public int A4Quantity { get; set; }
        public decimal A3Price { get; set; }
        public decimal A4Price { get; set; }
        public decimal TotalAmount { get; set; }
        public int A3Balance { get; set; }
        public int A4Balance { get; set; }
        public decimal AccountBalance { get; internal set; }
    }

}
