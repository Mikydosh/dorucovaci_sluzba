using Microsoft.AspNetCore.Mvc;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Application.Abstraction;
using System.Security.Cryptography.X509Certificates;

namespace DorucovaciSluzba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        IPackageAppService _packageAppService;

        public PackageController(IPackageAppService packageAppService)
        {
            _packageAppService = packageAppService;
        }

        public IActionResult Select()
        {
            IList<Zasilka> packages = _packageAppService.Select();
            return View(packages);
        }
    }
}
