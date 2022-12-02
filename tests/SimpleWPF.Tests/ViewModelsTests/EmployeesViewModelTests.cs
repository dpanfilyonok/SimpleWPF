using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SimpleWPF.Models;
using SimpleWPF.Services.Interfaces;
using SimpleWPF.ViewModels;
using Xunit;

namespace SimpleWPF.Tests.ViewModelsTests;

public class EmployeesViewModelTests : IAsyncLifetime
{
    private readonly Mock<IEmployeeService> _employeeServiceMock = new();
    private readonly Mock<IDepartmentService> _departmentServiceMock = new();
    private EmployeesViewModel _vm = null!;

    private readonly List<Employee> _employees = new()
    {
        new Employee {Id = 1},
        new Employee {Id = 2},
        new Employee {Id = 3},
    };

    public Task InitializeAsync()
    {
        _vm = new EmployeesViewModel(_employeeServiceMock.Object, _departmentServiceMock.Object);
        
        ConfigureEmployeeServiceMock();
        
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
    
    [Fact]
    public void GetCommandTest()
    {
        _vm.GetCommand.Execute(null);
        _vm.Items.Should().BeEquivalentTo(_employees);
    }
    
    private void ConfigureEmployeeServiceMock()
    {
        _employeeServiceMock
            .Setup(employeeService => employeeService.GetEmployees())
            .Returns(_employees);
    }
}