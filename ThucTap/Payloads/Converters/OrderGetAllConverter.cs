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
                UserName = dbContext.Account.FirstOrDefault(x => x.AccountID == order.AccountID).UserName,
                TotalPrice = order.TotalPrice,
                FullName = order.FullName,
                Phone = order.Phone,
                Address = order.Address,
                OrderStatusID = order.OrderStatusID,
                OrderName = dbContext.OrderStatus.FirstOrDefault(x => x.OrderStatusID == order.OrderStatusID).OrderName,
                CreatedAt = order.CreatedAt,
                OrderDetailDTOs = dbContext.OrderDetail.Where(x => x.OrderID == order.OrderID).Select(x => converter.EntityToDTO(x)).ToList(),
            };
        }
    }
}
