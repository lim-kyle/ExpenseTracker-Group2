using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces;

public interface IExpenseService
{
    
    Task AddExpenseAsync(Expense expense, CancellationToken ct);
    Task<List<Expense>> GetExpensesAsync(CancellationToken ct);
    Task<List<Expense>> GetExpensesAsyncByUserId(int userId);
    Task<List<Expense>> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate);
    Task<Expense> GetExpenseByIdAsync(int expenseId, CancellationToken ct);
    Task UpdateExpenseAsync(Expense expense, CancellationToken ct);
    Task DeleteExpenseAsync(int expenseId, CancellationToken ct);
}
