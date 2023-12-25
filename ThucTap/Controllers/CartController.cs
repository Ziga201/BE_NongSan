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
        public IActionResult AddToCart(AddToCartRequest request)
        {
            return Ok(service.AddToCart(request));  
        }
    }
}
