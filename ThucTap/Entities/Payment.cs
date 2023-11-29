namespace ThucTap.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
