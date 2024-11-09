using ASI.Basecode.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASI.Basecode.WebApp.Models
{

    public class ExpensesViewModel
    {
       public IEnumerable<Expense> Expenses { get; }
       public Expense Expense { get; set; }
    }
}
