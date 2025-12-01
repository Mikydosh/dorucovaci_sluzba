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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Security" });
            }

            var zasilky = _packageAppService.SelectForKuryr(user.Id);

            return View(zasilky);
        }
    }
}