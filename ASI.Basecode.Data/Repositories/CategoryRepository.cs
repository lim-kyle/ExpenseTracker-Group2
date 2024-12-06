using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                this.GetDbSet<Category>().Add(category);
                UnitOfWork.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("FOREIGN KEY")) throw new InvalidOperationException("User not found.", ex);
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Category CategoryExists(int userId, string name)
        {
            return this.GetDbSet<Category>().FirstOrDefault(c => c.UserId == userId && c.Name == name);
        }
        public void DeleteCategory(int categoryId, int userId)
        {
            try
            {

                var category = this.GetCategory(categoryId);
                if (category.UserId != userId) throw new Exception("User cannot delete another user's category.");
                this.GetDbSet<Category>().Remove(category);
                UnitOfWork.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint")) throw new InvalidOperationException("Unable to delete this category as it is associated with an existing expense.", ex);
                throw new InvalidOperationException(ex.Message);
            }
        }
        public IQueryable<Category> GetCategories()
        {
            return this.GetDbSet<Category>();
        }
        public Category GetCategory(int categoryId)
        {
            return this.GetDbSet<Category>().FirstOrDefault(c => c.Id == categoryId) ?? throw new Exception("Category not found");
        }
        public void UpdateCategory(Category category)
        {
            var existingCategory = this.GetCategory(category.Id);
            if (category.UserId != existingCategory.UserId) throw new Exception("User cannot update another user's category.");
            try
            {
                existingCategory.Name = category.Name;
                UnitOfWork.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("FOREIGN KEY")) throw new InvalidOperationException("User not found.", ex);
                throw new InvalidOperationException(ex.Message);
            }
        }
        public IQueryable<Category> GetCategoriesByUserId(int userId)
        {
            return this.GetDbSet<Category>().Where(c => c.UserId == userId);
        }

    }

}