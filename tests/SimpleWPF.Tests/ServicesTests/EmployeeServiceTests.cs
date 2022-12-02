using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Data;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.Services;
using SimpleWPF.Services.Interfaces;
using Xunit;

namespace SimpleWPF.Tests.ServicesTests;

public class EmployeeServiceTests : IAsyncLifetime
{
    private IEmployeeService _service = null!;
    private ApplicationContext _context = null!;
    private DbContextOptions<ApplicationContext> _dbContextOptions = null!;
    
    public async Task InitializeAsync()
    {
        var dbName = $"Db_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
        _context = new ApplicationContext(_dbContextOptions);
        await PopulateDataAsync(_context);
        
        var repo = new EntityRepository<Employee, int>(_context);
        _service = new EmployeeService(repo);
    }
    
    [Fact]
    public async void CreateEntityTest()
    {
        var department = _context.Departments.FirstOrDefault(d => d.Name == "D1");
        var newEntity = new Employee
        {
            Name = "A4", 
            Surname = "B4", 
            Gender = Gender.Male, 
            Department = department
        };

        var before = _service.GetEmployees().Count();
        await _service.AddEmployeeAsync(newEntity);
        _service.GetEmployees().Count().Should().Be(before + 1);
    }
    
    [Fact]
    public async void CreateAndDeleteEntityTest()
    {
        var department = _context.Departments.FirstOrDefault(d => d.Name == "D1");
        var newEntity = new Employee
        {
            Name = "A4", 
            Surname = "B4", 
            Gender = Gender.Male, 
            Department = department
        };

        var before = _service.GetEmployees().Count();
        await _service.AddEmployeeAsync(newEntity);
        await _service.DeleteEmployeeAsync(newEntity);
        _service.GetEmployees().Count().Should().Be(before);
    }
    
    [Fact]
    public async void CreateAndUpdateEntityTest()
    {
        var department = _context.Departments.FirstOrDefault(d => d.Name == "D1");
        var newEntity = new Employee
        {
            Name = "A4", 
            Surname = "B4", 
            Gender = Gender.Male, 
            Department = department
        };
        await _service.AddEmployeeAsync(newEntity);

        var entity = _context.Employees.FirstOrDefault(e => e.Name == "A4");
        entity.Name = "A5";
        await _service.UpdateEmployeeAsync(entity);
        
        _service.GetEmployees().Select(e => e.Name).Should().Contain("A5");
    }
    
    [Fact]
    public async void CreateAndDeleteDepartmentEntityTest()
    {
        var department = _context.Departments.FirstOrDefault(d => d.Name == "D1");
        var newEntity = new Employee
        {
            Name = "A4", 
            Surname = "B4", 
            Gender = Gender.Male, 
            Department = department
        };
        await _service.AddEmployeeAsync(newEntity);

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        
        var entity = _context.Employees.FirstOrDefault(e => e.Name == "A4");
        entity.Department.Should().BeNull();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    private static async Task PopulateDataAsync(ApplicationContext context)
    {
        var d1 = new Department {Name = "D1"};
        var d2 = new Department {Name = "D2"};

        var e1 = new Employee {Name = "A1", Surname = "B1", Gender = Gender.Male, Department = d1};
        var e2 = new Employee {Name = "A2", Surname = "B2", Gender = Gender.Female, Department = d1, SupervisorOfDepartment = d1};
        var e3 = new Employee {Name = "A3", Surname = "B3", Gender = Gender.Male, Department = d2};

        var o1 = new Order {Name = "O1", Employee = e1};
        var o2 = new Order {Name = "O2", Employee = e2};

        await context.Departments.AddRangeAsync(d1, d2);
        await context.Employees.AddRangeAsync(e1, e2, e3);
        await context.Orders.AddRangeAsync(o1, o2);

        await context.SaveChangesAsync();
    }
}