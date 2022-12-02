using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleWPF.Common;
using SimpleWPF.Models;
using SimpleWPF.Services;
using SimpleWPF.ViewModels.Interfaces;
using SimpleWPF.Views.Dialogs;

namespace SimpleWPF.ViewModels;

public class OrdersViewModel : ObservableObject, ICrudViewModel<Order>
{
    private readonly OrderService _orders;
    private readonly EmployeeService _employees;
    private readonly TagService _tags;

    public OrdersViewModel(OrderService orders, EmployeeService employees, TagService tags)
    {
        _orders = orders;
        _employees = employees;
        _tags = tags;
        
        GetCommand = new RelayCommand(GetEntitiesMethod);
        AddCommand = new AsyncRelayCommand(AddEntityMethod);
        RemoveCommand = new AsyncRelayCommand<Order>(RemoveEntityMethod);
        UpdateCommand = new AsyncRelayCommand<Order>(UpdateEntityMethod);

        GetEntitiesMethod();
    }
    
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }

    private Order? _selectedItem;
    public Order? SelectedItem 
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private ObservableCollection<Order> _items = null!;
    public ObservableCollection<Order> Items 
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    public IDictionary<string, string> ColumnsBindings { get; set; } = new Dictionary<string, string>
    {
        {"Name", "Name"},
        {"Employee", "Employee"},
        {"Tags", "Tags"}
    };

    private void GetEntitiesMethod()
    {
        Items = _orders.GetOrders().ToObservableCollection();
    }
    
    private async Task AddEntityMethod()
    {
        var order = new Order
        {
            Name = "Enter order name...",
        };
        var vm = new OrderViewModel(order, _employees.GetEmployees(), _tags.GetTags());
        var dialog = new OrderEditWindow
        {
            DataContext = vm
        };
        if (dialog.ShowDialog() == true)
        {
            vm.CustomTags.Split()
                .Select(tag => new Tag { Name = tag })
                .ToList()
                .ForEach(tag => order.Tags.Add(tag));

            await _orders.AddOrderAsync(order);
            GetEntitiesMethod();
        }
    }
    
    private async Task RemoveEntityMethod(Order? order)
    {
        if (order != null) await _orders.DeleteOrderAsync(order);
        GetEntitiesMethod();
    }
    
    private async Task UpdateEntityMethod(Order? order)
    {
        if (order == null) return;
        
        var vm = new OrderViewModel(order, _employees.GetEmployees(), _tags.GetTags());
        var dialog = new OrderEditWindow
        {
            DataContext = vm
        };
        if (dialog.ShowDialog() == true)
        {
            vm.CustomTags.Split()
                .Select(tag => new Tag { Name = tag })
                .ToList()
                .ForEach(tag => order.Tags.Add(tag));
            
            await _orders.UpdateOrderAsync(order);
            GetEntitiesMethod();
        }
    }
}