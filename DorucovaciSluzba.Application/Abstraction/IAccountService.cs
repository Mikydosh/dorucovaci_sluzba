using DorucovaciSluzba.Application.DTOs;
using DorucovaciSluzba.Domain.Enums;
using System.Data;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface IAccountService
    {
        Task<bool> Login(LoginDto dto);
        Task Logout();

        Task<string[]> Register(RegisterDto dto, params Roles[] roles);
    }
}
