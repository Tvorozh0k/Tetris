using BL;
using Interfaces;
using Tetris.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tetris.Controllers
{
    public class UsersController : Controller
    {
        private IUsersBL _bl;

        public UsersController(IUsersBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = _bl.GetByLogin(loginModel.Login);

            if (user != null && user.Password == loginModel.Password)
            {
                var identity = new CustomUserIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Game", "Home");
            }
            else
            {
                ModelState.AddModelError("Login", "Неверный логин или пароль");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model.Name == null)
            {
                ModelState.AddModelError("Name", "Введите логин");
                return View();
            }

            if (model.Age == null || model.Age < 7 || model.Age > 99)
            {
                ModelState.AddModelError("Age", "Возрастной диапазон: 7-99");
                return View();
            }

            if (model.Phone == null)
            {
                ModelState.AddModelError("Phone", "Введите номер телефона");
                return View();
            }

            if (model.Password == null)
            {
                ModelState.AddModelError("Password", "Введите пароль");
                return View();
            }

            if (model.Password != model.PasswordConfirm)
            {
                ModelState.AddModelError("PasswordConfirm", "Пароли не совпадают");
                return View();
            }

            var user = _bl.GetByLogin(model.Name);

            if (user == null)
            {
                var newUser = new Entities.User()
                {
                    Name = model.Name,
                    Age = model.Age ?? default(int),
                    Phone = model.Phone,
                    Password = model.Password
                };

                _bl.Add(newUser);

                var identity = new CustomUserIdentity(newUser);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Game", "Home");
            }
            else
            {
                ModelState.AddModelError("Name", "Пользователь с таким логином уже существует");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);          
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Get(int id)
        {
            var user = _bl.GetById(id);

            if (user != null)
            {
                return View(new UserModel() { Id = user.Id, FullName = $"{user.Name} {user.Phone}" });
            }
            else
            {
                return View();
            }

        }

    }
}