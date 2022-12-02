using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWPF.Models;

namespace SimpleWPF.Services.Interfaces;

public interface IDepartmentService
{
    public IEnumerable<Department> GetDepartments();
    public Task AddDepartmentAsync(Department department);
    public Task DeleteDepartmentAsync(Department department);
    public Task UpdateDepartmentAsync(Department department);
}