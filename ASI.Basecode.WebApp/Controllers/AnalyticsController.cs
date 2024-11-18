using ASI.Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExpendseTrends()
        {
            var expenseTrendsViewModel = new ExpenseTrendsViewModel
            {
                Months = new List<string> { "November", "October", "September", "August" },
                Groceries = new List<int> { 200, 150, 100, 180 },
                Bills = new List<int> { 300, 250, 200, 280 },
                Allowance = new List<int> { 150, 120, 180, 140 },
                TotalAmount = new List<int> { 650, 520, 480, 600 }
            };

            return View(expenseTrendsViewModel);
        }
    }
}
