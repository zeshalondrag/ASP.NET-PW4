using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SORAPC.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SORAPC.Controllers
{
    public class HomeController : Controller
    {

        public PcstoreContext db;

        public HomeController(PcstoreContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> gamingpc()
        {
        
            return View(await db.Catalogs.ToListAsync());
        }

        public async Task<IActionResult> workstation()
        {
            return View(await db.Catalogs.ToListAsync());
        }

        public async Task<IActionResult> server()
        {
            return View(await db.Catalogs.ToListAsync());
        }

        public IActionResult assistance()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Catalog catalogss)
        {
            ViewBag.CategoryId = catalogss.CategoryId;
            db.Catalogs.Add(catalogss);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> details(int? id)
        {
            if (id != null)
            {
                Catalog catalogss = await db.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    ViewBag.CategoryId = catalogss.CategoryId;
                    return View(catalogss);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> edit(int? id)
        {
            if (id != null)
            {
                Catalog catalogss = await db.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    ViewBag.CategoryId = catalogss.CategoryId;
                    return View(catalogss);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> edit(Catalog catalogss)
        {
            db.Catalogs.Update(catalogss);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> confirmdelete(int? id)
        {
            if (id != null)
            {
                Catalog catalogss = await db.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    ViewBag.CategoryId = catalogss.CategoryId;
                    return View(catalogss);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> delete(int? id)
        {
            if (id != null)
            {
                Catalog catalogss = await db.Catalogs.FirstOrDefaultAsync(p => p.IdCatalog == id);
                if (catalogss != null)
                {
                    db.Catalogs.Remove(catalogss);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Authorization(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            var user = db.Users.FirstOrDefault(u => u.Logins == email && u.Passwords == hashedPassword);
            if (user != null)
            {
                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Logins)
                };

                var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Ошибочка";
            return View();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string userSurname, string userName, string userMiddleName, string phone, string logins, string passwords)
        {
            if (db.Users.Any(u => u.Logins == logins))
            {
                ViewBag.ErrorMessage = "Пользователь уже есть!";
                return View();
            }

            string hashedPassword = HashPassword(passwords);

            var users = new User
            {
                UserSurname = userSurname,
                UserName = userName,
                UserMiddleName = userMiddleName,
                Phone = phone,
                Logins = logins,
                Passwords = hashedPassword
            };

            db.Users.Add(users);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
    }
}