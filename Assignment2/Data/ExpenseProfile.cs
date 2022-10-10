using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Data;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<Expense, ExpenseDto>();
    }
}