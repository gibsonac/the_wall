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
    [Route("TheWall")]
    public class WallController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public WallController(MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User SessionUser = dbContext.Users.FirstOrDefault(u => u.Userid == HttpContext.Session.GetInt32("Userid"));
            ViewBag.loggedUser = SessionUser;
            //THIS ONE WORKS FOR THE PROJECTS REQUIREMENTS
            List<Message> AllMessages = dbContext.Messages
            .OrderByDescending(m => m.Created_At)
            .Include(m => m.Creator)
            .Include(m => m.MessagesComments)
                .ThenInclude(c => c.Commentor)
            .ToList();
            ViewBag.AllMessages = AllMessages;

            //Devon's confirmed method for sorting comments(I have a pic for reference if this messes up)
            // List<Message> AllMessages = dbContext.Messages
            // .Include(m => m.Creator)
            // .Include(m => m.MessagesComments)
            //     .ThenInclude(c => c.Commentor)
            // .Select(m => new Message()
            // {
            //     Messageid = m.Messageid,
            //     Content = m.Content,
            //     Created_At = m.Created_At,
            //     Updated_At = m.Updated_At,
            //     Userid = m.Userid,
            //     Creator = m.Creator,
            //     MessagesComments = m.MessagesComments.OrderByDescending(c => c.Created_At).ToList()
            // })
            // .OrderByDescending(m => m.Created_At)
            // .ToList();
            return View("TheWall");
        }
        [HttpPost("newmessage")]
        public IActionResult NewMessage(Message newMessage)
        {
            User SessionUser = dbContext.Users.FirstOrDefault(u => u.Userid == HttpContext.Session.GetInt32("Userid"));
            if (ModelState.IsValid)
            {
                newMessage.Userid = SessionUser.Userid;
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.loggedUser = SessionUser;
            List<Message> AllMessages = dbContext.Messages
            .OrderByDescending(m => m.Created_At)
            .Include(m => m.Creator)
            .Include(m => m.MessagesComments)
                .ThenInclude(c => c.Commentor)
            .ToList();
            ViewBag.AllMessages = AllMessages;
            return View("TheWall");
        }
        [HttpPost("newcomment/{id}")]
        public IActionResult NewComment(int id, Comment newComment)
        {
            if (ModelState.IsValid)
            {
                newComment.Messageid = id;
                newComment.Userid = (int)HttpContext.Session.GetInt32("Userid");
                dbContext.Comments.Add(newComment);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            User SessionUser = dbContext.Users.FirstOrDefault(u => u.Userid == HttpContext.Session.GetInt32("Userid"));
            ViewBag.loggedUser = SessionUser;
            List<Message> AllMessages = dbContext.Messages
            .OrderByDescending(m => m.Created_At)
            .Include(m => m.Creator)
            .Include(m => m.MessagesComments)
                .ThenInclude(c => c.Commentor)
            .ToList();
            ViewBag.AllMessages = AllMessages;
            return View("TheWall");
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Comment thisComment = dbContext.Comments.FirstOrDefault(c => c.Commentid == id);
            dbContext.Remove(thisComment);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("delete/message/{id}")]
        public IActionResult DeleteMessage(int id)
        {
            Message thisMessage = dbContext.Messages.FirstOrDefault(c => c.Messageid == id);
            dbContext.Remove(thisMessage);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
