using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Entities;
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
        public IActionResult AddBlog([FromForm] AddBlogRequest request)
        {
            return Ok(_service.AddBlog(request));
        }
        [HttpPut("UpdateBlog")]
        public IActionResult UpdateBlog([FromForm] UpdateBlogRequest request)
        {
            return Ok(_service.UpdateBlog(request));
        }
        [HttpDelete("DeleteBlog/{id}")]
        public IActionResult DeleteBlog(int id)
        {
            return Ok(_service.DeleteBlog(id));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("GetBlogByID/{id}")]
        public IActionResult GetBlogByID(int id)
        {
            return Ok(_service.GetBlogByID(id));
        }
    }
}
