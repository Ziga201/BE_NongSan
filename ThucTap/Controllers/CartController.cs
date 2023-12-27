using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Payloads.Requests.Cart;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService service;

        public CartController()
        {
            service = new CartService();
        }
        [HttpPost("AddToCart")]
        public IActionResult AddToCart([FromForm]AddToCartRequest request)
        {
            return Ok(service.AddToCart(request));  
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll(int userID)
        {
            return Ok(service.GetAll(userID));  
        }
        [HttpDelete("DeleteCart/{id}")]
        public IActionResult DeleteCart(int id)
        {
            return Ok(service.DeleteCart(id));  
        } 
        [HttpDelete("DeleteCartItem/{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            return Ok(service.DeleteCartItem(id));  
        }
    }
}
