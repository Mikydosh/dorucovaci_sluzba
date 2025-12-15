using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Infrastructure.Identity;
using DorucovaciSluzba.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DorucovaciSluzba.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Customer/Profile/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Security" });
            }

            // Naplň ViewModel daty z databáze
            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Telefon = user.Telefon,
                DatumNarozeni = user.DatumNarozeni,
                Ulice = user.Ulice,
                CP = user.CP,
                Mesto = user.Mesto,
                Psc = user.Psc
                // NewPassword a ConfirmPassword zůstanou prázdné
            };

            return View(viewModel);
        }

        // POST: /Customer/Profile/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileViewModel model)
        {
            // Validace modelu
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Získej aktuálního uživatele
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Security" });
            }

            try
            {
                // Aktualizuj základní údaje
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.UserName = model.Email.Split('@')[0];
                user.NormalizedUserName = user.UserName.ToUpper();
                user.Telefon = model.Telefon;
                user.DatumNarozeni = model.DatumNarozeni;
                user.Ulice = model.Ulice;
                user.CP = model.CP;
                user.Mesto = model.Mesto;
                user.Psc = model.Psc;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                // Změna hesla POUZE pokud je vyplněno
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    // Odeber staré heslo
                    var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                    if (removePasswordResult.Succeeded)
                    {
                        // Přidej nové heslo
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                        if (!addPasswordResult.Succeeded)
                        {
                            foreach (var error in addPasswordResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }

                        TempData["ProfileSuccessMessage"] = "Profil a heslo byly úspěšně aktualizovány!";
                    }
                    else
                    {
                        foreach (var error in removePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    TempData["ProfileSuccessMessage"] = "Váš profil byl úspěšně aktualizován!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při aktualizaci profilu: {ex.Message}");
                return View(model);
            }
        }
    }
}