using ThucTap.Entities;

namespace ThucTap.Payloads.Requests.Order
{
    public class OrderRequest
    {
        public int PaymentID { get; set; }
        public int AccountID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
