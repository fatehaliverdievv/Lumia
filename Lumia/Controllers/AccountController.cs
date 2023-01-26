using Lumia.Models;
using Lumia.ViewModels;
using Lumia.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager{ get; }

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM userVM)
        {
            if (!ModelState.IsValid) { return View(); }
            var user = await _userManager.FindByEmailAsync(userVM.UserNameorEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userVM.UserNameorEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "your username or password isn't correct");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "your username or password isn't correct");
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM userVM)
        {
            var existedusername = await _userManager.FindByNameAsync(userVM.Username);
            if (existedusername != null)
            {
                ModelState.AddModelError("Username", "this username already exsist");
                return View();
            }
            if(!ModelState.IsValid) { return View(); }
            AppUser user = new AppUser
            {
                Name = userVM.Name,
                Email = userVM.Email,
                Surname = userVM.Surname,
                UserName = userVM.Username
            };
            var result= await _userManager.CreateAsync(user,userVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", "your username or password isn't correct");
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
