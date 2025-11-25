using Microsoft.AspNetCore.Mvc;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Models.Package;
using System.Security.Cryptography.X509Certificates;

namespace DorucovaciSluzba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        IPackageAppService _packageAppService;
        IUserAppService _userAppService;

        public PackageController(IPackageAppService packageAppService, IUserAppService userAppService)
        {
            _packageAppService = packageAppService;
            _userAppService = userAppService;
        }

        public IActionResult Select()
        {
            IList<Zasilka> packages = _packageAppService.Select();
            return View(packages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateZasilkaViewModel model)
        {
            // Validace modelu
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // Najdi nebo vytvoř odesílatele
                var odesilatel = _userAppService.GetOrCreate(
                    model.OdesilatelJmeno,
                    model.OdesilatelPrijmeni,
                    model.OdesilatelEmail,
                    model.OdesilatelUlice,
                    model.OdesilatelCP,
                    model.OdesilatelMesto,
                    model.OdesilatelPsc
                );

                // Najdi nebo vytvoř příjemce
                var prijemce = _userAppService.GetOrCreate(
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
        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Track(TrackPackageViewModel model)
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

                // Přesměruj na detail zásilky
                return RedirectToAction("Detail", new { id = zasilka.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při vyhledávání: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            return View(zasilka);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var zasilka = _packageAppService.GetById(id);

            if (zasilka == null)
            {
                return NotFound();
            }

            // Převeď entitu na ViewModel
            var viewModel = new EditZasilkaViewModel
            {
                Id = zasilka.Id,
                Cislo = zasilka.Cislo,
                DatumOdeslani = zasilka.DatumOdeslani,
                OdesilatelJmeno = $"{zasilka.Odesilatel?.Jmeno} {zasilka.Odesilatel?.Prijmeni}",
                PrijemceJmeno = $"{zasilka.Prijemce?.Jmeno} {zasilka.Prijemce?.Prijmeni}",

                // Adresa jako string (readonly, pro zobrazení)
                DestinaceAdresa = $"{zasilka.DestinaceUlice} {zasilka.DestinaceCP}, {zasilka.DestinaceMesto}, {zasilka.DestinacePsc}",

                // Editovatelné hodnoty
                StavId = zasilka.StavId,
                KuryrId = zasilka.KuryrId,

                // Data pro dropdowny
                DostupneStavy = _packageAppService.GetAllStates().ToList(),
                DostupniKuryri = _packageAppService.GetAllCouriers().ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditZasilkaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Znovu načti data pro dropdowny
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _packageAppService.GetAllCouriers().ToList();
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

                TempData["SuccessMessage"] = $"Zásilka {zasilka.Cislo} byla úspěšně aktualizována!";

                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při aktualizaci: {ex.Message}");
                model.DostupneStavy = _packageAppService.GetAllStates().ToList();
                model.DostupniKuryri = _packageAppService.GetAllCouriers().ToList();
                return View(model);
            }
        }
    }
}


