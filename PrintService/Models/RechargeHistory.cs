namespace PrintService.Models
{
    public partial class RechargeHistory
    {
        public int RechargeId { get; set; }

        public string StudentId { get; set; } = null!;

        public decimal Amonut { get; set; }

        public string RechargeMethod { get; set; } = null!;

        public DateTime RechargedDate { get; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
