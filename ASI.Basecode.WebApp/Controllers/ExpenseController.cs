using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ASI.Basecode.WebApp.Controllers;

public class ExpenseController : Controller
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
    
        return View();
    }

    public IActionResult AddExpenses()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddExpenses(Expense expense, CancellationToken ct) {

        if (ModelState.IsValid)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");


            expense.UserId = int.Parse(userIdClaim.Value);
            await _expenseService.AddExpenseAsync(expense, ct);
            return RedirectToAction("Index");
        }

        TempData["Errors"] = ModelState.ToDictionary(
             kvp => kvp.Key,
             kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
         );
        TempData["Expense"] = expense;

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateExpenses(int id, CancellationToken ct)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id, ct);
        if (expense == null)
        {
            return NotFound();
        }
        return View(expense);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Expense expense, CancellationToken ct)
    {
        if (expense == null)
        {
            ModelState.AddModelError(string.Empty, "Expense cannot be null.");
            return View(expense);
        }
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

        try
        {
            expense.UserId  = int.Parse(userIdClaim.Value);
            await _expenseService.UpdateExpenseAsync(expense, ct);
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(expense);
        }
    }

    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        try
        {
            await _expenseService.DeleteExpenseAsync(id, ct);
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("Index");
        }
    }
}
