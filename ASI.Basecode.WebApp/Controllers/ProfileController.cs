using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        public ProfileController(IUserService userService)
        {
            this._userService = userService;
        }
        public IActionResult Index()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var user = _userService.GetUser(int.Parse(userIdClaim.Value));
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Index(User user, string NewPassword, string ConfirmPassword)
        {
            
            if (ModelState.IsValid)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                var existingUser = _userService.GetUser(int.Parse(userIdClaim.Value));
               if(existingUser == null)
               {
                     return NotFound();
               }
               if(existingUser.Password != PasswordManager.EncryptPassword(user.Password))
               {
                   
                   TempData["ErrorMessage"] = "Password is incorrect";
                    return View(user);
               }

               if(!string.IsNullOrEmpty(NewPassword)) {
                if(NewPassword == ConfirmPassword)
                {
                    existingUser.Password = PasswordManager.EncryptPassword(NewPassword);
                }else {
                        TempData["ErrorMessage"] = "Password and Confirm Password do not match";
                        return View(user);
                }
               }
               
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                _userService.UpdateUser(existingUser);
              TempData["SuccessMessage"] = "Profile updated successfully";
              return RedirectToAction("Index");
            }

            return View();
        }
    }
}
