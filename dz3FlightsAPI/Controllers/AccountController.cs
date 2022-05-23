using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using dz3FlightsAPI.DB;
using dz3FlightsAPI.Models;
using dz3FlightsAPI.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace dz3FlightsAPI.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;
        public AccountController(UserContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        
        public IActionResult ListFlight()
        {
            return View(db.Flight);
        }
        
        public IActionResult CreateFlight()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateFlight(EditFlightModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (await AddFlight(model.Name, model.Cost, model.Time_Departure, model.Time_Registration))
                return Redirect("/Home/Index");
            else
            {
                ModelState.AddModelError("Username", "Name of flight in use");
                return (View(model));
            }
        }
        
        private async Task<bool> AddFlight(string name, int cost, DateTime time_dep, DateTime time_reg)
        {
            if (db.Flight.Any(p => p.Name == name))
                return false;
            Flight flight = new Flight()
            {
                Name = name,
                Cost = cost,
                Time_Departure = time_dep,
                Time_Registration = time_reg
            };
            await db.Flight.AddAsync(flight);
            await db.SaveChangesAsync();
            return true;
        }
        
        
        public IActionResult EditFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            return View(flight.ToEditFlightModel());
        }
        
        
        [HttpPost]
        public async Task<IActionResult> EditFlight(EditFlightModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            Flight flight = db.Flight.Find(model.Id);
            if (flight != null)
            {
                bool taken = flight.Name != model.Name && db.Flight.Any(p => p.Name == model.Name);
                if (taken)
                {
                    ModelState.AddModelError("Flight", "Flight in use");
                    return (View(model));
                }
                flight.Name = model.Name;
                await db.SaveChangesAsync();
                return Redirect("/Account/ListFlight");
            }
            else
            {
                ModelState.AddModelError("", "Invalid ID");
                return (View(model));
            }
        }
        
        public IActionResult DeleteFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            return View(flight.ToEditFlightModel());
        }
        public async Task<IActionResult> DelFlight(int id)
        {
            Flight flight = db.Flight.Find(id);
            if (flight != null)
            {
                db.Flight.Remove(flight);
                await db.SaveChangesAsync();
            }
            return Redirect("/Account/ListFlight");
        }

    }
}
