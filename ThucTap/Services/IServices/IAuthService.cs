using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Responses;

namespace ThucTap.IServices
{
    public interface IAuthService
    {
        Task<ResponseObject<RegisterDTO>> Register(RegisterRequest request);
        TokenDTO GenerateAccessToken(Account account);
        ResponseObject<TokenDTO> RenewAccessToken(RenewAccessTokenRequest request);
        ResponseObject<TokenDTO> Login(LoginRequest request);
        IQueryable<RegisterDTO> GetAll();
        ResponseObject<MailDTO> ForgotPassword(string mail);
        string CreateNewPassword(CreateNewPasswordRequest request);
        string ActiveAccount(ActiveAccountRequest request);


    }
}
