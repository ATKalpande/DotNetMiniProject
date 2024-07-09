using Castle.Core.Resource;
using DotNetMiniProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotNetMiniProject.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _DBContext;
        public AccountController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        // GET: AccountController

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user = _DBContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ID claim
                        new Claim(ClaimTypes.Name, user.Username) // Name claim
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(claimsPrincipal).Wait();
                    return RedirectToAction("MNO", "User", new { id = user.Id });
                }
                else
                {
                    var admin = _DBContext.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
                    if (admin != null)
                    {
                        var claims = new[]
                         {
                            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()), // ID claim
                         
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        HttpContext.SignInAsync(claimsPrincipal).Wait(); // Sign in user

                        return RedirectToAction("Index", "Admin");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid email or Password");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (!_DBContext.Admins.Any())
                {
                    ModelState.AddModelError(string.Empty, "You can't register until an admin is registered");
                    return View(user);
                }

                var existingUser = _DBContext.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    _DBContext.Users.Add(user);
                    _DBContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "This email already exists");
            }
            _DBContext.SaveChanges();
            return View(user);
        }

        public async Task<ActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
