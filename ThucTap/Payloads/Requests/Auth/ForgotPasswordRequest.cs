namespace ThucTap.Payloads.Requests.Auth
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public int CodeActive { get; set; }
        public string NewPassword { get; set; }
    }
}
