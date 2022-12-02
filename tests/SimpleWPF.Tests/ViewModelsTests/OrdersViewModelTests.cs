using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SimpleWPF.Models;
using SimpleWPF.Services.Interfaces;
using SimpleWPF.ViewModels;
using Xunit;

namespace SimpleWPF.Tests.ViewModelsTests;

public class OrdersViewModelTests:  IAsyncLifetime
{
    private readonly Mock<IOrderService> _orderServiceMock = new();
    private readonly Mock<IEmployeeService> _employeeServiceMock = new();
    private readonly Mock<ITagService> _tagServiceMock = new();
    private OrdersViewModel _vm = null!;

    private readonly List<Order> _orders = new()
    {
        new Order {Id = 1},
        new Order {Id = 2},
        new Order {Id = 3},
    };

    public Task InitializeAsync()
    {
        _vm = new OrdersViewModel(_orderServiceMock.Object, _employeeServiceMock.Object, _tagServiceMock.Object);
        
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
        _vm.Items.Should().BeEquivalentTo(_orders);
    }
    
    private void ConfigureEmployeeServiceMock()
    {
        _orderServiceMock
            .Setup(orderService => orderService.GetOrders())
            .Returns(_orders);
    }
}