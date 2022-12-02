using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleWPF.Common;
using SimpleWPF.Models;
using SimpleWPF.Services;
using SimpleWPF.Services.Interfaces;
using SimpleWPF.ViewModels.Interfaces;
using SimpleWPF.Views.Dialogs;

namespace SimpleWPF.ViewModels;

public class EmployeesViewModel : ObservableObject, ICrudViewModel<Employee>
{
    private readonly IEmployeeService _employees;
    private readonly IDepartmentService _departments;

    public EmployeesViewModel(IEmployeeService employees, IDepartmentService departments)
    {
        _employees = employees;
        _departments = departments;
        
        GetCommand = new RelayCommand(GetEntitiesMethod);
        AddCommand = new AsyncRelayCommand(AddEntityMethod);
        RemoveCommand = new AsyncRelayCommand<Employee>(RemoveEntityMethod);
        UpdateCommand = new AsyncRelayCommand<Employee>(UpdateEntityMethod);

        GetEntitiesMethod();
    }
    
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }

    private Employee? _selectedItem;
    public Employee? SelectedItem 
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private ObservableCollection<Employee> _items = null!;
    public ObservableCollection<Employee> Items 
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    public IDictionary<string, string> ColumnsBindings { get; set; } = new Dictionary<string, string>
    {
        {"Name", "Name"},
        {"Surname", "Surname"},
        {"Patronymic", "Patronymic"},
        {"Date Of Birth", "DateOfBirth"},
        {"Gender", "Gender"},
        {"Department", "Department"}
    };

    private void GetEntitiesMethod()
    {
        Items = _employees.GetEmployees().ToObservableCollection();
    }
    
    private async Task AddEntityMethod()
    {
        var employee = new Employee
        {
            Name = "Enter name...",
            Surname = "Enter surname...",
            DateOfBirth = DateTime.Now,
            Gender = Gender.Male
        };
        var dialog = new EmployeeEditWindow
        {
            DataContext = new EmployeeViewModel(employee, _departments.GetDepartments())
        };
        if (dialog.ShowDialog() == true)
        {
            await _employees.AddEmployeeAsync(employee);
            GetEntitiesMethod();
        }
    }
    
    private async Task RemoveEntityMethod(Employee? employee)
    {
        if (employee != null) await _employees.DeleteEmployeeAsync(employee);
        GetEntitiesMethod();
    }
    
    private async Task UpdateEntityMethod(Employee? employee)
    {
        if (employee == null) return;

        var dialog = new EmployeeEditWindow
        {
            DataContext = new EmployeeViewModel(employee, _departments.GetDepartments())
        };
        if (dialog.ShowDialog() == true)
        {
            await _employees.UpdateEmployeeAsync(employee);
            GetEntitiesMethod();
        }
    }
}