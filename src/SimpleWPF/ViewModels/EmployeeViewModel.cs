using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleWPF.Models;
using SimpleWPF.Models.Observables;

namespace SimpleWPF.ViewModels;

public class EmployeeViewModel : ObservableObject
{
    public EmployeeViewModel(Employee employee, IEnumerable<Department> departments)
    {
        _employee = new ObservableEmployee(employee);
        Departments = departments;
        Employee.ErrorsChanged += (sender, args) =>
        {
            HasErrors = Employee.HasErrors;
        };
    }
    
    private ObservableEmployee _employee;
    public ObservableEmployee Employee
    {
        get => _employee;
        set => SetProperty(ref _employee, value);
    }

    private bool _hasErrors;
    public bool HasErrors
    {
        get => _hasErrors;
        set => SetProperty(ref _hasErrors, value);
    }

    public IEnumerable<Department> Departments { get; }
}