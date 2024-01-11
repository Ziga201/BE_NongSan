using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Entities;
using ThucTap.Payloads.Requests.Other;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly IOtherService service;

        public OtherController()
        {
            service = new OtherService();
        }

        [HttpPost("SendMessage")]
        public IActionResult SendMessage([FromForm]MessageRequest request)
        {
            return Ok(service.SendMessage(request));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }
    }
}
