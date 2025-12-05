using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DorucovaciSluzba.Areas.Delivery.Controllers
{
    [Area("Delivery")]
    [Authorize(Roles = nameof(Roles.Kuryr))]
    public class MyDeliveriesController : Controller
    {
        private readonly IPackageAppService _packageAppService;
        private readonly UserManager<User> _userManager;

        public MyDeliveriesController(
            IPackageAppService packageAppService,
            UserManager<User> userManager)
        {
            _packageAppService = packageAppService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? sortBy = null, string? sortOrder = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Security" });
            }

            var zasilky = _packageAppService.SelectForKuryr(user.Id).ToList();

            // ŘAZENÍ
            sortBy = sortBy?.ToLower() ?? "cislo";
            zasilky = (sortBy, sortOrder?.ToLower()) switch
            {
                ("cislo", "desc") => zasilky.OrderByDescending(z => z.Cislo).ToList(),
                ("cislo", _) => zasilky.OrderBy(z => z.Cislo).ToList(),
                ("datum", "desc") => zasilky.OrderByDescending(z => z.DatumOdeslani).ToList(),
                ("datum", _) => zasilky.OrderBy(z => z.DatumOdeslani).ToList(),
                ("stav", "desc") => zasilky.OrderByDescending(z => z.Stav?.Stav).ToList(),
                ("stav", _) => zasilky.OrderBy(z => z.Stav?.Stav).ToList(),
                _ => zasilky.OrderBy(z => z.Cislo).ToList()
            };

            ViewBag.CurrentSort = sortBy;
            ViewBag.CurrentOrder = sortOrder ?? "asc";

            return View(zasilky);
        }
    }
}