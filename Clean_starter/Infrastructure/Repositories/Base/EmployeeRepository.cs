using Core.Entities;
using Infrastructure.Data;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;
public class EmployeeRepository: Repository < Employee > , IEmployeeRepository {
    public EmployeeRepository(EmployeeContext employeeContext): base(employeeContext) {}
    
    public async Task < IEnumerable < Employee >> GetEmployeeByLastName(string lastname) {
        return await _employeeContext.Employees.Where(m => m.LastName == lastname).ToListAsync();
    }
    
}