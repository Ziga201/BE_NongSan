namespace ThucTap.Payloads.Requests
{
    public class CreateNewPasswordRequest
    {
        public int AccountID { get; set; }
        public int CodeActive { get; set; }
        public string NewPassword { get; set; }
    }
}
