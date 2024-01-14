namespace ThucTap.Payloads.Requests.Product
{
    public class UpdateProductRequest
    {
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public IFormFile? AvatarImageProduct { get; set; }
        public string? Status { get; set; }
        public string? Describe { get; set; }
        public int? Discount { get; set; }
        public int? Quantity { get; set; }

    }
}
