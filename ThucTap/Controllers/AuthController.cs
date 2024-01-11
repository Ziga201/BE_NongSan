using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThucTap.IServices;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Requests.Auth;

namespace ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm]RegisterRequest request)
        {
            return Ok(_service.Register(request));
        }
        [HttpPut("Login")]
        public IActionResult Login([FromForm] LoginRequest request)
        {
            return Ok(_service.Login(request));
        }
        [HttpPut("RenewAccessToken")]
        public IActionResult RenewAccessToken([FromForm] RenewAccessTokenRequest request)
        {
            return Ok(_service.RenewAccessToken(request));
        }
        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("GetAllStaff")]
        public IActionResult GetAllStaff()
        {
            return Ok(_service.GetAllStaff());
        }
        [HttpPost("SendCode")]
        public IActionResult SendCode([FromForm] string email)
        {
            return Ok(_service.SendCode(email));
        }
        [HttpPut("ActiveAccount")]
        public IActionResult ActiveAccount([FromForm] ActiveAccountRequest request)
        {
            return Ok(_service.ActiveAccount(request));
        }
        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount([FromForm] AddAccountRequest request)
        {
            return Ok(await _service.AddAccount(request));
        }
        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromForm]UpdateAccountRequest request)
        {
            return Ok(await _service.UpdateAccount(request));
        }
        [HttpDelete("DeleteAccount/{id}")]
        public IActionResult DeleteAccount(int id)
        {
            return Ok(_service.DeleteAccount(id));
        }
        [HttpGet("GetAccountByID/{id}")]
        public IActionResult GetAccountByID(int id)
        {
            return Ok(_service.GetAccountByID(id));
        }
        [HttpPut("ForgotPassword")]
        public IActionResult ForgotPassword([FromForm] ForgotPasswordRequest request)
        {
            return Ok(_service.ForgotPassword(request));
        }
    }
}
