using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models.Identity;
using WebNotes.Services;
using WebNotes.ViewModels;

namespace WebNotes.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly WebNoteDbContext _db;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            RoleManager<Role> RoleManager,
            WebNoteDbContext Db)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _RoleManager = RoleManager;
            _db = Db;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            try
            {
                await InitializeIdentityAsync();
            }
            catch (Exception e)
            {
                throw;
            }

            var user = new User
                {                
                   UserName = Model.UserName
                };

            var user1 = _db.Users.Where(o => o.UserName == Model.UserName).FirstOrDefault();
            if (_db.Users.Contains(user1))
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                return View(Model);
            }

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

            if (login_result.Succeeded)
            
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

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
        private async Task InitializeIdentityAsync()
        {

            async Task CheckRole(string RoleName)
            {
                if (await _RoleManager.RoleExistsAsync(RoleName))
                    return;
                else
                {
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                }
            }

            await CheckRole(Role.Users);


        }
    }
}