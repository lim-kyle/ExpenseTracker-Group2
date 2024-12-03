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

    public IActionResult Index(string category, string start, string end)
    {
        ViewBag.Category = category == "" ? category : null;
        ViewBag.Start = start;
        ViewBag.End = end;
        return View();
    }
     public  IActionResult AddExpenses() {

        return RedirectToAction("Index");
     }
    [HttpPost]
    public async Task<IActionResult> AddExpenses(Expense expense, CancellationToken ct) {

        if (ModelState.IsValid)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            expense.UserId = int.Parse(userIdClaim.Value);
            await _expenseService.AddExpenseAsync(expense, ct);
            TempData["Success"] = "Expense Added Successfully";
            return RedirectToAction("Index");
        }
        return View("Index", expense);
    }

    public async Task<IActionResult> UpdateExpenses(string id, CancellationToken ct)
    {

        try
        {
            var op = EncryptionUtility.Decrypt(id);
            var expense = await _expenseService.GetExpenseByIdAsync(int.Parse(op), ct);
            if (expense == null)
            {
                TempData["ErrorMessage"] = "Expense Not Found";
                return RedirectToAction("Index");
            }
            return View(expense);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
       
        
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Expense expense, CancellationToken ct)
    {
        if(ModelState.IsValid)
        {
            if (expense == null)
            {
                ModelState.AddModelError(string.Empty, "Expense not found");
                return View(expense);
            }
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            try
            {
                expense.UserId = int.Parse(userIdClaim.Value);
                await _expenseService.UpdateExpenseAsync(expense, ct);
                TempData["Success"] = "Expense Updated Successfully";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        TempData["ErrorMessage"] = "All Fields are required";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteConfirmed(string id, CancellationToken ct)
    {
        try
        {
            var op = EncryptionUtility.Decrypt(id);
            await _expenseService.DeleteExpenseAsync(int.Parse(op), ct);
            TempData["Success"] = "Expense Deleted Successfully";
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("Index");
        }
    }
}
