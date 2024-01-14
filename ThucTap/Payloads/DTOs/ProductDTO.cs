namespace ThucTap.Payloads.DTOs
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string NameProductType { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public string? AvatarImageProduct { get; set; }
        public string? Describe { get; set; }
        public int? Discount { get; set; }
        public string? Status { get; set; }
        public int? Quantity { get; set; }
        public int? Purchases { get; set; }
    }
}
