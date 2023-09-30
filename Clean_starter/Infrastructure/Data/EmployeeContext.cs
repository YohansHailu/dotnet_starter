using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data;
public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

    public DbSet<Employee> Employees
    {
        get;
        set;
    }
}
