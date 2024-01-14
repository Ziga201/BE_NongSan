namespace ThucTap.Payloads.Requests.Cart
{
    public class HandleQuantityRequest
    {
        public int Number { get; set; }
        public int ProductID { get; set; }
        public int AccountID { get; set; }
    }
}
