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

public class DepartmentServiceTests : IAsyncLifetime
{
    private IDepartmentService _service = null!;
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

        var repo = new EntityRepository<Department, int>(_context);
        _service = new DepartmentService(repo);
    }

    [Fact]
    public async void CreateEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(d => d.Name == "A1");
        var newEntity = new Department
        {
            Name = "D3",
            Supervisor = employee
        };

        var before = _service.GetDepartments().Count();
        await _service.AddDepartmentAsync(newEntity);
        _service.GetDepartments().Count().Should().Be(before + 1);
    }

    [Fact]
    public async void CreateAndDeleteEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(d => d.Name == "A1");
        var newEntity = new Department
        {
            Name = "D3",
            Supervisor = employee
        };

        var before = _service.GetDepartments().Count();
        await _service.AddDepartmentAsync(newEntity);
        await _service.DeleteDepartmentAsync(newEntity);
        _service.GetDepartments().Count().Should().Be(before);
    }

    [Fact]
    public async void CreateAndUpdateEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(d => d.Name == "A1");
        var newEntity = new Department
        {
            Name = "D3",
            Supervisor = employee
        };
        await _service.AddDepartmentAsync(newEntity);

        var entity = _context.Departments.FirstOrDefault(d => d.Name == "D3");
        entity.Name = "D4";
        await _service.UpdateDepartmentAsync(entity);

        _service.GetDepartments().Select(d => d.Name).Should().Contain("D4");
    }

    [Fact]
    public async void CreateAndDeleteSupervisorEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(d => d.Name == "A1");
        var newEntity = new Department
        {
            Name = "D3",
            Supervisor = employee
        };
        await _service.AddDepartmentAsync(newEntity);

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        var entity = _context.Departments.FirstOrDefault(d => d.Name == "D3");
        entity.Supervisor.Should().BeNull();
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

        await context.Departments.AddRangeAsync(d1, d2);
        await context.Employees.AddRangeAsync(e1, e2, e3);

        await context.SaveChangesAsync();
    }
}