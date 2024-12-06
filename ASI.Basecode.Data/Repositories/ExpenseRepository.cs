using ASI.Basecode.Data.Exceptions;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories;

public class ExpenseRepository : BaseRepository, IExpenseRepository
{
    public ExpenseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public void AddExpense(Expense expense)
    {
        try
        {
            this.GetDbSet<Expense>().Add(expense);
            UnitOfWork.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException.Message.Contains("FOREIGN KEY") && ex.InnerException.Message.Contains("User"))
            {
                throw new CategoryEmpty("User not found.", ex);
            }
            throw new InvalidOperationException(ex.Message);
        }
    }

    public List<Expense> GetExpenses()
    {
        return this.GetDbSet<Expense>().ToList();
    }

    public void UpdateExpense(Expense expense)
    {
        try
        {
            var existingExpense = this.GetExpenseById(expense.Id);
            if (expense.UserId != existingExpense.UserId)
            {
                throw new InvalidOperationException("User cannot update another user's expense.");
            }
            existingExpense.ExpenseTitle = expense.ExpenseTitle;
            existingExpense.CategoryId = expense.CategoryId;
            existingExpense.Amount = expense.Amount;
            UnitOfWork.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException.Message.Contains("FOREIGN KEY") && ex.InnerException.Message.Contains("Category"))
            {
                throw new CategoryEmpty("Category not found.", ex);
            }
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Expense GetExpenseById(int expenseId)
    {
        return this.GetDbSet<Expense>().FirstOrDefault(c => c.Id == expenseId) ?? throw new ExpenseNotFound($"Expense not found.");
    }

    public void DeleteExpense(int expenseId, int userId)
    {
        var expense = this.GetExpenseById(expenseId);
        if (expense.UserId != userId) throw new InvalidOperationException("User cannot delete another user's expense.");
        this.GetDbSet<Expense>().Remove(expense);
        UnitOfWork.SaveChanges();

    }
    public List<Expense> GetUserExpenses(int userId)
    {
        return this.GetDbSet<Expense>().Where(c => c.UserId == userId).Include(e => e.Category).ToList();
    }
    public List<Expense> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate)
    {
        var query = this.GetDbSet<Expense>()
            .Where(e => e.UserId == userId && e.DateCreated >= startDate && e.DateCreated <= endDate);

        if (categoryId != 0)
        {
            query = query.Where(e => e.CategoryId == categoryId);
        }
        return query.Include(e => e.Category).ToList();
    }
}
