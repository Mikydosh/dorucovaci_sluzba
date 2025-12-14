using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Application.DTOs;
using DorucovaciSluzba.Infrastructure.Identity;

namespace DorucovaciSluzba.Infrastructure.Identity
{
    public class SecurityIdentityService : ISecurityService
    {
        private readonly UserManager<User> _userManager;

        public SecurityIdentityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO?> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? await MapToViewModel(user) : null;
        }

        public async Task<UserDTO?> FindUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null ? await MapToViewModel(user) : null;
        }

        public async Task<IList<string>> GetUserRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<UserDTO?> GetCurrentUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return user != null ? await MapToViewModel(user) : null;
        }

        // Helper metoda pro mapování User -> UserInfoViewModel
        private async Task<UserDTO> MapToViewModel(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Roles = roles
            };
        }
    }
}