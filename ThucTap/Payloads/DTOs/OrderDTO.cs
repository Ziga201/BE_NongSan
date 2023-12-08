namespace ThucTap.Payloads.DTOs
{
    public class OrderDTO
    {
        public string PaymentMethod { get; set; }
        public string UserName { get; set; }
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
