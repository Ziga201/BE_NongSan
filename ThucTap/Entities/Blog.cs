namespace ThucTap.Entities
{
    public class Blog
    {
        public int BlogID { get; set; }
        public int BlogTypeID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public int? View { get; set; }
        public DateTime CreateAt { get; set; }
        public BlogType BlogType { get; set; }
        public Account Account { get; set; }
    }
}
