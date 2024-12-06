using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace ASI.Basecode.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddCategory()
        {
            return RedirectToAction("Index"); ;
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return RedirectToAction("Login", "Account");
            category.UserId = int.Parse(userIdClaim.Value);

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.AddCategory(category);
                    TempData["Success"] = "Category added successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return View("Index", category);
        }
        public IActionResult UpdateCategory(string id)
        {
            try
            {
                var op = EncryptionUtility.Decrypt(id);
                Category category = _categoryService.GetCategory(int.Parse(op));
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category, string encryptedId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (ModelState.IsValid)
            {
                try
                {
                    var op = EncryptionUtility.Decrypt(encryptedId);
                    category.UserId = int.Parse(userIdClaim.Value);
                    category.Id = int.Parse(op);
                    _categoryService.UpdateCategory(category);
                    TempData["Success"] = "Category Updated Successfully";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }

                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteCategory(string id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            try
            {
                var op = EncryptionUtility.Decrypt(id);
                _categoryService.DeleteCategory(int.Parse(op), int.Parse(userIdClaim.Value));
                TempData["Success"] = "Category Deleted Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
