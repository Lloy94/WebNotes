using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models.Identity;
using WebNotes.ViewModels;

namespace WebNotes.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

           
                var user = new User
                {
                    UserName = Model.UserName,
                };


                var register_result = await _UserManager.CreateAsync(user, Model.Password);
                if (register_result.Succeeded)
                {

                    await _UserManager.AddToRoleAsync(user, Role.Users);

                    await _SignInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

               
            

            return View(Model);
        }
        #endregion
        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);


            return View(Model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;
            await _SignInManager.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}