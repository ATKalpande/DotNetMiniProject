using DotNetMiniProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DotNetMiniProject.Controllers
{
    public class UserController : Controller
    {
        //

        private readonly ApplicationDBContext _DBContext;
        public UserController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }
      /*  public IActionResult Index(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve user ID claim
            var userNameClaim = User.FindFirstValue(ClaimTypes.Name); // Retrieve user name claim
            ViewBag.UserName = userNameClaim;
            var user = _DBContext.Users.FirstOrDefault(x => x.Id == id);
            //ViewData["UserName"] = user.Username;
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var locationGroups = _DBContext.Fields
                .GroupBy(r => r.Location)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.LocationGroups = locationGroups; // Pass locationGroups to ViewBag

            return View(user);
        }
      */
        public IActionResult MNO(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve user ID claim
            var userNameClaim = User.FindFirstValue(ClaimTypes.Name); // Retrieve user name claim
            ViewBag.UserName = userNameClaim;
            var user = _DBContext.Users.FirstOrDefault(x => x.Id == id);
            //ViewData["UserName"] = user.Username;
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            var locationGroups = _DBContext.Fields
                .GroupBy(r => r.Location)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.LocationGroups = locationGroups; // Pass locationGroups to ViewBag

            return View(user);
        }

        public IActionResult SeeCourses(int id)
        {
            var filed = _DBContext.Fields.FirstOrDefault(x => x.Id == id);
            if (filed == null)
            {

                return NotFound();
            }

            var menu = _DBContext.Courses.FirstOrDefault(x => x.Field_Id == filed.Id);
            if (menu == null)
            {

                return NotFound("Menu not found for this restaurant");
            }


            var menuItems = _DBContext.Courses.Where(m => m.Field_Id == menu.Id).ToList();


            return View(menuItems);
        }




        //
        // GET: UserController





        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
