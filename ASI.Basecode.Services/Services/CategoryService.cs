
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public void AddCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name)) throw new ArgumentException("Expense title cannot be null or empty.");
            if (this.CategoryExists(category.UserId, category.Name) != null) throw new Exception("Category already exists.");
            _repository.AddCategory(category);
        }

        public Category CategoryExists(int userId, string name)
        {
            return _repository.CategoryExists(userId, name);
        }
        public void DeleteCategory(int categoryId, int userId)
        {
            _repository.DeleteCategory(categoryId, userId);

        }
        public IQueryable<Category> GetCategories()
        {
            return _repository.GetCategories();
        }
        public IQueryable<Category> GetCategoriesByUserId(int userId)
        {
            return _repository.GetCategoriesByUserId(userId);
        }
        public Category GetCategory(int categoryId)
        {
            return _repository.GetCategory(categoryId);
        }
        public void UpdateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name)) throw new ArgumentException("Category name cannot be null or empty.");

            var categoryExist = this.CategoryExists(category.UserId, category.Name);
            if (categoryExist != null && categoryExist.Id != category.Id) throw new Exception("Category already exists.");
            _repository.UpdateCategory(category);
        }
    }
}
