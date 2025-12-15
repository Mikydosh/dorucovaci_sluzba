using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Infrastructure.Database;
using DorucovaciSluzba.Infrastructure.Identity;
using DorucovaciSluzba.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorucovaciSluzba.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _dbContext;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        // GET -> /Admin/User/Select
        [HttpGet]
        public async Task<IActionResult> Select(string? sortBy = null, string? sortOrder = null, string? search = null, string? roleFilter = null)
        {
            var users = _userManager.Users.ToList();

            // Pro každého uživatele načti jeho role (PŘED filtrováním)
            var userRoles = new Dictionary<int, IList<string>>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles;
            }

            // Filtrování podle role
            if (!string.IsNullOrWhiteSpace(roleFilter))
            {
                users = users.Where(u =>
                    userRoles.ContainsKey(u.Id) &&
                    userRoles[u.Id].Any(r => r.Equals(roleFilter, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Vyhledávání
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                users = users.Where(u =>
                    (u.FirstName != null && u.FirstName.ToLower().Contains(search)) ||
                    (u.LastName != null && u.LastName.ToLower().Contains(search)) ||
                    // ignorovat e-mail, pokud je aplikován filtr podle role
                    (string.IsNullOrWhiteSpace(roleFilter) && u.Email != null && u.Email.ToLower().Contains(search)) ||
                    (u.UserName != null && u.UserName.ToLower().Contains(search)) ||
                    (u.Telefon != null && u.Telefon.Contains(search)) ||
                    (u.Mesto != null && u.Mesto.ToLower().Contains(search)) ||
                    (u.Ulice != null && u.Ulice.ToLower().Contains(search)) ||
                    (u.Psc != null && u.Psc.Contains(search)) ||
                    // Hledání v rolích pouze pokud není aplikován filtr podle role
                    (string.IsNullOrWhiteSpace(roleFilter) && userRoles.ContainsKey(u.Id) && userRoles[u.Id].Any(r => r.ToLower().Contains(search)))
                ).ToList();
            }

            // Řazení
            sortBy = sortBy?.ToLower() ?? "id";
            users = (sortBy, sortOrder?.ToLower()) switch
            {
                ("id", "desc") => users.OrderByDescending(u => u.Id).ToList(),
                ("id", _) => users.OrderBy(u => u.Id).ToList(),

                ("jmeno", "desc") => users.OrderByDescending(u => u.FirstName).ToList(),
                ("jmeno", _) => users.OrderBy(u => u.FirstName).ToList(),

                ("prijmeni", "desc") => users.OrderByDescending(u => u.LastName).ToList(),
                ("prijmeni", _) => users.OrderBy(u => u.LastName).ToList(),

                ("email", "desc") => users.OrderByDescending(u => u.Email).ToList(),
                ("email", _) => users.OrderBy(u => u.Email).ToList(),

                // Řazení podle role (první role v seznamu)
                ("role", "desc") => users.OrderByDescending(u =>
                    userRoles.ContainsKey(u.Id) && userRoles[u.Id].Any() ? userRoles[u.Id].First() : "").ToList(),
                ("role", _) => users.OrderBy(u =>
                    userRoles.ContainsKey(u.Id) && userRoles[u.Id].Any() ? userRoles[u.Id].First() : "").ToList(),

                _ => users.OrderBy(u => u.Id).ToList()
            };

            ViewBag.UserRoles = userRoles;
            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentOrder = sortOrder ?? "asc";
            ViewBag.CurrentSearch = search ?? "";
            ViewBag.RoleFilter = roleFilter ?? "";
            ViewBag.AvailableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return View(users);
        }

        // GET -> /Admin/User/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Zjisti aktuální roli uživatele
            var userRoles = await _userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault();
            var roleId = 0;

            if (!string.IsNullOrEmpty(currentRole))
            {
                var role = await _roleManager.FindByNameAsync(currentRole);
                roleId = role?.Id ?? 0;
            }

            var viewModel = new EditUserViewModel
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
                Psc = user.Psc,
                RoleId = roleId,
                DostupneRole = _roleManager.Roles.ToList() // Načti všechny dostupné role -> [Admin, Podpora, Kuryr, Uzivatel]
            };

            return View(viewModel);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            // Doplň dostupné role pro případ znovuzobrazení formuláře
            if (!ModelState.IsValid)
            {
                model.DostupneRole = _roleManager.Roles.ToList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Automaticky generuje UserName z emailu
            var userName = model.Email.Split('@')[0];

            // Aktualizuj data uživatele
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.UserName = userName;
            user.NormalizedUserName = userName.ToUpper();
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
                model.DostupneRole = _roleManager.Roles.ToList();
                return View(model);
            }

            // Aktualizuj roli
            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRole = await _roleManager.FindByIdAsync(model.RoleId.ToString());

            if (selectedRole != null)
            {
                // Odeber všechny staré role
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                // Přidej novou roli
                await _userManager.AddToRoleAsync(user, selectedRole.Name!);
            }

            TempData["UserSuccessMessage"] = $"Uživatel {user.FirstName} {user.LastName} byl úspěšně aktualizován!";
            return RedirectToAction(nameof(Select));
        }

        // Smazání uživatele
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Kontrola, zda uživatel nemá přiřazené zásilky
            var hasPackages = await _dbContext.Zasilky
                .AnyAsync(z => z.OdesilatelId == id ||
                               z.PrijemceId == id ||
                               z.KuryrId == id);

            // Pokud má, zobraz chybovou zprávu a nepovol smazání
            if (hasPackages)
            {
                TempData["UserErrorMessage"] = "Uživatel nemůže být smazán, protože má přiřazené zásilky. Nejprve odstraňte vazby na zásilky nebo je přiřaďte jinému uživateli.";
                return RedirectToAction(nameof(Select));
            }

            // smazání uživatele
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["UserSuccessMessage"] = "Uživatel byl úspěšně smazán.";
                return RedirectToAction(nameof(Select));
            }

            return RedirectToAction(nameof(Select));
        }
    }
}