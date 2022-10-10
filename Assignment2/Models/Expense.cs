using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

public class Expense
{
    [Key]
    public long ExpenseId { get; set; }
    [Microsoft.Build.Framework.Required]
    public long ModelId { get; set; }
    [Microsoft.Build.Framework.Required]
    public long JobId { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    public string? Text { get; set; }
    [Column(TypeName = "decimal(9,2)")]
    public decimal Amount { get; set; }
}