using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
    public class ExpenseTrendsViewModel
    {
        public List<string> Months { get; set; }

        // Expenses for each category (Groceries, Bills, Allowance) across the months
        public List<int> Groceries { get; set; }
        public List<int> Bills { get; set; }
        public List<int> Allowance { get; set; }

        // Optionally, you can add total expense fields or other metrics as needed
        public List<int> TotalAmount { get; set; }
    }
}
