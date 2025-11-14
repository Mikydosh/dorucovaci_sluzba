using DorucovaciSluzba.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Select()
        {
            IList<Uzivatel> users = _userAppService.Select();
            return View(users);
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _userAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(UserController.Select));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
