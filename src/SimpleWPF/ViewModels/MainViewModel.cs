using System.Windows.Input;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SimpleWPF.ViewModels.Interfaces;
using SimpleWPF.Views;

namespace SimpleWPF.ViewModels;

public class MainViewModel
{
    public MainViewModel()
    {
        CreateEmployeesWindowCommand = new RelayCommand(() => CreateCrudWindow(Ioc.Default.GetService<EmployeesViewModel>()));
        CreateDepartmentsWindowCommand = new RelayCommand(() => CreateCrudWindow(Ioc.Default.GetService<DepartmentsViewModel>()));
        CreateOrdersWindowCommand = new RelayCommand(() => CreateCrudWindow(Ioc.Default.GetService<OrdersViewModel>()));
    }

    public ICommand CreateEmployeesWindowCommand { get; }
    public ICommand CreateDepartmentsWindowCommand { get; }
    public ICommand CreateOrdersWindowCommand { get; }

    private static void CreateCrudWindow<T>(ICrudViewModel<T>? vm)
    {
        var window = new CrudWindow { DataContext = vm };
        window.Show();
    }
}