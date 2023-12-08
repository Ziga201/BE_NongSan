using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Handle.Page;
using ThucTap.IServices;
using ThucTap.Payloads.Requests.Product;
using ThucTap.Payloads.Requests.ProductType;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct([FromQuery] Pagination? pagination)
        {
            return Ok(_service.GetProduct(pagination));
        }
        [HttpGet("GetOutstandingProduct")]
        public IActionResult GetOutstandingProduct(int productTypeID)
        {
            return Ok(_service.GetOutstandingProduct(productTypeID));
        }
        [HttpGet("GetProductReview")]
        public IActionResult GetProductReview(int productTypeID)
        {
            return Ok(_service.GetProductReview(productTypeID));
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductRequest request)
        {
            return Ok(await _service.AddProduct(request));

        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
        {
            return Ok(await _service.UpdateProduct(request));
        }
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return Ok(_service.DeleteProduct(id));
        }
        [HttpPost("AddProductReview")]
        public IActionResult AddProductReview(AddProductReviewRequest request)
        {
            return Ok(_service.AddProductReview(request));

        }
        [HttpPut("UpdateView")]
        public IActionResult UpdateView(int id)
        {
            return Ok(_service.UpdateView(id));

        }
        [HttpGet("NumberOfPurchases")]
        public IActionResult NumberOfPurchases(int id)
        {
            return Ok(_service.NumberOfPurchases(id));

        }
        
    }
}
