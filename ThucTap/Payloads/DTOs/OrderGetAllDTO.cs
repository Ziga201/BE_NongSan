using ThucTap.Entities;

namespace ThucTap.Payloads.DTOs
{
    public class OrderGetAllDTO
    {
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public string UserName { get; set; }
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string OrderName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailDTO> OrderDetailDTOs { get; set; }
    }
}
