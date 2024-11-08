
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
        public CategoryService(ICategoryRepository repository) {
            _repository = repository;
        }
        public void AddCategory(Category category)
        {
            _repository.AddCategory(category);
        }
        public void DeleteCategory(int categoryId)
        {
            _repository.DeleteCategory(categoryId);
        }
        public IQueryable<Category> GetCategories()
        {
            return _repository.GetCategories();
        }
        public IQueryable<Category> GetCategoriesByUserId(int userId)
        {
            return _repository.GetCategoriesByUserId(userId);
        }
        public Category? GetCategory(int categoryId)
        {
            return _repository.GetCategory(categoryId);
        }
        public void UpdateCategory(Category category)
        {
            _repository.UpdateCategory(category);
        }
    }
}
