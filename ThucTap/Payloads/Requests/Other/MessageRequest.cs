namespace ThucTap.Payloads.Requests.Other
{
    public class MessageRequest
    {
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
    }
}
