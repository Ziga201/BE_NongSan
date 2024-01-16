using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.Requests.Blog;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogController()
        {
            _service = new BlogService();
        }
        [HttpPost("AddBlog")]
        public async Task<IActionResult> AddBlog([FromForm] AddBlogRequest request)
        {
            return Ok(await _service.AddBlog(request));
        }
        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogRequest request)
        {
            return Ok(await _service.UpdateBlog(request));
        }
        [HttpDelete("DeleteBlog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            return Ok(_service.DeleteBlog(id));
        }
        [HttpPut("UpdateViewBlog/{id}")]
        public IActionResult UpdateViewBlog(int id)
        {
            return Ok(_service.UpdateViewBlog(id));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery]Pagination? pagination)
        {
            return Ok(_service.GetAll(pagination));
        }
        [HttpGet("GetAllByBlogTypeID/{id}")]
        public IActionResult GetAllByBlogTypeID(int id)
        {
            return Ok(_service.GetAllByBlogTypeID(id));
        }
        [HttpGet("GetBlogByID/{id}")]
        public IActionResult GetBlogByID(int id)
        {
            return Ok(_service.GetBlogByID(id));
        }
    }
}
