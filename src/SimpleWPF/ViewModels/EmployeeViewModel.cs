using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleWPF.Models;
using SimpleWPF.Models.Observables;

namespace SimpleWPF.ViewModels;

public class EmployeeViewModel : ObservableObject
{
    public EmployeeViewModel(Employee employee, IReadOnlyList<Department> departments)
    {
        Departments = departments;
        _employee = new ObservableEmployee(employee);
    }
    
    private ObservableEmployee _employee;
    public ObservableEmployee Employee 
    {
        get => _employee;
        set => SetProperty(ref _employee, value);
    }
    
    public IReadOnlyList<Department> Departments { get; }
}