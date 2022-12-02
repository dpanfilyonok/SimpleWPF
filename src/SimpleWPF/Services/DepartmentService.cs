using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Models;
using SimpleWPF.Repositories;

namespace SimpleWPF.Services;

public class DepartmentService
{
    private readonly ICrudRepository<Department, int> _departments;

    public DepartmentService(ICrudRepository<Department, int> departments)
    {
        _departments = departments;
    }
    
    public IEnumerable<Department> GetDepartments()
    {
        return _departments.GetAll().Include(d => d.Supervisor).ToList();
    }

    public async Task AddDepartmentAsync(Department department)
    {
        department.SupervisorId = department.Supervisor?.Id;
        department.Supervisor = null;
        await _departments.AddAsync(department);
    }

    public async Task DeleteDepartmentAsync(Department department)
    {
        await _departments.DeleteAsync(department);
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
        department.SupervisorId = department.Supervisor?.Id;
        department.Supervisor = null;
        await _departments.UpdateAsync(department);
    }
}