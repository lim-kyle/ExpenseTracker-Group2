using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ASI.Basecode.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreated { get; }
        public DateTime DateTUpdated;
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
