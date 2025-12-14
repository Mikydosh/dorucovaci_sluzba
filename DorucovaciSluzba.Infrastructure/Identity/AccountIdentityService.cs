using Microsoft.AspNetCore.Identity;
using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Application.DTOs;
using DorucovaciSluzba.Domain.Enums;

namespace DorucovaciSluzba.Infrastructure.Identity
{
    public class AccountIdentityService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountIdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, true, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<string[]> Register(RegisterDto dto, params Roles[] roles)
        {
            // Zkontroluj, jestli uživatel s tímto emailem už existuje
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                // Pokud uživatel existuje, ale NEMÁ HESLO (anonymní)
                var hasPassword = await _userManager.HasPasswordAsync(existingUser);

                if (!hasPassword)
                {
                    // Povyš anonymního uživatele na registrovaného
                    existingUser.UserName = dto.Username;
                    existingUser.FirstName = dto.FirstName;
                    existingUser.LastName = dto.LastName;
                    existingUser.PhoneNumber = dto.Phone;

                    // Aktualizuj uživatele
                    var updateResult = await _userManager.UpdateAsync(existingUser);
                    if (!updateResult.Succeeded)
                    {
                        return updateResult.Errors.Select(e => e.Description).ToArray();
                    }

                    // Nastav heslo
                    var addPasswordResult = await _userManager.AddPasswordAsync(existingUser, dto.Password);
                    if (!addPasswordResult.Succeeded)
                    {
                        return addPasswordResult.Errors.Select(e => e.Description).ToArray();
                    }

                    // Přidej role
                    foreach (var role in roles)
                    {
                        if (!await _userManager.IsInRoleAsync(existingUser, role.ToString()))
                        {
                            var roleResult = await _userManager.AddToRoleAsync(existingUser, role.ToString());
                            if (!roleResult.Succeeded)
                            {
                                return roleResult.Errors.Select(e => e.Description).ToArray();
                            }
                        }
                    }

                    return null; // Úspěch - anonymní uživatel povýšen
                }
                else
                {
                    // Uživatel existuje a MÁ heslo → opravdu duplicita
                    return new[] { "Uživatel s tímto e-mailem již existuje." };
                }
            }

            User user = new User()
            {
                UserName = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            string[] errors = null;
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, role.ToString());
                    if (!resultRole.Succeeded)
                    {}
                }
            }

            if (result.Errors != null && result.Errors.Any())
            {
                errors = result.Errors.Select(e => e.Description).ToArray();
            }

            return errors;
        }
    }
}