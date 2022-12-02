using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SimpleWPF.Models;
using SimpleWPF.Models.Observables;

namespace SimpleWPF.ViewModels;

public class DepartmentViewModel : ObservableObject
{
    public DepartmentViewModel(Department department, IEnumerable<Employee> employees)
    {
        _department = new ObservableDepartment(department);
        Employees = employees;
        Department.ErrorsChanged += (sender, args) =>
        {
            HasErrors = Department.HasErrors;
        };
    }
    
    private ObservableDepartment _department;
    public ObservableDepartment Department 
    {
        get => _department;
        set => SetProperty(ref _department, value);
    }
    
    private bool _hasErrors;
    public bool HasErrors
    {
        get => _hasErrors;
        set => SetProperty(ref _hasErrors, value);
    }
    
    public IEnumerable<Employee> Employees { get; }
}
