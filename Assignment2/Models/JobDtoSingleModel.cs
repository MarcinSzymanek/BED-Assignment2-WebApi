using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models;

public class JobDtoSingleModel
{
    public string? Customer { get; set; }
    public DateTime StartDate { get; set; }
    public int Days { get; set; }
    [MaxLength(128)]
    public string? Location { get; set; }
    [MaxLength(2000)]
    public string? Comments { get; set; }
    public List<Expense>? Expenses { get; set; }
}