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
        public List<OrderDTO> EntityToListDTO(List<Order> list)
        {
            List<OrderDTO> listDTO = new List<OrderDTO>();
            foreach (var order in list)
            {
                OrderDTO dto = new OrderDTO();
                dto.PaymentMethod = dbContext.Payment.FirstOrDefault(x => x.PaymentID == order.PaymentID).PaymentMethod;
                dto.UserName = dbContext.User.FirstOrDefault(x => x.UserID == order.UserID).UserName;
                dto.OriginalPrice = order.OriginalPrice;
                dto.ActualPrice = order.ActualPrice;
                dto.FullName = order.FullName;
                dto.Email = order.Email;
                dto.Phone = order.Phone;
                dto.Address = order.Address;
                dto.OrderName = dbContext.OrderStatus.FirstOrDefault(x => x.OrderStatusID == order.OrderStatusID).OrderName;
                dto.CreatedAt = order.CreatedAt;
                listDTO.Add(dto);
            }
            return listDTO;
                
        }
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
                OrderName = dbContext.OrderStatus.FirstOrDefault(x => x.OrderStatusID == order.OrderStatusID).OrderName,
                CreatedAt = order.CreatedAt,
            };
        }
    }
}
