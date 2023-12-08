using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Order;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IOrderService
    {
        double Statistic(int? month,int? quarter, int? year);
        ResponseObject<OrderDTO> Order(OrderRequest orderRequest, List<OrderDetailRequest> orderDetailRequests);
        List<OrderDTO> GetAll();
        ResponseObject<OrderDTO> ChangeOrderStatus(int id);
        //string GetPayment(double totalAmount, int id);

    }
}
