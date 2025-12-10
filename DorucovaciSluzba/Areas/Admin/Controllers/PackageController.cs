using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Extensions;
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
        private readonly IPackageHistoryAppService _packageHistoryAppService;

        public PackageController(
            IPackageAppService packageAppService, 
            UserManager<User> userManager,
            IPackageHistoryAppService packageHistoryAppService)
        {
            _packageAppService = packageAppService;
            _userManager = userManager;
            _packageHistoryAppService = packageHistoryAppService;
        }

        // ========================================
        // CHRÁNĚNÉ AKCE (pouze Admin a Podpora)
        // ========================================

        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora))]

        public async Task<IActionResult> Select(string? sortBy, string? sortOrder, string? search = null)
        {
            IList<Zasilka> packages = _packageAppService.Select(sortBy, sortOrder ?? "asc", null);

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

            // Filtrování podle search (včetně jmen uživatelů, emailů)
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                packages = packages.Where(z =>
                    // Číslo zásilky
                    z.Cislo.ToLower().Contains(search) ||

                    // Destinace
                    z.DestinaceUlice.ToLower().Contains(search) ||
                    z.DestinaceMesto.ToLower().Contains(search) ||
                    z.DestinacePsc.Contains(search) ||
                    z.DestinaceCP.ToLower().Contains(search) ||

                    // Stav
                    (z.Stav?.Stav != null && z.Stav.Stav.ToLower().Contains(search)) ||

                    // Odesílatel
                    (users.ContainsKey(z.OdesilatelId) && (
                        (users[z.OdesilatelId].FirstName != null && users[z.OdesilatelId].FirstName.ToLower().Contains(search)) ||
                        (users[z.OdesilatelId].LastName != null && users[z.OdesilatelId].LastName.ToLower().Contains(search)) ||
                        (users[z.OdesilatelId].Email != null && users[z.OdesilatelId].Email.ToLower().Contains(search))
                    )) ||

                    // Příjemce
                    (users.ContainsKey(z.PrijemceId) && (
                        (users[z.PrijemceId].FirstName != null && users[z.PrijemceId].FirstName.ToLower().Contains(search)) ||
                        (users[z.PrijemceId].LastName != null && users[z.PrijemceId].LastName.ToLower().Contains(search)) ||
                        (users[z.PrijemceId].Email != null && users[z.PrijemceId].Email.ToLower().Contains(search))
                    )) ||

                    // Kurýr
                    (z.KuryrId.HasValue && users.ContainsKey(z.KuryrId.Value) && (
                        (users[z.KuryrId.Value].FirstName != null && users[z.KuryrId.Value].FirstName.ToLower().Contains(search)) ||
                        (users[z.KuryrId.Value].LastName != null && users[z.KuryrId.Value].LastName.ToLower().Contains(search)) ||
                        (users[z.KuryrId.Value].Email != null && users[z.KuryrId.Value].Email.ToLower().Contains(search))
                    ))
                ).ToList();
            }

            // Předej uživatele do ViewBag
            ViewBag.Users = users;
            // informace o řazení do view
            ViewBag.CurrentSort = sortBy ?? "Id";
            ViewBag.CurrentOrder = sortOrder ?? "asc";
            // search
            ViewBag.CurrentSearch = search ?? "";

            return View(packages);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

                // Zaloguj vytvoření zásilky (první stav)
                _packageHistoryAppService.Create(zasilka.Id, zasilka.StavId);

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
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora) + ", " + nameof(Roles.Kuryr))]
        public async Task<IActionResult> Edit(int id, string? returnUrl = null)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            // Kontrola oprávnění pro kurýra
            if (User.IsInRole(nameof(Roles.Kuryr)))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || zasilka.KuryrId != currentUser.Id)
                {
                    return Forbid();
                }
            }

            // Načti uživatele
            var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
            var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());

            var viewModel = new EditZasilkaViewModel
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

            ViewBag.ReturnUrl = returnUrl;
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Podpora) + ", " + nameof(Roles.Kuryr))]
        public IActionResult Edit(EditZasilkaViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _userManager.GetUsersInRoleAsync("Kuryr").Result.ToList();
                return View(model);
            }

            try
            {
                // Načti starý stav před aktualizací
                var oldZasilka = _packageAppService.GetById(model.Id);
                int? oldStavId = oldZasilka?.StavId;

                var zasilka = new Zasilka
                {
                    Id = model.Id,
                    StavId = model.StavId,
                    KuryrId = model.KuryrId
                };

                _packageAppService.Update(zasilka);

                // Zaloguj změnu POUZE pokud se stav změnil
                if (oldStavId.HasValue && oldStavId.Value != model.StavId)
                {
                    // Zkontroluj, jestli tento stav už není v historii
                    var existujiciHistorie = _packageHistoryAppService.GetHistoryForPackage(model.Id);
                    bool stavUzExistuje = existujiciHistorie.Any(h => h.StavId == model.StavId);

                    if (!stavUzExistuje)
                    {
                        _packageHistoryAppService.Create(model.Id, model.StavId);
                    }
                }

                TempData["SuccessMessage"] = $"Zásilka byla úspěšně aktualizována!";

                // Použij returnUrl nebo defaultní Select
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při aktualizaci: {ex.Message}");
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _userManager.GetUsersInRoleAsync("Kuryr").Result.ToList();
                ViewBag.ReturnUrl = returnUrl;
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

                // Ověř, že email patří odesílateli nebo příjemci
                var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
                var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());

                if (odesilatel?.Email.ToLower() != model.Email.ToLower() &&
                    prijemce?.Email.ToLower() != model.Email.ToLower())
                {
                    ModelState.AddModelError("", "Zásilka nebyla nalezena. Zkontrolujte číslo zásilky a e-mail.");
                    return View(model);
                }

                // Použití SessionExtensions
                var authorizedPackages = HttpContext.Session.GetObject<List<int>>("AuthorizedPackages")
                                         ?? new List<int>();

                if (!authorizedPackages.Contains(zasilka.Id))
                {
                    authorizedPackages.Add(zasilka.Id);
                    HttpContext.Session.SetObject("AuthorizedPackages", authorizedPackages);
                }

                // Ulož email do session pro ověření příjemce
                HttpContext.Session.SetString($"PackageEmail_{zasilka.Id}", model.Email);

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

            // KONTROLA OPRÁVNĚNÍ
            bool isAuthorized = false;

            // 1. Je uživatel přihlášený jako Admin nebo Podpora?
            if (User.IsInRole(nameof(Roles.Admin)) || User.IsInRole(nameof(Roles.Podpora)))
            {
                isAuthorized = true;
            }
            // 2. Je uživatel Kurýr a je mu zásilka přiřazena?
            else if (User.IsInRole(nameof(Roles.Kuryr)))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && zasilka.KuryrId == currentUser.Id)
                {
                    isAuthorized = true;
                }
            }
            // 3. Je uživatel běžný Uživatel a je odesílatel nebo příjemce?
            else if (User.IsInRole(nameof(Roles.Uzivatel)))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null &&
                    (zasilka.OdesilatelId == currentUser.Id || zasilka.PrijemceId == currentUser.Id))
                {
                    isAuthorized = true;
                }
            }
            // 4. Má zásilku v seznamu autorizovaných? (přišel přes Track formulář)
            else
            {
                var authorizedPackages = HttpContext.Session.GetObject<List<int>>("AuthorizedPackages");
                if (authorizedPackages != null && authorizedPackages.Contains(id))
                {
                    isAuthorized = true;
                }
            }

            // 5. Není autorizován? → Přesměruj na Track
            if (!isAuthorized)
            {
                return RedirectToAction("Track");
            }

            // Načtení souvisejících uživatelů
            var odesilatel = await _userManager.FindByIdAsync(zasilka.OdesilatelId.ToString());
            var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());
            User? kuryr = null;
            if (zasilka.KuryrId.HasValue)
            {
                kuryr = await _userManager.FindByIdAsync(zasilka.KuryrId.Value.ToString());
            }

            // Načti historii zásilky
            var historie = _packageHistoryAppService.GetHistoryForPackage(id);

            // Načti všechny možné stavy
            var vsechnyStavy = _packageAppService.GetAllStates().OrderBy(s => s.Id).ToList();
            var historieStavIds = historie.Select(h => h.StavId).ToHashSet();

            // Filtruj stavy - zahrnuj jen ty, které:
            // 1. Jsou v historii NEBO
            // 2. Nemají název "Reklamováno"
            vsechnyStavy = vsechnyStavy
                .Where(s => historieStavIds.Contains(s.Id) || !s.Stav.Contains("Reklamováno"))
                .ToList();

            // Předej do ViewBag
            ViewBag.Odesilatel = odesilatel;
            ViewBag.Prijemce = prijemce;
            ViewBag.Kuryr = kuryr;
            ViewBag.Historie = historie;
            ViewBag.VsechnyStavy = vsechnyStavy;

            // Zkontroluj, jestli je to příjemce (buď přihlášený nebo přes Track)
            bool isPrijemce = false;

            // 1. Je přihlášený jako příjemce?
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var currentUserForButton = await _userManager.GetUserAsync(User);
                if (currentUserForButton != null && zasilka.PrijemceId == currentUserForButton.Id)
                {
                    isPrijemce = true;
                }
            }
            // 2. Přišel přes Track a zadal email příjemce?
            else
            {
                var authorizedPackages = HttpContext.Session.GetObject<List<int>>("AuthorizedPackages");
                if (authorizedPackages != null && authorizedPackages.Contains(id))
                {
                    // Zkontroluj, jestli email v session odpovídá příjemci
                    var trackedEmail = HttpContext.Session.GetString($"PackageEmail_{id}");
                    if (!string.IsNullOrEmpty(trackedEmail) &&
                        prijemce?.Email != null &&
                        trackedEmail.Equals(prijemce.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        isPrijemce = true;
                    }
                }
            }

            ViewBag.IsRecipient = isPrijemce;


            return View(zasilka);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeAddress(int id)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            // Ověření, že má uživatel oprávnění
            bool isAuthorized = false;

            // 1. Přihlášený příjemce s rolí Uzivatel
            if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole(nameof(Roles.Uzivatel)))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && zasilka.PrijemceId == currentUser.Id)
                {
                    isAuthorized = true;
                }
            }
            // 2. Nepřihlášený uživatel, který přišel přes Track s emailem příjemce
            else
            {
                var authorizedPackages = HttpContext.Session.GetObject<List<int>>("AuthorizedPackages");
                var trackedEmail = HttpContext.Session.GetString($"PackageEmail_{id}");

                if (authorizedPackages != null && authorizedPackages.Contains(id) && !string.IsNullOrEmpty(trackedEmail))
                {
                    var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());
                    if (prijemce != null && trackedEmail.Equals(prijemce.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        isAuthorized = true;
                    }
                }
            }

            if (!isAuthorized)
            {
                return RedirectToAction("Track");
            }

            // Načti příjemce pro zobrazení
            var prijemceUser = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());

            var viewModel = new ChangeAddressViewModel
            {
                ZasilkaId = zasilka.Id,
                Cislo = zasilka.Cislo,
                DestinaceUlice = zasilka.DestinaceUlice,
                DestinaceCP = zasilka.DestinaceCP,
                DestinaceMesto = zasilka.DestinaceMesto,
                DestinacePsc = zasilka.DestinacePsc,
                PrijemceJmeno = $"{prijemceUser?.FirstName} {prijemceUser?.LastName}",
                DatumOdeslani = zasilka.DatumOdeslani
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeAddress(ChangeAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var zasilka = _packageAppService.GetById(model.ZasilkaId);

                if (zasilka == null)
                {
                    return NotFound();
                }

                // Stejné ověření jako v GET
                bool isAuthorized = false;

                if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole(nameof(Roles.Uzivatel)))
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser != null && zasilka.PrijemceId == currentUser.Id)
                    {
                        isAuthorized = true;
                    }
                }
                else
                {
                    var authorizedPackages = HttpContext.Session.GetObject<List<int>>("AuthorizedPackages");
                    var trackedEmail = HttpContext.Session.GetString($"PackageEmail_{model.ZasilkaId}");

                    if (authorizedPackages != null && authorizedPackages.Contains(model.ZasilkaId) && !string.IsNullOrEmpty(trackedEmail))
                    {
                        var prijemce = await _userManager.FindByIdAsync(zasilka.PrijemceId.ToString());
                        if (prijemce != null && trackedEmail.Equals(prijemce.Email, StringComparison.OrdinalIgnoreCase))
                        {
                            isAuthorized = true;
                        }
                    }
                }

                if (!isAuthorized)
                {
                    return RedirectToAction("Track");
                }

                // Aktualizuj adresu
                _packageAppService.UpdateAddress(
                    model.ZasilkaId,
                    model.DestinaceUlice,
                    model.DestinaceCP,
                    model.DestinaceMesto,
                    model.DestinacePsc
                );

                TempData["SuccessMessage"] = "Adresa doručení byla úspěšně změněna!";
                return RedirectToAction("Detail", new { id = model.ZasilkaId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při změně adresy: {ex.Message}");
                return View(model);
            }
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

            var result = await _userManager.CreateAsync(novy);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(novy, "Uzivatel");
                return novy;
            }

            throw new Exception($"Nepodařilo se vytvořit uživatele: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

    }
}


