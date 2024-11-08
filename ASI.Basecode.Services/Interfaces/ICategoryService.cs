using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategory(int categoryId);
        IQueryable<Category> GetCategories();
        IQueryable<Category> GetCategoriesByUserId(int userId);
        Category? GetCategory(int categoryId); 
        void UpdateCategory(Category category);
    }
}
