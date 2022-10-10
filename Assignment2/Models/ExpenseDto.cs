using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

public class ExpenseDto
{
    public DateTime Date { get; set; }
    public string? Text { get; set; }
    [Column(TypeName = "decimal(9,2)")]
    public decimal Amount { get; set; }
}