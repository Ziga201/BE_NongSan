﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ThucTap.Entities;
using ThucTap.Handle.Image;
using ThucTap.Handle.Send;
using ThucTap.Handle.Validate;
using ThucTap.IServices;
using ThucTap.Migrations;
using ThucTap.Payloads.Converters;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Requests.Auth;
using ThucTap.Payloads.Responses;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ThucTap.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly ResponseObject<RegisterDTO> responseObject;
        private readonly RegisterConverter converter;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<TokenDTO> responseTokenObject;
        private readonly ResponseObject<MailDTO> responseMailObject;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            responseObject = new ResponseObject<RegisterDTO>();
            converter = new RegisterConverter();
            responseTokenObject = new ResponseObject<TokenDTO>();
            responseMailObject = new ResponseObject<MailDTO>();
        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public TokenDTO GenerateAccessToken(Account account)
        {
            string secretKey = "ploboocyuvdwxjumbyprpwuvwsfuvfrp";
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);            
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);

            var role = dbContext.Decentralization.FirstOrDefault(x => x.DecentralizationID == account.DecentralizationID);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", account.AccountID.ToString()),
                    new Claim("Username", account.UserName),
                    new Claim(ClaimTypes.Role, role.AuthorityName)
                }),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //RefreshToken rf = new RefreshToken
            //{
            //    Token = refreshToken,
            //    ExpiredTime = DateTime.Now.AddDays(1),
            //    UserId = user.Id
            //};

            account.ResetPasswordToken = refreshToken;
            account.ResetPasswordTokenExpiry = DateTime.Now.AddDays(1);

            dbContext.Update(account);
            dbContext.SaveChanges();

            TokenDTO tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }

        //public TokenDTO GenerateAccessToken(Account account)
        //{
        //    string secretKey = "ploboocyuvdwxjumbyprpwuvwsfuvfrp";


        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

        //    var algorithm = SecurityAlgorithms.HmacSha256;

        //    var credentials = new SigningCredentials(key, algorithm);
        //    var role = dbContext.Decentralization.FirstOrDefault(x => x.DecentralizationID == account.DecentralizationID);
        //    var tokenDescription = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("Id", account.AccountID.ToString()),
        //            new Claim("Username", account.UserName),
        //            new Claim(ClaimTypes.Role, role.AuthorityName)
        //        }),
        //        NotBefore = DateTime.UtcNow,
        //        Expires = DateTime.UtcNow.AddHours(4),
        //        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        //    };


        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescription);

        //    var accessToken  = tokenHandler.WriteToken(token);

        //    TokenDTO tokenDTO = new TokenDTO
        //    {
        //        AccessToken = accessToken,
        //        RefreshToken = "aaa"
        //    };
        //    return tokenDTO;
        //}





        public ResponseObject<RegisterDTO> Register(RegisterRequest request)
        {
            if (dbContext.Account.Any(x => x.UserName == request.UserName))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại", null);
            }
            if (!ValidatePassword.isValidPassword(request.Password))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu bao gồm chữ, số và ký tự đặc biệt", null);
            }
            if (!ValidateEmail.isValidEmail(request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email không hợp lệ", null);
            if (dbContext.Account.Any(x => x.Email == request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại", null);


            Account account = new Account();
            account.UserName = request.UserName;
            account.Password = request.Password;
            account.Email = request.Email;
            account.Status = nameof(Enum.Status.INACTIVE);
            account.DecentralizationID = 1;
            account.Avatar = "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg";
            account.CreatedAt = DateTime.Now;
            account.UpdateAt = DateTime.Now;
            dbContext.Add(account);
            dbContext.SaveChanges();


            HandleSendCode(account.AccountID, account.Email);
            return responseObject.ResponseSucess("Đăng ký tài khoản thành công, vui lòng kiểm tra email để nhận mã xác thực tài khoản",
                converter.EntityToDTO(account));

        }

        private void HandleSendCode(int accountID, string email)
        {
            //Send code
            var confirm = dbContext.ConfirmEmail.FirstOrDefault(x => x.AccountID == accountID);
            if (confirm != null)
            {
                dbContext.Remove(confirm);
                dbContext.SaveChanges();
            }
            ConfirmEmail confirmEmail = new ConfirmEmail();
            confirmEmail.AccountID = accountID;
            confirmEmail.CodeActive = new Random().Next(100000, 999999);
            confirmEmail.ExpriedTime = DateTime.Now.AddMinutes(2);
            confirmEmail.IsConfirmed = false;
            dbContext.Add(confirmEmail);
            dbContext.SaveChanges();

            SendMail.Send(new MailContent
            {
                MailTo = email,
                Subject = "Mã xác thực tài khoản",
                Content = $"<table role=\"presentation\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px;\">\r\n        <tr>\r\n            <td align=\"\">\r\n                <h2 style=\"color: #3498db;\">Công ty Đông Anh Group!</h2>\r\n                <p>Cảm ơn bạn đã đăng ký. Đây là mã kích hoạt tài khoản của bạn:</p>\r\n                <p style=\"font-size: 24px; font-weight: bold; color: #3498db;\">[{confirmEmail.CodeActive}]</p>\r\n                <p>Nếu bạn không thực hiện đăng ký, xin vui lòng bỏ qua email này.</p>\r\n                <p>Trân trọng,</p>\r\n                <p style=\"color: #333; font-weight: bold;\">[Web bán hàng]</p>\r\n            </td>\r\n        </tr>\r\n    </table>"
            });
        }


        public ResponseObject<TokenDTO> RenewAccessToken(RenewAccessTokenRequest request)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.ResetPasswordToken == request.RefeshToken);
            if (account == null)
                return responseTokenObject.ResponseError(StatusCodes.Status404NotFound, "Refresh Token không tồn tại", null);
            if (account.ResetPasswordTokenExpiry < DateTime.Now)
                return responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Refresh Token chưa hết hạn", null);
            return responseTokenObject.ResponseSucess("Tạo mới Token thành công", GenerateAccessToken(account));
        }

        public async Task<ResponseObject<TokenDTO>> Login(LoginRequest request)
        {
            var account = await dbContext.Account.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (account == null)
                return responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản hoặc mật khẩu không chính xác", null);
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin đăng nhập", null);
            //bool checkPass = BCryptNet.Verify(request.Password, account.Password);
            if (request.Password != account.Password)
                return responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản hoặc mật khẩu không chính xác", null);
            if (account.Status == nameof(Enum.Status.INACTIVE))
                return responseTokenObject.ResponseError(StatusCodes.Status400BadRequest, "tài khoản chưa được kích hoạt", null);

            return responseTokenObject.ResponseSucess("Đăng nhập thành công", GenerateAccessToken(account));
        }

        public IQueryable<RegisterDTO> GetAll()
        {
            var result = dbContext.Account.Select(x => converter.EntityToDTO(x));
            return result;
        }

        public ResponseObject<MailDTO> SendCode(string email)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.Email == email);

            if (account == null)
            {
                return responseMailObject.ResponseError(StatusCodes.Status404NotFound, "Mail không tồn tại", null);
            }
            var confirm = dbContext.ConfirmEmail.FirstOrDefault(x => x.AccountID == account.AccountID);
            if (confirm != null)
            {
                dbContext.Remove(confirm);
                dbContext.SaveChanges();
            }
            HandleSendCode(account.AccountID, account.Email);



            return responseMailObject.ResponseSucess("Đã gửi mã xác thực, mã có hiệu lực trong 2 phút! Vui lòng kiểm tra Mail", null);
        }

        public ResponseObject<RegisterDTO> ActiveAccount(ActiveAccountRequest request)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.Email == request.Email);
            if (account == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Email không tồn tại", null);
            var confirm = dbContext.ConfirmEmail.FirstOrDefault(x => x.AccountID == account.AccountID
                            && x.CodeActive == request.CodeActive);
            if (confirm == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác thực không đúng!", null);
            if (confirm.ExpriedTime < DateTime.Now)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác thực hết hạn sử dụng", null);
            confirm.IsConfirmed = true;
            dbContext.Update(confirm);
            dbContext.SaveChanges();

            account.Status = nameof(Enum.Status.ACTIVE);
            dbContext.Update(account);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xác thực tài khoản thành công, mời bạn đăng nhập", null);
        }

        public async Task<ResponseObject<RegisterDTO>> UpdateAccount(UpdateAccountRequest request)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.AccountID == request.AccountID);
            if (account == null)
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản không tồn tại", null);
            if (!ValidatePassword.isValidPassword(request.Password))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu bao gồm chữ, số và ký tự đặc biệt", null);
            if (!ValidateEmail.isValidEmail(request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email không hợp lệ", null);

            if (account.Email != request.Email)
                if (dbContext.Account.Any(x => x.Email == request.Email))
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại", null);

            var avatarFile = await UploadImage.Upfile(request.Avatar);


            account.Password = request.Password;
            account.Email = request.Email;
            account.Status = request.Status ?? "ACTIVE";
            account.DecentralizationID = request.DecentralizationID ?? 1;
            if (avatarFile != "") account.Avatar = avatarFile;
            //account.Avatar = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            account.FullName = request.FullName;
            account.Phone = request.Phone;
            account.Address = request.Address;
            account.UpdateAt = DateTime.Now;
            dbContext.Update(account);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Cập nhật tài khoản thành công", converter.EntityToDTO(account));
        }
        public ResponseObject<RegisterDTO> DeleteAccount(int id)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.AccountID == id);
            if (account == null)
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản không tồn tại", null);
            dbContext.Remove(account);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Xoá tài khoản thành công", converter.EntityToDTO(account));
        }

        public Account GetAccountByID(int id)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.AccountID == id);
            if (account == null) return null;
            return account;
        }

        public async Task<ResponseObject<RegisterDTO>> AddAccount(AddAccountRequest request)
        {
            if (dbContext.Account.Any(x => x.UserName == request.UserName))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản đã tồn tại", null);
            }
            if (!ValidatePassword.isValidPassword(request.Password))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu bao gồm chữ, số và ký tự đặc biệt", null);
            }
            if (!ValidateEmail.isValidEmail(request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email không hợp lệ", null);
            if (dbContext.Account.Any(x => x.Email == request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại", null);

            var avatarFile = await UploadImage.Upfile(request.Avatar);

            Account account = new Account();
            account.UserName = request.UserName;
            //account.Password = BCryptNet.HashPassword(request.Password);
            account.Password = request.Password;
            account.Email = request.Email;
            account.Status = request.Status;
            account.DecentralizationID = request.DecentralizationID;
            account.Avatar = avatarFile == "" ? "https://inkythuatso.com/uploads/thumbnails/800/2023/03/9-anh-dai-dien-trang-inkythuatso-03-15-27-03.jpg" : avatarFile;
            account.FullName = request.FullName;
            account.Phone = request.Phone;
            account.Address = request.Address;
            account.CreatedAt = DateTime.Now;
            account.UpdateAt = DateTime.Now;
            dbContext.Add(account);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Thêm tài khoản thành công", converter.EntityToDTO(account));
        }

        public IQueryable<RegisterDTO> GetAllStaff()
        {
            return dbContext.Account.Where(x => x.DecentralizationID == 3).Select(converter.EntityToDTO).AsQueryable();
        }

        public ResponseObject<RegisterDTO> ForgotPassword(ForgotPasswordRequest request)
        {
            var account = dbContext.Account.FirstOrDefault(x => x.Email == request.Email);
            if (account == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Email không tồn tại", null);
            var confirm = dbContext.ConfirmEmail.FirstOrDefault(x => x.AccountID == account.AccountID
                            && x.CodeActive == request.CodeActive);
            if (confirm == null)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác thực không đúng!", null);
            if (confirm.ExpriedTime < DateTime.Now)
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Mã xác thực hết hạn sử dụng", null);
            if (!ValidatePassword.isValidPassword(request.NewPassword))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu bao gồm chữ, số và ký tự đặc biệt", null);
            confirm.IsConfirmed = true;
            dbContext.Update(confirm);
            dbContext.SaveChanges();

            account.Password = request.NewPassword;
            dbContext.Update(account);
            dbContext.SaveChanges();
            return responseObject.ResponseSucess("Cập nhật mật khẩu mới thành công, mời bạn đăng nhập", converter.EntityToDTO(account));
        }
    }
}
