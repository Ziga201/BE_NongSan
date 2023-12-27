using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTap.Handle.Page;
using ThucTap.IServices;
using ThucTap.Payloads.Requests;
using ThucTap.Services;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController()
        {
            _service = new UserService();
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(AddUserRequest request)
        {
            return Ok(_service.AddUser(request));
        }
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserRequest request)
        {
            return Ok(_service.UpdateUser(request));
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(_service.DeleteUser(id));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("GetByID")]
        public IActionResult GetByID(int id)
        {
            return Ok(_service.GetByID(id));
        }

    }
}
