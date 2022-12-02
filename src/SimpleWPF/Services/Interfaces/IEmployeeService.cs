using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWPF.Models;

namespace SimpleWPF.Services.Interfaces;

public interface IEmployeeService
{
    public IEnumerable<Employee> GetEmployees();
    public Task AddEmployeeAsync(Employee employee);
    public Task DeleteEmployeeAsync(Employee employee);
    public Task UpdateEmployeeAsync(Employee employee);
}