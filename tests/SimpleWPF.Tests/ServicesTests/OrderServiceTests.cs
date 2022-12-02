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

public class OrderServiceTests : IAsyncLifetime
{
    private IOrderService _service = null!;
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
        
        var repo = new EntityRepository<Order, int>(_context);
        _service = new OrderService(repo);
    }
    
    [Fact]
    public async void CreateEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Name == "A1");
        var tag = new Tag {Name = "T4"};
        await _context.Tags.AddAsync(tag);
        var newEntity = new Order
        {
            Name = "O3", 
            Employee = employee,
            Tags = {tag}
        };

        var before = _service.GetOrders().Count();
        await _service.AddOrderAsync(newEntity);
        _service.GetOrders().Count().Should().Be(before + 1);
    }
    
    [Fact]
    public async void CreateAndDeleteEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Name == "A1");
        var newEntity = new Order
        {
            Name = "O3", 
            Employee = employee
        };

        var before = _service.GetOrders().Count();
        await _service.AddOrderAsync(newEntity);
        await _service.DeleteOrderAsync(newEntity);
        _service.GetOrders().Count().Should().Be(before);
    }
    
    [Fact]
    public async void CreateAndUpdateEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Name == "A1");
        var newEntity = new Order
        {
            Name = "O3", 
            Employee = employee
        };
        await _service.AddOrderAsync(newEntity);

        var entity = _context.Orders.FirstOrDefault(o => o.Name == "O3");
        entity.Name = "O4";
        await _service.UpdateOrderAsync(entity);
        
        _service.GetOrders().Select(e => e.Name).Should().Contain("O4");
    }
    
    [Fact]
    public async void CreateAndDeleteEmployeeEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Name == "A1");
        var newEntity = new Order
        {
            Name = "O3", 
            Employee = employee
        };
        await _service.AddOrderAsync(newEntity);

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        
        var entity = _context.Orders.FirstOrDefault(o => o.Name == "O3");
        entity.Employee.Should().BeNull();
    }
    
    [Fact]
    public async void CreateAndDeleteTagEntityTest()
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Name == "A1");
        var t4 = new Tag {Name = "T4"};
        var t5 = new Tag {Name = "T5"};
        var newEntity = new Order
        {
            Name = "O3", 
            Employee = employee,
            Tags = {t4, t5}
        };
        await _service.AddOrderAsync(newEntity);
        var before = _context.Orders.Include(o => o.Tags).FirstOrDefault(o => o.Name == "O3")!.Tags.Count;

        _context.Tags.Remove(t4);
        await _context.SaveChangesAsync();

        var entity = _context.Orders.FirstOrDefault(o => o.Name == "O3");
        entity.Tags.Count.Should().Be(before - 1);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    private static async Task PopulateDataAsync(ApplicationContext context)
    {
        var e1 = new Employee {Name = "A1", Surname = "B1", Gender = Gender.Male};
        var e2 = new Employee {Name = "A2", Surname = "B2", Gender = Gender.Female};
        var e3 = new Employee {Name = "A3", Surname = "B3", Gender = Gender.Male};

        var o1 = new Order {Name = "O1", Employee = e1};
        var o2 = new Order {Name = "O2", Employee = e2};

        var t1 = new Tag {Name = "T1", Orders = {o1}};
        var t2 = new Tag {Name = "T2", Orders = {o2}};
        var t3 = new Tag {Name = "T3", Orders = {o1, o2}};

        await context.Employees.AddRangeAsync(e1, e2, e3);
        await context.Orders.AddRangeAsync(o1, o2);
        await context.Tags.AddRangeAsync(t1, t2, t3);
        
        await context.SaveChangesAsync();
    }
}
