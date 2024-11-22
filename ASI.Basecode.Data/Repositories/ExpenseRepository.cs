using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories;

public class ExpenseRepository : BaseRepository, IExpenseRepository
{
    public ExpenseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task AddExpenseAsync(Expense expense, CancellationToken ct)
    {
        await this.GetDbSet<Expense>().AddAsync(expense, ct);
        await UnitOfWork.SaveChangesAsync(ct);
    }

    public async Task<List<Expense>> GetExpensesAsync(CancellationToken ct)
    {
        return await this.GetDbSet<Expense>().ToListAsync(ct);
    }

    public async Task UpdateExpenseAsync(Expense expense, CancellationToken ct)
    {
        var existingExpense = await this.GetDbSet<Expense>().FirstOrDefaultAsync(c => c.Id == expense.Id, ct);
        if (existingExpense != null)
        {
            existingExpense.ExpenseTitle = expense.ExpenseTitle;
            existingExpense.Category = expense.Category;
            existingExpense.Amount = expense.Amount;
            await UnitOfWork.SaveChangesAsync(ct);
        }
    }

    public async Task<Expense?> GetExpenseByIdAsync(int expenseId, CancellationToken ct)
    {
        return await this.GetDbSet<Expense>().FirstOrDefaultAsync(c => c.Id == expenseId, ct);
    }

    public async Task DeleteExpenseAsync(int expenseId, CancellationToken ct)
    {
        var expense = await this.GetDbSet<Expense>().FirstOrDefaultAsync(c => c.Id == expenseId, ct);
        if (expense != null)
        {
            this.GetDbSet<Expense>().Remove(expense);
            await UnitOfWork.SaveChangesAsync(ct);
        }
    }
    public async Task<List<Expense>> GetExpensesAsyncByUserId(int userId)
    {
        return await this.GetDbSet<Expense>().Where(c => c.UserId == userId).Include(e => e.Category).ToListAsync();
    }
    public async Task<List<Expense>> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate) {
        var query = this.GetDbSet<Expense>()
            .Where(e => e.UserId == userId && e.DateCreated >= startDate && e.DateCreated <= endDate);
            
        if (categoryId != 0) {
            query = query.Where(e => e.CategoryId == categoryId);
        }
        return await query.Include(e => e.Category).ToListAsync();
    }
}
