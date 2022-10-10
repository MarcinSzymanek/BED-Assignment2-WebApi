using Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Assignment2.Data;

public class DataContext :DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Model> Models => Set<Model>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Expense> Expenses => Set<Expense>();
}