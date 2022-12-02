using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleWPF.Models;
using SimpleWPF.Models.Observables;

namespace SimpleWPF.ViewModels;

public class OrderViewModel : ObservableObject
{
    public OrderViewModel(Order order, IReadOnlyList<Employee> employees, IReadOnlyList<Tag> tags)
    {
        _order = new ObservableOrder(order);
        Employees = employees;
        Tags = tags;
        Order.ErrorsChanged += (sender, args) => { HasErrors = Order.HasErrors; };
    }

    private ObservableOrder _order;
    public ObservableOrder Order
    {
        get => _order;
        set => SetProperty(ref _order, value);
    }
    
    private string _customTags = "";
    public string CustomTags
    {
        get => _customTags;
        set => SetProperty(ref _customTags, value);
    }

    private bool _hasErrors;
    public bool HasErrors
    {
        get => _hasErrors;
        set => SetProperty(ref _hasErrors, value);
    }

    public IReadOnlyList<Employee> Employees { get; }
    public IReadOnlyList<Tag> Tags { get; }
}