using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.Models;

namespace Posts.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;
    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    // Register Logic

    [HttpPost("users/register")]
    public IActionResult RegisterUser(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        // Hashing Time! 🥔
        PasswordHasher<User> hasher = new PasswordHasher<User>();

        newUser.Password = hasher.HashPassword(newUser, newUser.Password);

        _context.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        return RedirectToAction("AllPosts", "Post");
    }

    // Login Logic

    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser logAttempt)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        User? dbUser = _context.Users.FirstOrDefault(u => u.Email == logAttempt.LogEmail);

        if (dbUser == null)
        {
            ModelState.AddModelError("LogPassword", "Email/Password Not Found");
            return View("Index");
        }

        PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
        PasswordVerificationResult pwCompare = hasher.VerifyHashedPassword(logAttempt, dbUser.Password, logAttempt.LogPassword);
        if (pwCompare == 0)
        {
            ModelState.AddModelError("LogPassword", "Email/Password Not Found");
            return View("Index");
        }

        HttpContext.Session.SetInt32("UserId", dbUser.UserId);
        return RedirectToAction("AllPosts", "Posts");
    }

    [HttpPost("users/logout")]
    public RedirectToActionResult LogOut()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Index");
    }


    [HttpGet("users/profile")]
    public IActionResult Profile()
    {
        int LoggedId = (int)HttpContext.Session.GetInt32("UserId");

        // Fetching the User and what posts they liked.

        User? LoggedUser = _context.Users.Include(u => u.AllPosts).FirstOrDefault(u => u.UserId == LoggedId);


        if (LoggedUser == null)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }

        // Pass data into the view so we can access it!
        return View(LoggedUser);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
