using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWPF.Models;

namespace SimpleWPF.Services.Interfaces;

public interface IOrderService
{
    public IEnumerable<Order> GetOrders();
    public Task AddOrderAsync(Order order);
    public Task DeleteOrderAsync(Order order);
    public Task UpdateOrderAsync(Order order);
}