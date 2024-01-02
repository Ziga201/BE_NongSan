using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Payloads.Requests.Order;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController()
        {
            service = new OrderService();
        }

        [HttpGet("Statistic")]
        public IActionResult Statistic([FromQuery]int? month, [FromQuery] int? quarter, [FromQuery] int? year)
        {
            return Ok(service.Statistic(month, quarter, year));
        }
        [HttpPost("Order")]
        public IActionResult Order([FromQuery]OrderRequest orderRequest, [FromBody] List<OrderDetailRequest> orderDetailRequest)
        {
            return Ok(service.Order(orderRequest, orderDetailRequest));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }
         [HttpGet("GetAllOrderByID/{userID}")]
        public IActionResult GetAllOrderByID(int userID)
        {
            return Ok(service.GetAllOrderByID(userID));
        }
        [HttpGet("GetAllOrderDetail/{orderID}")]
        public IActionResult GetAllOrderDetail(int orderID)
        {
            return Ok(service.GetAllOrderDetail(orderID));
        }
        [HttpGet("GetAllPayment")]
        public IActionResult GetAllPayment()
        {
            return Ok(service.GetAllPayment());
        }
        [HttpPut("ChangeOrderStatus")]
        public IActionResult ChangeOrderStatus(int id)
        {
            return Ok(service.ChangeOrderStatus(id));
        }
        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            return Ok(service.DeleteOrder(id));
        }


    }
}
