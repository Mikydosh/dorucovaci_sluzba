using System.Security.Claims;
using DorucovaciSluzba.Application.DTOs;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface ISecurityService
    {
        Task<UserDTO> FindUserByUsername(string username);
        Task<UserDTO?> FindUserByEmail(string email);
        Task<IList<string>> GetUserRoles(int userId);
        Task<UserDTO?> GetCurrentUser(ClaimsPrincipal principal);
    }
}
