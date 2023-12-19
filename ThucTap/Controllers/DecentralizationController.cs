using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Services.Implement;
using ThucTap.Services.IServices;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecentralizationController : ControllerBase
    {
        private readonly IDecentralizationService service;

        public DecentralizationController()
        {
            service = new DecentralizationService();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        { 
            return Ok(service.GetAll());
        }
    }
}
