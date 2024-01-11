namespace ThucTap.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public Account Account { get; set; }
    }
}
