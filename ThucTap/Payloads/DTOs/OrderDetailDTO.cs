namespace ThucTap.Payloads.DTOs
{
    public class OrderDetailDTO
    {
        public int ProductID { get; set; }
        public string NameProduct { get; set; }
        public string AvatarImageProduct { get; set; }
        public double PriceTotal { get; set; }
        public int Quantity { get; set; }
    }
}
