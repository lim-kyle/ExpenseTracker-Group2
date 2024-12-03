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
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            int userId = int.Parse(userIdClaim.Value);
            IQueryable<Category> categories = _categoryService.GetCategoriesByUserId(userId);
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
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
                    bool categoryExists = _categoryService
                        .GetCategoriesByUserId(category.UserId)
                        .Any(c => c.Name == category.Name);

                    if (categoryExists)
                    {
                        TempData["ErrorMessage"] = "A category with the same name already exists.";
                        return RedirectToAction("Index");
                    }

                     _categoryService.AddCategory(category);
                    TempData["Success"] = "Category added successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                    Console.WriteLine(ex); 
                }
            }

            return RedirectToAction("Index"); 
        }
        public IActionResult UpdateCategory(string id)
        {

            var op = EncryptionUtility.Decrypt(id);

            Category category = _categoryService.GetCategory(int.Parse(op));
            category.Id = int.Parse(op);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category Not Found";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (ModelState.IsValid)
            {
                try
                {

                    category.UserId = int.Parse(userIdClaim.Value);
                    _categoryService.UpdateCategory(category);
                    TempData["Success"] = "Category Updated Successfully";
                }catch (Exception e)
                {
                    TempData["ErrorMessage"] = "Category Not Found";
                }
               
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteCategory(string id)
        {
            var op = EncryptionUtility.Decrypt(id);
            var ok = _categoryService.DeleteCategory(int.Parse(op));
            if (ok == "Category not found")
            {
                TempData["ErrorMessage"] = ok;
            } else if (ok == "Error")
            {
                TempData["ErrorMessage"] = "This category is associated with an expense.";
            }
            else
            {
                TempData["Success"] = ok;
            }
            return RedirectToAction("Index");
        } 
    }
}
