using System.Numerics;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThucTap.Payloads.Converters
{
    public class OrderConverter : BaseService
    {
        public OrderDTO EntityToDTO(Order order)
        {
            return new OrderDTO()
            {
                PaymentMethod = dbContext.Payment.FirstOrDefault(x => x.PaymentID == order.PaymentID).PaymentMethod,
                UserName = dbContext.User.FirstOrDefault(x => x.UserID == order.UserID).UserName,
                OriginalPrice = order.OriginalPrice,
                ActualPrice = order.ActualPrice,
                FullName = order.FullName,
                Email = order.Email,
                Phone = order.Phone,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
            };
        }
    }
}
