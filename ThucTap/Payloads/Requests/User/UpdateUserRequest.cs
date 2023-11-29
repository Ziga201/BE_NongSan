namespace ThucTap.Payloads.Requests
{
    public class UpdateUserRequest
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int AccountID { get; set; }
    }
}
