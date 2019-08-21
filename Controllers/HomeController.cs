using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace the_wall.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool emailExists = dbContext.Users.Any(u => u.Email == user.Email);
                if (emailExists)
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                User newUser = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                HttpContext.Session.SetInt32("Userid", newUser.Userid);
                return RedirectToAction("Index", "Wall");
            }
            return View("Index");
        }

        [HttpPost("submitlogin")]
        public IActionResult SubmitLogin(LoginUser userSubmission)
        {
            if (ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                if (userInDb == null)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LogPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                    return View("Index");
                }
                User newUser = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                HttpContext.Session.SetInt32("Userid", newUser.Userid);
                return RedirectToAction("Index", "Wall");
            }
            return View("Index");
        }
        // [HttpGet("TheWall")]
        // public IActionResult TheWall()
        // {
        //     if (HttpContext.Session.GetInt32("Userid") == null)
        //     {
        //         return RedirectToAction("Index");
        //     }
        //     User loggedUser = dbContext.Users.FirstOrDefault(u => u.Userid == HttpContext.Session.GetInt32("Userid"));
        //     ViewBag.loggedUser = loggedUser;
        //     List<Message> AllMessages = dbContext.Messages.ToList();
        //     ViewBag.AllMessages = AllMessages;
        //     return View("TheWall");
        // }
    }
}
