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

public class DepartmentsViewModel : ObservableObject, ICrudViewModel<Department>
{
    private readonly ICrudRepository<Department, int> _departments;
    private readonly ICrudRepository<Employee, int> _employees;

    public DepartmentsViewModel(ICrudRepository<Department, int> departments, ICrudRepository<Employee, int> employees)
    {
        _employees = employees;
        _departments = departments;
        
        GetCommand = new RelayCommand(GetEntitiesMethod);
        AddCommand = new AsyncRelayCommand(AddEntityMethod);
        RemoveCommand = new AsyncRelayCommand<Department>(RemoveEntityMethod);
        UpdateCommand = new AsyncRelayCommand<Department>(UpdateEntityMethod);

        GetEntitiesMethod();
    }
    
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }
    
    private Department? _selectedItem;
    public Department? SelectedItem 
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private ObservableCollection<Department> _items = null!;
    public ObservableCollection<Department> Items 
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }
    
    public IDictionary<string, string> ColumnsBindings { get; set; } = new Dictionary<string, string>
    {
        {"Name", "Name"},
        {"Supervisor", "Supervisor"}
    };
    
    private void GetEntitiesMethod()
    {
        Items = _departments.GetAll().Include(d => d.Supervisor).ToObservableCollection();
    }
    
    private async Task AddEntityMethod()
    {
        var department = new Department
        {
            Name = "Enter name..."
        };
        
        var allEmployees = _employees.GetAll().ToList();
        var dialog = new DepartmentEditWindow { DataContext = new DepartmentViewModel(department, allEmployees) };
        if (dialog.ShowDialog() == true)
        {
            department.SupervisorId = department.Supervisor?.Id;
            department.Supervisor = null;
            await _departments.AddAsync(department);
            GetEntitiesMethod();
        }
    }
    
    private async Task RemoveEntityMethod(Department? department)
    {
        if (department != null) await _departments.DeleteAsync(department);
        GetEntitiesMethod();
    }
    
    private async Task UpdateEntityMethod(Department? department)
    {
        if (department == null) return;
        
        var allEmployees = _employees.GetAll().ToList();
        var dialog = new DepartmentEditWindow { DataContext = new DepartmentViewModel(department, allEmployees) };
        if (dialog.ShowDialog() == true)
        {
            department.SupervisorId = department.Supervisor?.Id;
            department.Supervisor = null;
            await _departments.UpdateAsync(department);
            GetEntitiesMethod();
        }
    }

}