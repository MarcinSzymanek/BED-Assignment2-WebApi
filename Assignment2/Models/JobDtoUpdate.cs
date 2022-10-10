using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models;

public class JobDtoUpdate
{
    public DateTime StartDate { get; set; }
    public int Days { get; set; }
    public string? Location { get; set; }
    public string? Comments { get; set; }
}