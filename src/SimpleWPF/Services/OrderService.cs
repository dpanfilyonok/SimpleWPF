using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.Services.Interfaces;

namespace SimpleWPF.Services;

public class OrderService : IOrderService
{
    private readonly ICrudRepository<Order, int> _orders;

    public OrderService(ICrudRepository<Order, int> orders)
    {
        _orders = orders;
    }
    
    public IEnumerable<Order> GetOrders()
    {
        return _orders.GetAll()
            .Include(o => o.Employee)
            .Include(o => o.Tags)
            .ToList();
    }

    public async Task AddOrderAsync(Order order)
    {
        order.EmployeeId = order.Employee?.Id;
        order.Employee = null;
        await _orders.AddAsync(order);
    }

    public async Task DeleteOrderAsync(Order order)
    {
        order.EmployeeId = order.Employee?.Id;
        order.Employee = null;
        await _orders.DeleteAsync(order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        order.EmployeeId = order.Employee?.Id;
        order.Employee = null;
        await _orders.UpdateAsync(order);
    }
}