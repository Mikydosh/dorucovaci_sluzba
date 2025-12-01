using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Infrastructure.Identity;
using DorucovaciSluzba.Models.Package;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DorucovaciSluzba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        IPackageAppService _packageAppService;
        UserManager<User> _userManager;

        public PackageController(IPackageAppService packageAppService, UserManager<User> userManager)
        {
            _packageAppService = packageAppService;
            _userManager = userManager;
        }

        // ========================================
        // CHRÁNĚNÉ AKCE (pouze Admin a Podpora)
        // ========================================

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]

        public async Task<IActionResult> Select()
        {
            IList<Zasilka> packages = _packageAppService.Select();

            // NOVÉ: Načti všechny uživatele najednou (efektivnější než po jednom)
            var userIds = packages
                .SelectMany(z => new[] { z.OdesilatelId, z.PrijemceId, z.KuryrId ?? 0 })
                .Distinct()
                .Where(id => id > 0)
                .ToList();

            var users = new Dictionary<int, User>();
            foreach (var id in userIds)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    users[id] = user;
                }
            }

            // Předej uživatele do ViewBag
            ViewBag.Users = users;

            return View(packages);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]
        public async Task<IActionResult> Create(CreateZasilkaViewModel model)
        {
            // Validace modelu
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // Najdi nebo vytvoř odesílatele
                var odesilatel = await GetOrCreateUserAsync(
                    model.OdesilatelJmeno,
                    model.OdesilatelPrijmeni,
                    model.OdesilatelEmail,
                    model.OdesilatelUlice,
                    model.OdesilatelCP,
                    model.OdesilatelMesto,
                    model.OdesilatelPsc
                );

                // Najdi nebo vytvoř příjemce
                var prijemce = await GetOrCreateUserAsync(
                    model.PrijemceJmeno,
                    model.PrijemcePrijmeni,
                    model.PrijemceEmail,
                    model.DestinaceUlice,
                    model.DestinaceCP,
                    model.DestinaceMesto,
                    model.DestinacePsc
                );

                // Vytvoř zásilku
                var zasilka = new Zasilka
                {
                    OdesilatelId = odesilatel.Id,
                    PrijemceId = prijemce.Id,
                    DestinaceUlice = model.DestinaceUlice,
                    DestinaceCP = model.DestinaceCP,
                    DestinaceMesto = model.DestinaceMesto,
                    DestinacePsc = model.DestinacePsc
                    // Cislo, DatumOdeslani, StavId se nastaví automaticky v AppService
                };

                _packageAppService.Create(zasilka);

                return RedirectToAction(nameof(PackageController.Select));
            }
            catch (Exception)
            {
                return View(model);
            }
        }


        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]
        public IActionResult Delete(int id)
        {
            bool deleted = _packageAppService.Delete(id);

            if (deleted){
                return RedirectToAction(nameof(PackageController.Select));
            }
            else {
                return NotFound();
            }
        }

        


        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]
        public async Task<IActionResult> Edit(int id)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            // NOVÉ: Načti uživatele
            var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
            var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());

            var viewModel = new EditUserViewModel
            {
                Id = zasilka.Id,
                Cislo = zasilka.Cislo,
                DatumOdeslani = zasilka.DatumOdeslani,
                OdesilatelJmeno = $"{odesilatel?.FirstName} {odesilatel?.LastName}",
                PrijemceJmeno = $"{prijemce?.FirstName} {prijemce?.LastName}",
                DestinaceAdresa = $"{zasilka.DestinaceUlice} {zasilka.DestinaceCP}, {zasilka.DestinaceMesto}, {zasilka.DestinacePsc}",
                StavId = zasilka.StavId,
                KuryrId = zasilka.KuryrId,
                DostupneStavy = _packageAppService.GetAllStates().ToList(),

                // ZMĚNA: Načti kurýry z UserManager
                DostupniKuryri = (await _userManager.GetUsersInRoleAsync("Kuryr")).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _userManager.GetUsersInRoleAsync("Kuryr").Result.ToList();
                return View(model);
            }

            try
            {
                var zasilka = new Zasilka
                {
                    Id = model.Id,
                    StavId = model.StavId,
                    KuryrId = model.KuryrId
                };

                _packageAppService.Update(zasilka);

                TempData["SuccessMessage"] = $"Zásilka byla úspěšně aktualizována!";

                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při aktualizaci: {ex.Message}");
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _userManager.GetUsersInRoleAsync("Kuryr").Result.ToList();
                return View(model);
            }
        }

        // ========================================
        // VEŘEJNÉ AKCE (bez autorizace)
        // ========================================

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Track(TrackPackageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var zasilka = _packageAppService.FindByCisloAndEmail(
                    model.CisloZasilky,
                    model.Email
                );

                if (zasilka == null)
                {
                    ModelState.AddModelError("", "Zásilka nebyla nalezena. Zkontrolujte číslo zásilky a e-mail.");
                    return View(model);
                }

                // ZMĚNA: Ověř, že email patří odesílateli nebo příjemci
                var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
                var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());

                if (odesilatel?.Email.ToLower() != model.Email.ToLower() &&
                    prijemce?.Email.ToLower() != model.Email.ToLower())
                {
                    ModelState.AddModelError("", "Zásilka nebyla nalezena. Zkontrolujte číslo zásilky a e-mail.");
                    return View(model);
                }

                return RedirectToAction("Detail", new { id = zasilka.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při vyhledávání: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            // NOVÉ: Načti uživatele z UserManager
            var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
            var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());
            User? kuryr = null;
            if (zasilka.KuryrId.HasValue)
            {
                kuryr = await _userManager.FindByIdAsync(zasilka.KuryrId.Value.ToString());
            }

            // Předej do ViewBag
            ViewBag.Odesilatel = odesilatel;
            ViewBag.Prijemce = prijemce;
            ViewBag.Kuryr = kuryr;

            return View(zasilka);
        }

        // ========================================
        // HELPER METODY
        // ========================================

        private async Task<User> GetOrCreateUserAsync(
            string jmeno, string prijmeni, string email,
            string ulice, string cp, string mesto, string psc)
        {
            // Zkus najít podle emailu
            var existujici = await _userManager.FindByEmailAsync(email);

            if (existujici != null)
            {
                // Aktualizuj adresu, pokud se změnila
                bool zmeneno = false;

                if (existujici.Ulice != ulice) { existujici.Ulice = ulice; zmeneno = true; }
                if (existujici.CP != cp) { existujici.CP = cp; zmeneno = true; }
                if (existujici.Mesto != mesto) { existujici.Mesto = mesto; zmeneno = true; }
                if (existujici.Psc != psc) { existujici.Psc = psc; zmeneno = true; }

                if (zmeneno)
                {
                    await _userManager.UpdateAsync(existujici);
                }

                return existujici;
            }

            // Vytvoř nového uživatele
            var novy = new User
            {
                UserName = email.Split('@')[0],
                Email = email,
                FirstName = jmeno,
                LastName = prijmeni,
                Ulice = ulice,
                CP = cp,
                Mesto = mesto,
                Psc = psc,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(novy, "Temp123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(novy, "Uzivatel");
                return novy;
            }

            throw new Exception($"Nepodařilo se vytvořit uživatele: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

    }
}


