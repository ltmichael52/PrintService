namespace PrintService.Payments
{
    public class PaymentRequest
    {
        public string? OrderType { get; set; }
        public double Amount { get; set; }
        public string? OrderDescription { get; set; }
        public string? Name { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
