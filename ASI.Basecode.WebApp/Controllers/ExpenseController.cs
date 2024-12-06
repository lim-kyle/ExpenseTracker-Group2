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
        return View();
    }
    public IActionResult AddExpenses()
    {

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult AddExpenses(Expense expense)
    {

        if (ModelState.IsValid)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                expense.UserId = int.Parse(userIdClaim.Value);
                _expenseService.AddExpense(expense);
                TempData["Success"] = "Expense Added Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        return View("Index", expense);
    }

    public IActionResult UpdateExpenses(string id)
    {

        try
        {
            var op = EncryptionUtility.Decrypt(id);
            var expense = _expenseService.GetExpenseById(int.Parse(op));
            return View(expense);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }


    }

    [HttpPost]
    public IActionResult Edit(Expense expense, string encryptedId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var op = EncryptionUtility.Decrypt(encryptedId);
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                expense.UserId = int.Parse(userIdClaim.Value);
                expense.Id = int.Parse(op);
                _expenseService.UpdateExpense(expense);
                TempData["Success"] = "Expense Updated Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
        TempData["ErrorMessage"] = "All Fields are required";
        return RedirectToAction("Index");
    }

    public IActionResult DeleteConfirmed(string id)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            var op = EncryptionUtility.Decrypt(id);
            _expenseService.DeleteExpense(int.Parse(op), int.Parse(userId.Value));
            TempData["Success"] = "Expense Deleted Successfully";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return RedirectToAction("Index");
    }
}
