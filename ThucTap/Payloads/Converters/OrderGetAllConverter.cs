using Microsoft.EntityFrameworkCore;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;

namespace ThucTap.Payloads.Converters
{
    public class OrderGetAllConverter:BaseService
    {
        private readonly OrderDetailConverter converter;

        public OrderGetAllConverter()
        {
            converter = new OrderDetailConverter();
        }

        public OrderGetAllDTO EntityToDTO(Order order)
        {
            return new OrderGetAllDTO()
            {
                OrderID = order.OrderID,
                PaymentMethod = dbContext.Payment.FirstOrDefault(x => x.PaymentID == order.PaymentID).PaymentMethod,
                UserName = dbContext.User.FirstOrDefault(x => x.UserID == order.UserID).UserName,
                OriginalPrice = order.OriginalPrice,
                ActualPrice = order.ActualPrice,
                FullName = order.FullName,
                Phone = order.Phone,
                Address = order.Address,
                OrderName = dbContext.OrderStatus.FirstOrDefault(x => x.OrderStatusID == order.OrderStatusID).OrderName,
                CreatedAt = order.CreatedAt,
                OrderDetailDTOs = dbContext.OrderDetail.Where(x => x.OrderID == order.OrderID).Select(x => converter.EntityToDTO(x)).ToList(),
            };
        }
    }
}
