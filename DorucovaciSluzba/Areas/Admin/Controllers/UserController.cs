using DorucovaciSluzba.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using DorucovaciSluzba.Domain.Entities;
using DorucovaciSluzba.Models.User;

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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var uzivatel = _userAppService.GetById(id);

            if (uzivatel == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserViewModel
            {
                Id = uzivatel.Id,
                Jmeno = uzivatel.Jmeno,
                Prijmeni = uzivatel.Prijmeni,
                Email = uzivatel.Email,
                Telefon = uzivatel.Telefon,
                DatumNarozeni = uzivatel.DatumNarozeni,
                Ulice = uzivatel.Ulice,
                CP = uzivatel.CP,
                Mesto = uzivatel.Mesto,
                Psc = uzivatel.Psc,
                TypId = uzivatel.TypId,
                DostupneRole = _userAppService.GetAllUserTypes().ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Znovu načti role pro dropdown
                model.DostupneRole = _userAppService.GetAllUserTypes().ToList();
                return View(model);
            }

            try
            {
                var uzivatel = _userAppService.GetById(model.Id);

                if (uzivatel == null)
                {
                    return NotFound();
                }

                // Aktualizuj data
                uzivatel.Jmeno = model.Jmeno;
                uzivatel.Prijmeni = model.Prijmeni;
                uzivatel.Email = model.Email;
                uzivatel.Telefon = model.Telefon;
                uzivatel.DatumNarozeni = model.DatumNarozeni;
                uzivatel.Ulice = model.Ulice;
                uzivatel.CP = model.CP;
                uzivatel.Mesto = model.Mesto;
                uzivatel.Psc = model.Psc;
                uzivatel.TypId = model.TypId;

                _userAppService.Update(uzivatel);
                return RedirectToAction("Select");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Chyba při aktualizaci: {ex.Message}");
                model.DostupneRole = _userAppService.GetAllUserTypes().ToList();
                return View(model);
            }
        }
    }
}
