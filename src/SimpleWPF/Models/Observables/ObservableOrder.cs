using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleWPF.Models.Observables;

public class ObservableOrder : ObservableValidator
{
    private readonly Order _order;

    public ObservableOrder(Order order) => _order = order;

    [Required]
    public string Name
    {
        get => _order.Name;
        set => SetProperty(_order.Name, value, _order, (entity, val) => entity.Name = val, true);
    }
    
    public Employee? Employee
    {
        get => _order.Employee;
        set => SetProperty(_order.Employee, value, _order, (entity, val) => entity.Employee = val);
    }
    
    public List<Tag> Tags
    {
        get => _order.Tags;
        set => SetProperty(_order.Tags, value, _order, (entity, val) => entity.Tags = val);
    }
}