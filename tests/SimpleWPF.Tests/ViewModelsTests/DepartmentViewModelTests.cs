using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SimpleWPF.Models;
using SimpleWPF.Services.Interfaces;
using SimpleWPF.ViewModels;
using Xunit;

namespace SimpleWPF.Tests.ViewModelsTests;

public class DepartmentViewModelTests : IAsyncLifetime
{
    private readonly Mock<IDepartmentService> _departmentServiceMock = new();
    private readonly Mock<IEmployeeService> _employeeServiceMock = new();
    private DepartmentsViewModel _vm = null!;

    private readonly List<Department> _departments = new()
    {
        new Department {Id = 1},
        new Department {Id = 2},
        new Department {Id = 3},
    };

    public Task InitializeAsync()
    {
        _vm = new DepartmentsViewModel(_departmentServiceMock.Object, _employeeServiceMock.Object);
        
        ConfigureDepartmentServiceMock();
        
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
        _vm.Items.Should().BeEquivalentTo(_departments);
    }
    
    private void ConfigureDepartmentServiceMock()
    {
        _departmentServiceMock
            .Setup(departmentService => departmentService.GetDepartments())
            .Returns(_departments);
    }
}