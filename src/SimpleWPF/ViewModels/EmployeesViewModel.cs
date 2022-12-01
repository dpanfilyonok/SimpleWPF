using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleWPF.Common;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.ViewModels.Interfaces;
using SimpleWPF.Views.Dialogs;

namespace SimpleWPF.ViewModels;

public class EmployeesViewModel : ObservableObject, ICrudViewModel<Employee>
{
    private readonly ICrudRepository<Employee, int> _employees;
    private readonly ICrudRepository<Department, int> _departments;

    public EmployeesViewModel(ICrudRepository<Employee, int> employees, ICrudRepository<Department, int> departments)
    {
        _employees = employees;
        _departments = departments;
        GetCommand = new RelayCommand(GetEmployeesMethod);
        AddCommand = new AsyncRelayCommand(AddEmployeeMethod);
        RemoveCommand = new AsyncRelayCommand<Employee>(RemoveEmployeeMethod, (e) => e != null);
        UpdateCommand = new AsyncRelayCommand<Employee>(UpdateEmployeeMethod, (e) => e != null);
        
        _items = _employees.GetAll().ToObservableCollection();
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

    private ObservableCollection<Employee> _items;
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
        {"Department", "Department.Name"}
    };

    private void GetEmployeesMethod()
    {
        Items = _employees.GetAll().ToObservableCollection();
    }
    
    private async Task AddEmployeeMethod()
    {
        var employee = new Employee
        {
            DateOfBirth = DateTime.Now,
            Gender = Gender.Male
        };
        var allDepartments = new List<Department> {new()};
        var dialog = new EmployeeEditWindow {DataContext = new EmployeeViewModel(employee, allDepartments)};
        if (dialog.ShowDialog() == true)
        {
            MessageBox.Show(employee.Gender.ToString());
            MessageBox.Show(employee.DateOfBirth.ToString());
            MessageBox.Show(employee.Department.Name);
            // await _employees.AddAsync(employee);
        }
    }
    
    private async Task RemoveEmployeeMethod(Employee? employee)
    {
        if (employee != null) await _employees.DeleteAsync(employee);
        GetEmployeesMethod();
    }
    
    private async Task UpdateEmployeeMethod(Employee? employee)
    {
        if (employee != null) await _employees.UpdateAsync(employee);
        GetEmployeesMethod();
    }
}