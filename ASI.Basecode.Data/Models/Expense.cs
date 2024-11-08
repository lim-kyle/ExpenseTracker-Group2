using System;

namespace  ASI.Basecode.Data.Models;

public class Expense
{
    public int Id { get; set; }
    public string ExpenseTitle { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    // Foreign key for Category
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Foreign key for User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

}
