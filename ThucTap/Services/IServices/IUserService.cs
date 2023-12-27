using Microsoft.AspNetCore.Mvc.RazorPages;
using ThucTap.Entities;
using ThucTap.Handle.Page;
using ThucTap.Payloads.DTOs;
using ThucTap.Payloads.Requests;
using ThucTap.Payloads.Responses;

namespace ThucTap.IServices
{
    public interface IUserService
    {
        ResponseObject<UserDTO> AddUser(AddUserRequest request);
        ResponseObject<UserDTO> UpdateUser(UpdateUserRequest request);
        ResponseObject<UserDTO> DeleteUser(int id);
        List<User> GetAll();
        ResponseObject<UserDTO> GetByID(int id);
    }
}
