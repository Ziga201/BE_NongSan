namespace ThucTap.Payloads.DTOs
{
    public class OrderDetailDTO
    {
        public string NameProduct { get; set; }
        public string AvatarImageProduct { get; set; }
        public double PriceTotal { get; set; }
        public int Quantity { get; set; }
    }
}
