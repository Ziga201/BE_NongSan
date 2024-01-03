using Azure;
using CloudinaryDotNet;
using System;
using System.Numerics;
using ThucTap.Entities;
using ThucTap.Handle.Payment;
using ThucTap.Handle.Send;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Order;
using ThucTap.Payloads.Responses;
using ThucTap.Services.IServices;

namespace ThucTap.Services.Implement
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ResponseObject<OrderDTO> responseObject;
        private readonly OrderConverter converter;
        private readonly OrderDetailConverter orderDetailConverter;
        private readonly OrderGetAllConverter orderGetAllConverter;
        private readonly Utils utils;

        public OrderService()
        {
            responseObject = new ResponseObject<OrderDTO>();
            converter = new OrderConverter();
            orderDetailConverter = new OrderDetailConverter();
            orderGetAllConverter = new OrderGetAllConverter();
            utils = new Utils();
        }

        public ResponseObject<OrderDTO> Order(OrderRequest orderRequest, List<OrderDetailRequest> orderDetailRequests)
        {
            if (!dbContext.Payment.Any(x => x.PaymentID == orderRequest.PaymentID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Phương thức thanh toán không tồn tại", null);
            if (!dbContext.Account.Any(x => x.AccountID == orderRequest.AccountID))
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
            Order order = new Order();
            order.PaymentID = orderRequest.PaymentID;
            order.AccountID = orderRequest.AccountID;

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

            double totalAmount = 0;
            foreach (var item in order.OrderDetails)
            {
                totalAmount += item.PriceTotal;
            }

            var orderUpdate = dbContext.Order.FirstOrDefault(x => x.OrderID == order.OrderID);

            orderUpdate.OriginalPrice = totalAmount;
            orderUpdate.ActualPrice = totalAmount;
            dbContext.Update(orderUpdate);
            dbContext.SaveChanges();

            string listName = "";
            order.OrderDetails.ForEach(x =>
            {
                listName += $"{dbContext.Product.FirstOrDefault(y => y.ProductID == x.ProductID).NameProduct} x {x.Quantity}";
            });


            SendMail.Send(new MailContent
            {
                MailTo = order.Email,
                Subject = $"DongAnh Shop đã nhận đơn hàng #{order.OrderID}",
                Content = $"<body style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;\">\r\n\r\n    <div style=\"max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">\r\n        <h1 style=\"color: #333333;\">Đặt Hàng Thành Công!</h1>\r\n        <p style=\"color: #555555;\">Cảm ơn bạn đã đặt hàng. Đơn hàng của bạn đã được xác nhận và đang được xử lý.</p>\r\n        <p style=\"color: #555555;\">Thông tin đơn hàng:</p>\r\n        <ul>\r\n            <li><strong>Sản phẩm:</strong> {listName}</li>\r\n            <li><strong>Phương thức thanh toán: </strong>{dbContext.Payment.FirstOrDefault(x => x.PaymentID == order.PaymentID).PaymentMethod} </li>\r\n            <li><strong>Tổng cộng:</strong> {totalAmount} VND</li>\r\n        </ul>\r\n        <p style=\"color: #555555;\">Cảm ơn bạn đã tin tưởng và mua sắm tại cửa hàng chúng tôi.</p>\r\n        <p style=\"color: #555555;\">Chúng tôi sẽ thông báo cho bạn khi đơn hàng của bạn được gửi đi.</p>\r\n        <p style=\"color: #555555;\">Xin vui lòng liên hệ chúng tôi nếu bạn có bất kỳ câu hỏi hoặc yêu cầu thêm thông tin.</p>\r\n        <p style=\"text-align: center; margin-top: 20px;\">\r\n            <a href=\"#\" style=\"display: inline-block; padding: 10px 20px; font-size: 16px; text-align: center; text-decoration: none; color: #ffffff; background-color: #3498db; border-radius: 3px;\">Xem Đơn Hàng</a>\r\n        </p>\r\n    </div>\r\n\r\n</body>"
            });
            if (orderRequest.PaymentID == 2)
            {
                string url = GetPayment(totalAmount, order.OrderID);

                return responseObject.ResponseSucess(url, converter.EntityToDTO(order));

            }
            else
                return responseObject.ResponseSucess("http://localhost:3000/confirm", converter.EntityToDTO(order));


        }
        private List<OrderDetail> OrderDetail(int orderID, List<OrderDetailRequest> orderDetailRequests)
        {
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            foreach (var request in orderDetailRequests)
            {
                var product = dbContext.Product.FirstOrDefault(x => x.ProductID == request.ProductID);
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = orderID;
                orderDetail.ProductID = request.ProductID;
                orderDetail.PriceTotal = product != null ? product.Price * request.Quantity : 0;
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

        public List<OrderDTO> GetAll()
        {
            var listOrder = dbContext.Order.ToList();
            var listOrderDTO = listOrder.Select(converter.EntityToDTO).ToList();
            return listOrderDTO;
        }

        public ResponseObject<OrderDTO> ChangeOrderStatus(int id)
        {
            var order = dbContext.Order.FirstOrDefault(x => x.OrderID == id);
            if (order == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đơn hàng", null);
            order.OrderStatusID = 2;

            dbContext.Update(order);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Chuyển trạng thái đơn hàng thành công", converter.EntityToDTO(order));
        }

        private string GetPayment(double totalAmount, int orderID)
        {
            //Get Config Info
            string vnp_Returnurl = $"http://localhost:3000/confirm/";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "ADKF5BBF"; //Ma website
            string vnp_HashSecret = "BSTZNVIFXKNXZXBSJTNLHJIOJRJGDQMY"; //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                return "Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config";
            }


            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (totalAmount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            //vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");


            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + orderID);

            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", orderID.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày


            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            //Response.Redirect(paymentUrl);
        }

        public List<Payment> GetAllPayment()
        {
            return dbContext.Payment.ToList();
        }

        public List<OrderDetailDTO> GetAllOrderDetail(int orderID)
        {
            var listOrderDetail = dbContext.OrderDetail.Where(x => x.OrderID == orderID);
            var listOrderDetailDTO = listOrderDetail.Select(orderDetailConverter.EntityToDTO).ToList();
            return listOrderDetailDTO;
        }

        public List<OrderGetAllDTO> GetAllOrderByID(int accountID)
        {
            var listOrder = dbContext.Order.Where(x => x.AccountID == accountID);
            var listOrderGetAllDTO = listOrder.Select(orderGetAllConverter.EntityToDTO).ToList();
            return listOrderGetAllDTO;
        }

        public ResponseObject<OrderDTO> DeleteOrder(int id)
        {
            var order = dbContext.Order.FirstOrDefault(x => x.OrderID == id);
            if (order == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Order không tồn tại", null);

            dbContext.Remove(order);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Hủy đơn hàng thành công", converter.EntityToDTO(order));

        }

    }
}
