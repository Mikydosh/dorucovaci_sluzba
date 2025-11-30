using System.Security.Claims;
using DorucovaciSluzba.Application.ViewModels;

namespace DorucovaciSluzba.Application.Abstraction
{
    public interface ISecurityService
    {
        Task<UserInfoViewModel?> FindUserByUsername(string username);
        Task<UserInfoViewModel?> FindUserByEmail(string email);
        Task<IList<string>> GetUserRoles(int userId);
        Task<UserInfoViewModel?> GetCurrentUser(ClaimsPrincipal principal);
    }
}
