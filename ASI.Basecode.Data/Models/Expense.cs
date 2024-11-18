using System;
using System.ComponentModel.DataAnnotations;

namespace  ASI.Basecode.Data.Models;

public class Expense
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Expense title is required")]
    public string ExpenseTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Amount field is required.")]
    public decimal Amount { get; set; } = 0;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    // Foreign key for Category
    [Required]

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Foreign key for User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

}
