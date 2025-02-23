using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPm_Gackan1.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace RPm_Gackan1.Controllers
{
	public class ProfileController : Controller
	{
        public FlorenceeContext db;

        public ProfileController(FlorenceeContext context)
        {
            db = context;
        }

		[HttpGet]
		public IActionResult Authorization()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Authorization(string Email, string UserPassword)
        {
            string hashedPassword = HashPassword(UserPassword);
            var user = db.Users.FirstOrDefault(u => u.Email == Email && u.UserPassword == hashedPassword);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserLogin),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Catalog", "Cataloge");
            }
            ViewBag.ErrorMessage = "Неверные учетные данные";
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string UserLogin, string UserPassword, string Email, string PhoneNumber)
        {
            if (db.Users.Any(u => u.Email == Email))
            {
                ViewBag.ErrorMessage = "Пользователь с таким email уже существует";
                return View();
            }

            string hashedPassword = HashPassword(UserPassword);

            var user = new User
            {
                UserLogin = UserLogin,
                UserPassword = hashedPassword,
                Email = Email,
                PhoneNumber = PhoneNumber
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return RedirectToAction("Catalog", "Cataloge");

        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "-").ToLower();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Authorization", "Profile");
        }


    }
}
