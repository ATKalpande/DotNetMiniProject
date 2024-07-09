using Castle.Core.Resource;
using DotNetMiniProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetMiniProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _DBContext;
        public AdminController(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }
        public IActionResult Index()
        {
            var users = _DBContext.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid) // model bidning main kuch errorto nhi aa rha so chking
            {
                var admin = _DBContext.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
                if (admin != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Users.Add(user);
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var user = _DBContext.Users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            if (ModelState.IsValid && user != null)
            {
                _DBContext.Entry(user).State = EntityState.Modified;
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult DeleteUser(int id)
        {
            var userToDelete = _DBContext.Users.FirstOrDefault(c => c.Id == id);
            if (userToDelete != null)
            {
                _DBContext.Users.Remove(userToDelete);
                _DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //end crud customer 


        [HttpGet]
        public IActionResult AllFields()
        {
            var filed = _DBContext.Fields.ToList();
           // ViewBag.Fields = filed;
            return View(filed);
        }

        [HttpGet]
        public IActionResult AddField()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddField(Field field)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Fields.Add(field);
                _DBContext.SaveChanges();

                //menu 
                var newCourse = new Course { Field_Id = field.Id };
                _DBContext.Courses.Add(newCourse);
                _DBContext.SaveChanges();
                return RedirectToAction("AllFields");
            }
            return View(field);
        }

        [HttpGet]
        public IActionResult UpdateField(int id)
        {
            var resturant = _DBContext.Fields.FirstOrDefault(a => a.Id == id);
            if (resturant == null)
            {
                return NotFound();
            }
            return View(resturant);
        }
        [HttpPost]
        public IActionResult UpdateField(Field field)
        {
            if (ModelState.IsValid)
            {
                _DBContext.Entry(field).State = EntityState.Modified;
                _DBContext.SaveChanges();
                return RedirectToAction("AllFields");
            }
            return View(field);
        }


        public IActionResult DeleteField(int id)
        {
            var fild = _DBContext.Fields.FirstOrDefault(r => r.Id == id);
            if (fild != null)
            {
                _DBContext.Fields.Remove(fild);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("AllFields");
        }
        //end resturant

        [HttpGet]

        public IActionResult EditCourse(int id)
        {
            var courseId = _DBContext.Courses.FirstOrDefault(m => m.Field_Id == id)?.Id;
            if (courseId != null)
            {

                ViewBag.CourseID = courseId;
                TempData["CourseID"] = courseId;
                var menuItems = _DBContext.Subjects.Where(m => m.Course_Id == courseId).ToList();
                return View(menuItems);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult AddSubject()
        {
            //int MenuId = 0; // Default value in case TempData["MenuId"] is null or not convertible to int
            //if (TempData["MenuId"] != null && int.TryParse(TempData["MenuId"].ToString(), out int tempMenuId))
            //{
            //    MenuId = tempMenuId; 
            //    ViewBag.MenuId= MenuId;
            //}
            return View();
        }

        [HttpPost]
        public IActionResult AddSubject(Subjects subjet)
        {
            ModelState.Remove("Menu");

            int CourseID = 0; // Default value in case TempData["MenuId"] is null or not convertible to int
            if (TempData["CourseID"] != null && int.TryParse(TempData["MenuId"].ToString(), out int tempCourseId))
            {
                CourseID = tempCourseId;
                subjet.Course_Id = CourseID;
            }



            if (ModelState.IsValid)
            {
                _DBContext.Subjects.Add(subjet);
                _DBContext.SaveChanges();
                return RedirectToAction("EditMenue", new { id = subjet.Course_Id });
            }
            return View(subjet);
        }

        [HttpGet]
        public IActionResult UpdateSubject(int id)
        {
            var sub = _DBContext.Subjects.FirstOrDefault(a => a.Id == id);
            return View(sub);
        }
        [HttpPost]
        public IActionResult UpdateSubject(Subjects sub)
        {
            //if (ModelState.IsValid)
            //{
            _DBContext.Entry(sub).State = EntityState.Modified;
            _DBContext.SaveChanges();
            return RedirectToAction("EditMenu", new { id = sub.Course_Id });
            //}
            // return View(meal);
        }

        public IActionResult DeleteSubject(int id)
        {
            var sub = _DBContext.Subjects.FirstOrDefault(m => m.Id == id);
            if (sub != null)
            {
                _DBContext.Subjects.Remove(sub);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("EditMenu", new { id = sub.Course_Id });
        }
        public IActionResult AddCourse()
        {
            Subjects subObj = new Subjects();
            subObj.SubName = "BigData";
            subObj.Price = 180;
            subObj.Description = "BegADv";
            subObj.Course_Id = 11;
            return View(subObj);
        }

    }
}
