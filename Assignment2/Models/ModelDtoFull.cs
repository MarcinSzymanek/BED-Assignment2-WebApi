namespace Assignment2.Models;

public class ModelDtoFull
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }

    public string? AddresLine1 { get; set; }
    public string? AddresLine2 { get; set; }

    public string? Zip { get; set; }

    public string? City { get; set; }

    public DateTime BirthDay { get; set; }
    public double Height { get; set; }
    public int ShoeSize { get; set; }
    public string? HairColor { get; set; }
    public string? Comments { get; set; }

    public List<JobDtoSimple>? Jobs { get; set; }
    public List<ExpenseDto>? Expenses { get; set; }
}