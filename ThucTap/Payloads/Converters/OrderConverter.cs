using MailKit.Search;
using Org.BouncyCastle.Asn1.X509;
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
                OrderID = order.OrderID,
                PaymentMethod = dbContext.Payment.FirstOrDefault(x => x.PaymentID == order.PaymentID).PaymentMethod,
                UserName = dbContext.Account.FirstOrDefault(x => x.AccountID == order.AccountID).UserName,
                OriginalPrice = order.OriginalPrice,
                ActualPrice = order.ActualPrice,
                FullName = order.FullName,
                Email = order.Email,
                Phone = order.Phone,
                Address = order.Address,
                OrderName = dbContext.OrderStatus.FirstOrDefault(x => x.OrderStatusID == order.OrderStatusID).OrderName,
                CreatedAt = order.CreatedAt,
            };
        }
    }
}
