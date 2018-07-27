using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using beltexam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace beltexam.Controllers
{
    public class HomeController : Controller
    {
        private BeltExamContext _context;
 
        public HomeController(BeltExamContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user){
        //the name of the instance must be different than the asp-for on the form
            if(ModelState.IsValid)
            {
                User exists = _context.Users.SingleOrDefault(thisuser => thisuser.email == user.email);
                if(exists != null){
                    ModelState.AddModelError("Email", "An account with this email already exists!");
                    return View("Index");
                }

                else{
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.password = Hasher.HashPassword(user, user.password);
                    // HttpContext.Session.SetInt32("UserID", user.UserId);
                    user.wallet = 10000;                
                    _context.Add(user);
                    _context.SaveChanges();
                    int id = _context.Users.Where(u => u.email == user.email).Select(u => u.UserId).SingleOrDefault();
                    HttpContext.Session.SetInt32("UserID", id);
                    return RedirectToAction("Welcome");
                }
            }
            else
            {
                Console.WriteLine("form did not submit");
                return View("Index");
    
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email_login, string password_login)
        {
            User checkUser = _context.Users.SingleOrDefault(user => user.email == email_login);
            if (checkUser != null && password_login != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(checkUser, checkUser.password, password_login))
                {
                    Console.WriteLine("logging in");
                    HttpContext.Session.SetInt32("UserID", checkUser.UserId);
                    return RedirectToAction("Welcome");
                }
                else{
                    ModelState.AddModelError("password", "password invalid");
                    Console.WriteLine("can't log in");
                    return View("Index");
                }
            }
            else{
                return View("Index");
            }
            
        }
        [HttpGet]
        [Route("welcome")]
        public IActionResult Welcome()
        {
            int? UserID =  HttpContext.Session.GetInt32("UserID");
            if (UserID != null)
            {
                User currentUser = _context.Users.SingleOrDefault(u => u.UserId == UserID);

                ViewBag.current_user = currentUser;
                ViewBag.allAuctions = _context.Products.Include(x => x.creator).ToList().OrderBy(x => x.ending);
                return View("Welcome");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("newproduct")]
        public IActionResult NewProduct(){
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult New(Product newproduct){
            int? UserID =  HttpContext.Session.GetInt32("UserID");
            if (UserID != null)
            {

            User currentUser = _context.Users.SingleOrDefault(u => u.UserId == UserID);
            newproduct.creator = currentUser;
            newproduct.created_at= DateTime.Now;
            _context.Add(newproduct);
            _context.SaveChanges();

           
            return RedirectToAction("Welcome");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpGet]
        [Route("product/{id}")]
        public IActionResult Bid(int id){
            int? UserID =  HttpContext.Session.GetInt32("UserID");
            if (UserID != null)
            {

                ViewBag.currentAuction = _context.Products.Include(i => i.creator).SingleOrDefault(x => x.ProductId == id);
                // ViewBag.productCreater = _context.Products.Where(x => x.creator)

                return View();
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("placebid/{id}")]
        public IActionResult Placebid(int id){
            int? UserID =  HttpContext.Session.GetInt32("UserID");
            if (UserID != null)
            {
                // Bid newbid= new Bid(){
                //     newbid.UserId
                // };
                return RedirectToAction("Bid");

            }
            else
            {
                return View("Index");
            }            
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
        