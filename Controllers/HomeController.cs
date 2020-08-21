using System;
using Microsoft.AspNetCore.Mvc;
using login_register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace login_register.Controllers
{
    public class HomeController : Controller
    {

        private MyContext _context;

        public HomeController(MyContext context){
            _context = context;
        }

        //Global variables
        


        [HttpGet("")]
        public IActionResult IndexRegister(){
            LogRegVmod vMod = new LogRegVmod();
            return View("Index", vMod);
        }

        // [HttpGet("/login")]
        // public IActionResult Login(){
        //     return View("Login");
        // }

        [HttpPost("/loguserin")]
        public IActionResult LogUserIn(LogRegVmod vMod)
        {
            //validate newUser form
            if(ModelState.IsValid){
                
                //grab user by email from db
                User dbuser = _context.Users.FirstOrDefault(u=>u.Email == vMod.login.Email);
                //if no matching email send back
                if(dbuser == null){
                    ModelState.AddModelError("login.Email", "Invalid Email or Password");
                    return View("Index", vMod);
                }
                //hash incoming password
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                // user.Password = hasher.HashPassword(user, user.Password);
                var result = hasher.VerifyHashedPassword(vMod.login, dbuser.Password, vMod.login.Password);
                if(result!=0){ //0 is failure 1 is success 
                    //put into session
                    HttpContext.Session.SetString("UserId",dbuser.UserId.ToString());
                    return Redirect("Weddings");
                }
                ModelState.AddModelError("login.Email", "Invalid Email or Password");
            }
            //return to page with errors                
            return View("Index", vMod);
        }

        [HttpPost("/register")]
        public IActionResult Register(LogRegVmod vMod)
        {
            //validate newUser form
            if(ModelState.IsValid){
                //check database for existing email
                if(_context.Users.Any(u=>u.Email == vMod.register.Email)){
                    //return to page
                    ModelState.AddModelError("register.Email", "This email is already in use");
                    return View("Index", vMod);
                }
                
                //hashpassword
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                vMod.register.Password = hasher.HashPassword(vMod.register, vMod.register.Password);
                //create user and save
                _context.Users.Add(vMod.register);
                _context.SaveChanges();

                //log in
                //retreive user from DB
                User user = _context.Users.FirstOrDefault(u=>u.Email == vMod.register.Email);
                //put into into session USING variable from database query
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                return Redirect("Weddings");
            }
            //return with errors
            return View("Index", vMod);
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if(UserId!=null){
                ViewBag.UserId = HttpContext.Session.GetString("UserId");
                User user = _context.Users.FirstOrDefault(u=>u.UserId.ToString() == UserId);
                ViewBag.UserName = user.FirstName;
                return View("Success");
            }
            return Redirect("/");
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            //clear user session!!  not clearing for some reasone?????
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}