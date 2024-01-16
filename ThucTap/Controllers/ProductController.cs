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
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("GetProductByID/{id}")]
        public IActionResult GetProductByID(int id)
        {
            return Ok(_service.GetProductByID(id));
        }
        [HttpGet("GetRelatedProduct")]
        public IActionResult GetRelatedProduct(int productTypeID)
        {
            return Ok(_service.GetRelatedProduct(productTypeID));
        }
        [HttpGet("GetProductReview")]
        public IActionResult GetProductReview(int productID)
        {
            return Ok(_service.GetProductReview(productID));
        }
        [HttpGet("GetProductReviewByAccountID/{accountID}")]
        public IActionResult GetProductReviewByAccountID(int accountID)
        {
            return Ok(_service.GetProductReviewByAccountID(accountID));
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
        public IActionResult AddProductReview([FromForm] AddProductReviewRequest request)
        {
            return Ok(_service.AddProductReview(request));

        }
        [HttpGet("NumberOfPurchases")]
        public IActionResult NumberOfPurchases(int id)
        {
            return Ok(_service.NumberOfPurchases(id));

        }
        
    }
}
