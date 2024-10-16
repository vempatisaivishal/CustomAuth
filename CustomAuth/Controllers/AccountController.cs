using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity; // Add this using directive
using CustomAuth.Entities;
using CustomAuth.Models;
using System.Security.Claims;

namespace Registration_App.Controllers
{
  public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<UserFA> _passwordHasher;
        public AccountController(AppDbContext appDbcontext)
        {
            _context = appDbcontext;
            _passwordHasher = new PasswordHasher<UserFA>();
        }
        public IActionResult Index()
        {
            return View(_context.TblVishal.ToList());
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserFA account = new UserFA
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    DateOfBirth=model.DateOfBirth,
                    Gender = model.Gender,
                    Skills = model.Skills,
                    Role=model.Role,
                };
                // Hash the password before saving
                account.Password = _passwordHasher.HashPassword(account, model.Password);
                try
                {
                    _context.TblVishal.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully.";
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Please enter unique email or username.");
                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.TblVishal
                    .FirstOrDefault(x => x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail);
                if (user != null)
                {
                    // Verify the password
                    var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim("Name", user.FirstName),
                            new Claim("Email", user.Email),
                            new Claim(ClaimTypes.Role, "User")
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction("SecurePage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username/Email or Password is not correct.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct.");
                }
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            // Fetch the user from the database
            var user = _context.TblVishal.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            // Create the ViewModel with current user data
            var model = new UpdateViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Skills = user.Skills // Assuming Skills is a List<string>
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Fetch the user from the database
                var user = _context.TblVishal.FirstOrDefault(x => x.Id == model.Id);
                if (user == null)
                {
                    return NotFound();
                }
                // Update user details
                user.Email = model.Email;
                user.UserName = model.UserName;
                // Update password if provided
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    user.Password = _passwordHasher.HashPassword(user, model.Password);
                }
                user.Skills = model.Skills; // Update skills if necessary
                try
                {
                    _context.SaveChanges();
                    ViewBag.Message = "Profile updated successfully.";
                    return RedirectToAction("SecurePage");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the profile.");
                }
            }
            return View(model);
        }

        //[HttpGet]
        //[Authorize]
        //public IActionResult Delete(int Id)
        //{
        //    var user = _context.TblVishal.FirstOrDefault(x => x.Id == Id);
        //    //if (user == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View(user);
        //}

        [HttpGet]
        
       
        public IActionResult Delete(int Id)
        {
            var user = _context.TblVishal.FirstOrDefault(x => x.Id == Id);
           
                _context.TblVishal.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
           

            //return NotFound();
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}



