using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces;

public interface IExpenseRepository
{
    Task AddExpenseAsync(Expense expense, CancellationToken ct);
    Task<List<Expense>> GetExpensesAsync(CancellationToken ct);
    Task<List<Expense>> GetExpensesAsyncByUserId(int userId);
    Task UpdateExpenseAsync(Expense expense, CancellationToken ct);
    Task<Expense?> GetExpenseByIdAsync(int expenseId, CancellationToken ct);
    Task DeleteExpenseAsync(int expenseId, CancellationToken ct);
}
