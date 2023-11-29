namespace ThucTap.Payloads.Requests.Order
{
    public class OrderDetailRequest
    {
        public int ProductID { get; set; }
        public double PriceTotal { get; set; }
        public int Quantity { get; set; }
    }
}
