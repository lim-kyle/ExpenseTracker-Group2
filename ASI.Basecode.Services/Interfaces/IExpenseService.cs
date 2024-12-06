using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces;

public interface IExpenseService
{
    
    void AddExpense(Expense expense);
    List<Expense> GetUserExpenses(int userId);
    List<Expense> GetExpenses();
    List<Expense> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate);
    Expense GetExpenseById(int expenseId);
    void UpdateExpense(Expense expense);
    void DeleteExpense(int expenseId,int userId);
}
