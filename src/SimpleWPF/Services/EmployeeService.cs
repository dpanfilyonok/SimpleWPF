using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Models;
using SimpleWPF.Repositories;

namespace SimpleWPF.Services;

public class EmployeeService
{
    private readonly ICrudRepository<Employee, int> _employees;

    public EmployeeService(ICrudRepository<Employee, int> employees)
    {
        _employees = employees;
    }
    
    public IEnumerable<Employee> GetEmployees()
    {
        return _employees.GetAll().Include(e => e.Department).ToList();
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        employee.DepartmentId = employee.Department?.Id;
        employee.Department = null;
        await _employees.AddAsync(employee);
    }

    public async Task DeleteEmployeeAsync(Employee employee)
    {
        await _employees.DeleteAsync(employee);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        employee.DepartmentId = employee.Department?.Id;
        employee.Department = null;
        await _employees.UpdateAsync(employee);
    }
}