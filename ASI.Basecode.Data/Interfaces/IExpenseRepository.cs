using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Interfaces;

public interface IExpenseRepository
{
    void AddExpense(Expense expense);
    Expense GetExpenseById(int expenseId);

    List<Expense> GetExpenses();
    List<Expense> GetUserExpenses(int userId);
    List<Expense> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate);
    void UpdateExpense(Expense expense);
    void DeleteExpense(int expenseId,int userId);
}