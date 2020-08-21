using System;
using Microsoft.AspNetCore.Mvc;
using login_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using exam_wedding_practice.Models;


namespace login_register.Controllers
{
    public class WeddingController : Controller
    {

        private MyContext _context;

        public WeddingController(MyContext context){
            _context = context;
        }

        [HttpGet("weddings")]
        public IActionResult Weddings()
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                List<Wedding> weddings = _context.Weddings
                    .Include(w => w.GuestList)
                    .ThenInclude(g => g.User)
                    .ToList();
                User user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == uid);
                WeddingsVmod vmod = new WeddingsVmod{
                    weddings = weddings,
                    user = user
                };
                return View("Weddings", vmod);
            }
            return Redirect("/");
        }

        [HttpGet("new_wedding")]
        public IActionResult NewWedding()
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                User user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == uid);
                Wedding wedding = new Wedding{
                    OwnerId = user.UserId
                };
                return View("NewWedding", wedding);
            }
            return Redirect("/");
        }

        [HttpPost("create_wedding")]
        public IActionResult CreateWedding(Wedding wedding)
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                User user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == uid);
                if(ModelState.IsValid){
                    _context.Add(wedding);
                    _context.SaveChanges();
                    return RedirectToAction("Weddings");
                }
                return View("NewWedding", wedding);
            }
            return Redirect("/");
        }

        [HttpGet("/wedding/{wedId}")]
        public IActionResult Wedding(int wedId)
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                Wedding wedding = _context.Weddings
                .Include(w => w.GuestList)
                .ThenInclude(g => g.User)
                .FirstOrDefault(w => w.WeddingId == wedId);
                return View("Wedding", wedding);
            }
            return Redirect("/");
        }

        [HttpGet("/change_rsvp/{wedId}")]
        public IActionResult ChangeRsvp(int wedId)
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                User user = _context.Users
                    .Include(w => w.GuestAtWedding)
                    .ThenInclude(a => a.Wedding)
                    .FirstOrDefault(u => u.UserId.ToString() == uid);
                Wedding wedding = _context.Weddings.FirstOrDefault(w => w.WeddingId == wedId);

                if(user.GuestAtWedding.Any(w => w.WeddingId == wedId)){
                    Guest guest = _context.Guests.FirstOrDefault(g => g.UserId.ToString() == uid && g.WeddingId == wedId);
                    _context.Remove(guest);
                    _context.SaveChanges();
                }
                else{
                    Guest guest = new Guest{
                        UserId = user.UserId,
                        WeddingId = wedId
                    };
                    _context.Add(guest);
                    _context.SaveChanges();
                }
                return RedirectToAction("Weddings");
            }
            return Redirect("/");
        }

        [HttpGet("/delete_wedding/{wedId}")]
        public IActionResult DeleteWedding(int wedId)
        {
            string uid = HttpContext.Session.GetString("UserId");
            if(uid!=null){
                User user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == uid);
                Wedding wedding = _context.Weddings.FirstOrDefault(w => w.WeddingId == wedId);
                if(user.UserId == wedding.OwnerId){
                    _context.Remove(wedding);
                    _context.SaveChanges();
                }
                return RedirectToAction("Weddings");
            }
            return Redirect("/");
        }
    }
}