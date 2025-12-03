using DorucovaciSluzba.Application.Abstraction;
using DorucovaciSluzba.Application.ViewModels;
using DorucovaciSluzba.Controllers;
using DorucovaciSluzba.Domain.Enums;
using DorucovaciSluzba.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DorucovaciSluzba.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller 
    {
        IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService,
                                    SignInManager<User> signInManager,
                                    UserManager<User> userManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await _accountService.Register(registerVM, Roles.Uzivatel);

                if (errors == null)
                {
                    // Přihlas uživatele přímo bez LoginViewModel
                    var user = await _userManager.FindByEmailAsync(registerVM.Email);

                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });
                    }

                    // Fallback - pokud se nepodaří najít uživatele (nemělo by nastat)
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    //errors to logger
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }

            }

            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                bool isLogged = await _accountService.Login(loginVM);
                if (isLogged)
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });

                loginVM.LoginFailed = true;
            }

            return View(loginVM);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(Login));
        }

    }
}
