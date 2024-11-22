using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
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
        public IActionResult UpdateCategory(int id)
        {

            Category? category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (ModelState.IsValid)
            {

                category.UserId = int.Parse(userIdClaim.Value);
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        } 
    }
}
