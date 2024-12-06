
using ASI.Basecode.Data.Exceptions;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repository;

    public ExpenseService(IExpenseRepository repository)
    {
        _repository = repository;
    }

    public List<Expense> GetExpenses()
    {
        return _repository.GetExpenses();
    }
    public void AddExpense(Expense expense)
    {
        if (string.IsNullOrWhiteSpace(expense.ExpenseTitle))
        {
            throw new ExpenseTitleEmpty("Expense title cannot be null or empty.");
        }
        if (expense.CategoryId == 0)
        {
            throw new CategoryEmpty("Category cannot be null or empty.");
        }
        if (expense.Amount < 0)
        {
            throw new InvalidAmount("Invalid Expense Amount.");
        }
        _repository.AddExpense(expense);
    }



    public Expense GetExpenseById(int expenseId)
    {

        var expense = _repository.GetExpenseById(expenseId);
        return expense;
    }

    public void UpdateExpense(Expense expense)
    {
        if (string.IsNullOrWhiteSpace(expense.ExpenseTitle))
        {
            throw new ExpenseTitleEmpty("Expense title cannot be null or empty.");
        }
        if (expense.CategoryId <= 0)
        {
            throw new CategoryEmpty("Category cannot be null or empty.");
        }
        if (expense.Amount < 0)
        {
            throw new InvalidAmount("Invalid Expense Amount.");
        }
        _repository.UpdateExpense(expense);
    }

    public void DeleteExpense(int expenseId, int userId)
    {
        _repository.DeleteExpense(expenseId,userId);
    }

    public List<Expense> GetUserExpenses(int userId)
    {
        return _repository.GetUserExpenses(userId);
    }
    public List<Expense> FilterExpensesByCategoryAndDate(int userId, int categoryId, DateTime startDate, DateTime endDate)
    {
        return _repository.FilterExpensesByCategoryAndDate(userId, categoryId, startDate, endDate);
    }
}
