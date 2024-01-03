using System;

namespace ThucTap.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int PaymentID { get; set; }
        public int AccountID { get; set; }
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int OrderStatusID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Payment Payment { get; set; }
        public Account Account { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
