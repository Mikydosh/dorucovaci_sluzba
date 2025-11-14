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
    }
}
