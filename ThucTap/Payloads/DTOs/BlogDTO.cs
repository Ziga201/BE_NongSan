namespace ThucTap.Payloads.DTOs
{
    public class BlogDTO
    {
        public int BlogID { get; set; }
        public int AccountID { get; set; }
        public int BlogTypeID { get; set; }
        public string BlogTypeName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int? View { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
