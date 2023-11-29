using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThucTap.IServices;
using ThucTap.Payloads.Requests;

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
        public async Task<IActionResult> Register([FromForm]RegisterRequest request)
        {
            return Ok(await _service.Register(request));
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string mail)
        {
            return Ok(_service.ForgotPassword(mail));
        }
        [HttpPut("CreateNewPassword")]
        public IActionResult CreateNewPassword(CreateNewPasswordRequest request)
        {
            return Ok(_service.CreateNewPassword(request));
        }
        [HttpPut("ActiveAccount")]
        public IActionResult ActiveAccount(ActiveAccountRequest request)
        {
            return Ok(_service.ActiveAccount(request));
        }
    }
}
