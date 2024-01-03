namespace ThucTap.Entities
{
    public class ProductReview
    {
        public int ProductReviewID { get; set; }
        public int ProductID { get; set; }
        public int AccountID { get; set; }
        public string ContentRated { get; set; }
        public int PointEvaluation { get; set; }
        public string ContentSeen { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Product Product { get; set; }
        public Account Account { get; set; }
    }
}
