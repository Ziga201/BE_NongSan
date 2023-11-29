using System.Numerics;
using ThucTap.Entities;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Order;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThucTap.Services.Implement
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ResponseObject<OrderDTO> responseObject;
        private readonly OrderConverter converter;

        public OrderService()
        {
            responseObject = new ResponseObject<OrderDTO>();
            converter = new OrderConverter();
        }

        public ResponseObject<OrderDTO> Order(OrderRequest orderRequest, List<OrderDetailRequest> orderDetailRequests)
        {
            if (!dbContext.Payment.Any(x => x.PaymentID == orderRequest.PaymentID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Phương thức thanh toán không tồn tại", null);
            if (!dbContext.User.Any(x => x.UserID == orderRequest.UserID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            Order order = new Order();
            order.PaymentID = orderRequest.PaymentID;
            order.UserID = orderRequest.UserID;
            order.OriginalPrice = orderRequest.OriginalPrice;
            order.ActualPrice = orderRequest.ActualPrice;
            order.FullName = orderRequest.FullName;
            order.Email = orderRequest.Email;
            order.Phone = orderRequest.Phone;
            order.Address = orderRequest.Address;
            order.OrderStatusID = 1;
            order.CreatedAt = DateTime.Now;
            order.UpdateAt = DateTime.Now;
            dbContext.Add(order);
            dbContext.SaveChanges();
            order.OrderDetails = OrderDetail(order.OrderID, orderDetailRequests);
            return responseObject.ResponseSucess("Đặt hàng thành công", converter.EntityToDTO(order));

        }
        private List<OrderDetail> OrderDetail(int orderID, List<OrderDetailRequest> orderDetailRequests)
        {
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            foreach (var request in orderDetailRequests)
            {
                
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = orderID;
                    orderDetail.ProductID = request.ProductID;
                    orderDetail.PriceTotal = request.PriceTotal;
                    orderDetail.Quantity = request.Quantity;
                    orderDetail.CreatedAt = DateTime.Now;
                    orderDetail.UpdateAt = DateTime.Now;
                    listOrderDetail.Add(orderDetail);
            }
            dbContext.AddRange(listOrderDetail);
            dbContext.SaveChanges();
            return listOrderDetail;
        }

        public double Statistic(int? month, int? quarter, int? year)
        {
            var order = dbContext.OrderDetail.ToList();
            if (quarter != null)
                switch (quarter)
                {
                    case 1:
                        order = order.Where(x => x.CreatedAt.Month >= 1 && x.CreatedAt.Month <= 3).ToList();
                        break;
                    case 2:
                        order = order.Where(x => x.CreatedAt.Month >= 4 && x.CreatedAt.Month <= 6).ToList();
                        break;
                    case 3:
                        order = order.Where(x => x.CreatedAt.Month >= 7 && x.CreatedAt.Month <= 9).ToList();
                        break;
                    case 4:
                        order = order.Where(x => x.CreatedAt.Month >= 10 && x.CreatedAt.Month <= 12).ToList();
                        break;
                    default:
                        break;
                }
            if (year != null)
                order = order.Where(x => x.CreatedAt.Year == year).ToList();
            if (month != null)
                order = order.Where(x => x.CreatedAt.Month == month).ToList();

            double total = order.Sum(x => x.PriceTotal);
            return total;
        }
    }
}
