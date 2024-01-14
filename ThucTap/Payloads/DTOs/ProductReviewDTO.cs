namespace ThucTap.Payloads.DTOs
{
    public class ProductReviewDTO
    {
        public int ProductID { get; set; }
        public string NameProduct { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public int PointEvaluation { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
