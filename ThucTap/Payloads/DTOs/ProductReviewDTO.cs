namespace ThucTap.Payloads.DTOs
{
    public class ProductReviewDTO
    {
        public string NameProduct { get; set; }
        public string UserName { get; set; }
        public string ContentRated { get; set; }
        public int PointEvaluation { get; set; }
        public string ContentSeen { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
