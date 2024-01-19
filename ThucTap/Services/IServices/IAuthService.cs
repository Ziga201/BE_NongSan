using CloudinaryDotNet.Actions;
using ThucTap.Entities;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Requests.Auth;
using ThucTap.Payloads.Responses;

namespace ThucTap.IServices
{
    public interface IAuthService
    {
        ResponseObject<RegisterDTO> Register(RegisterRequest request);
        TokenDTO GenerateAccessToken(Account account);
        ResponseObject<TokenDTO> RenewAccessToken(RenewAccessTokenRequest request);
        Task<ResponseObject<TokenDTO>> Login(LoginRequest request);
        IQueryable<RegisterDTO> GetAll();
        ResponseObject<MailDTO> SendCode(string email);
        ResponseObject<RegisterDTO> ActiveAccount(ActiveAccountRequest request);
        ResponseObject<RegisterDTO> ForgotPassword(ForgotPasswordRequest request);
        Task<ResponseObject<RegisterDTO>> AddAccount(AddAccountRequest request);
        Task<ResponseObject<RegisterDTO>> UpdateAccount(UpdateAccountRequest request);
        ResponseObject<RegisterDTO> DeleteAccount(int id);
        Account GetAccountByID(int id);
        IQueryable<RegisterDTO> GetAllStaff();


    }
}
