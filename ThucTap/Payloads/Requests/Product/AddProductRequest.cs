namespace ThucTap.Payloads.Requests.Product
{
    public class AddProductRequest
    {
        public int ProductTypeID { get; set; }
        public string NameProduct { get; set; }
        public int Price { get; set; }
        public IFormFile? AvatarImageProduct { get; set; }
        public string? Describe { get; set; }
        public string? Status { get; set; }
        public int? Discount { get; set; } = 0;
        public int? Quantity { get; set; } = 0;

    }
}
