using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void AddCategory(Category category)
        {
            this.GetDbSet<Category>().Add(category);
            UnitOfWork.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = this.GetDbSet<Category>().FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                this.GetDbSet<Category>().Remove(category);
                UnitOfWork.SaveChanges();
            }
        }

        public IQueryable<Category> GetCategories()
        {
            return this.GetDbSet<Category>();
        }

        public Category? GetCategory(int categoryId)
        {

            return this.GetDbSet<Category>().FirstOrDefault(c => c.Id == categoryId);
        }
        public void UpdateCategory(Category category)
        {
            var existingCategory = this.GetDbSet<Category>().FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                UnitOfWork.SaveChanges();
            }
        }
        public IQueryable<Category> GetCategoriesByUserId(int userId)
        {
            return this.GetDbSet<Category>().Where(c => c.UserId == userId);
        }

    }

}