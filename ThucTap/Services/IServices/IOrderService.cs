using Microsoft.AspNetCore.Mvc.RazorPages;
using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests.Order;
using ThucTap.Payloads.Responses;

namespace ThucTap.Services.IServices
{
    public interface IOrderService
    {
        double Statistic(int? month,int? quarter, int? year);
        ResponseObject<OrderDTO> Order(OrderRequest orderRequest, List<OrderDetailRequest> orderDetailRequests);
        PageResult<OrderGetAllDTO> GetAll(Pagination? pagination);
        List<OrderGetAllDTO> GetAllOrderByID(int accountID);
        List<OrderDetailDTO> GetAllOrderDetail(int orderID);
        ResponseObject<OrderDTO> ChangeOrderStatus(int id);
        List<Payment> GetAllPayment();

        ResponseObject<OrderDTO> DeleteOrder(int id);


    }
}
