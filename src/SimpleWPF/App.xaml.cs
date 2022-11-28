using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SimpleWPF.Models;
using SimpleWPF.Repositories;

namespace SimpleWPF;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }
    
    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ICrudRepository<Employee, int>, EntityRepository<Employee, int>>();
        services.AddSingleton<ICrudRepository<Department, int>, EntityRepository<Department, int>>();
        services.AddSingleton<ICrudRepository<Order, int>, EntityRepository<Order, int>>();
        services.AddSingleton<ICrudRepository<Tag, int>, EntityRepository<Tag, int>>();

        return services.BuildServiceProvider();
    }
}