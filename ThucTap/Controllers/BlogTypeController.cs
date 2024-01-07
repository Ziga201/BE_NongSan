using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Entities;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTypeController : ControllerBase
    {
        private readonly IBlogTypeService _service;

        public BlogTypeController()
        {
            _service = new BlogTypeService();
        }
        [HttpPost("AddBlogType")]
        public IActionResult AddBlogType([FromForm] string blogTypeName )
        {
            return Ok(_service.AddBlogType(blogTypeName));
        }
        [HttpPut("UpdateBlogType")]
        public IActionResult UpdateBlogType([FromForm] BlogType blogType )
        {
            return Ok(_service.UpdateBlogType(blogType));
        }
        [HttpDelete("DeleteBlogType/{id}")]
        public IActionResult DeleteBlogType(int id)
        {
            return Ok(_service.DeleteBlogType(id));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}
