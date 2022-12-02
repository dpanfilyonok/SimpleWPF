using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Common;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.ViewModels.Interfaces;
using SimpleWPF.Views.Dialogs;

namespace SimpleWPF.ViewModels;

public class OrdersViewModel : ObservableObject, ICrudViewModel<Order>
{
    private readonly ICrudRepository<Order, int> _orders;
    private readonly ICrudRepository<Employee, int> _employees;
    private readonly ICrudRepository<Tag, int> _tags;

    public OrdersViewModel(ICrudRepository<Order, int> orders, ICrudRepository<Employee, int> employees, ICrudRepository<Tag, int> tags)
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
        Items = _orders.GetAll()
            .Include(o => o.Employee)
            .Include(o => o.Tags)
            .ToObservableCollection();
    }
    
    private async Task AddEntityMethod()
    {
        var order = new Order
        {
            Name = "Enter order name...",
        };
        
        var allEmployees = _employees.GetAll().ToList();
        var allTags = _tags.GetAll().ToList();
        var vm = new OrderViewModel(order, allEmployees, allTags);
        var dialog = new OrderEditWindow { DataContext = vm };
        if (dialog.ShowDialog() == true)
        {
            var customTags = 
                vm.CustomTags.Split()
                .Select(tag => new Tag { Name = tag })
                .ToList();

            foreach (var tag in customTags)
            {
                order.Tags.Add(tag);
            }
            
            order.EmployeeId = order.Employee?.Id;
            order.Employee = null;
            
            await _orders.AddAsync(order);
            GetEntitiesMethod();
        }
    }
    
    private async Task RemoveEntityMethod(Order? order)
    {
        if (order != null) await _orders.DeleteAsync(order);
        GetEntitiesMethod();
    }
    
    private async Task UpdateEntityMethod(Order? order)
    {
        if (order == null) return;
        
        var allEmployees = _employees.GetAll().ToList();
        var allTags = _tags.GetAll().ToList();
        var vm = new OrderViewModel(order, allEmployees, allTags);
        var dialog = new OrderEditWindow { DataContext = vm };
        if (dialog.ShowDialog() == true)
        {
            var customTags = 
                vm.CustomTags.Split()
                    .Select(tag => new Tag { Name = tag })
                    .ToList();

            foreach (var tag in customTags)
            {
                await _tags.AddAsync(tag);
                order.Tags.Add(tag);
            }
            
            order.EmployeeId = order.Employee?.Id;
            order.Employee = null;
            
            await _orders.UpdateAsync(order);
            GetEntitiesMethod();
        }
    }
}