using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleWPF.Data;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.ViewModels;
using SimpleWPF.Views;

namespace SimpleWPF;

public partial class App : Application
{
    public App()
    {
        Ioc.Default.ConfigureServices(ConfigureServices());
        Ioc.Default.GetRequiredService<ApplicationContext>();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var window = new MainWindow();
        window.Show();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        
        services.AddSingleton<ApplicationContext>();
        services.AddSingleton<ICrudRepository<Employee, int>, EntityRepository<Employee, int>>();
        services.AddSingleton<ICrudRepository<Department, int>, EntityRepository<Department, int>>();
        services.AddSingleton<ICrudRepository<Order, int>, EntityRepository<Order, int>>();
        services.AddSingleton<ICrudRepository<Tag, int>, EntityRepository<Tag, int>>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<DepartmentsViewModel>();
        services.AddTransient<OrdersViewModel>();
        
        return services.BuildServiceProvider();
    }
}