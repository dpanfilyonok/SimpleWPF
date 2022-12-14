using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleWPF.Data;
using SimpleWPF.Models;
using SimpleWPF.Repositories;
using SimpleWPF.Services;
using SimpleWPF.Services.Interfaces;
using SimpleWPF.ViewModels;
using SimpleWPF.Views;

namespace SimpleWPF;

public partial class App : Application
{
    public App()
    {
        Ioc.Default.ConfigureServices(ConfigureServices());
        Ioc.Default.GetRequiredService<ApplicationContext>();
        
        var logger = Ioc.Default.GetRequiredService<ILogger<App>>();
        RegisterGlobalExceptionHandling(logger);
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

        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
        
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, true));
        
        services.AddDbContext<ApplicationContext>(
            options => options.UseNpgsql("Host=localhost;Port=5433;Database=wpf;Username=postgres;Password=root"));
        
        services.AddSingleton<ICrudRepository<Employee, int>, EntityRepository<Employee, int>>();
        services.AddSingleton<ICrudRepository<Department, int>, EntityRepository<Department, int>>();
        services.AddSingleton<ICrudRepository<Order, int>, EntityRepository<Order, int>>();
        services.AddSingleton<ICrudRepository<Tag, int>, EntityRepository<Tag, int>>();

        services.AddSingleton<IEmployeeService, EmployeeService>();
        services.AddSingleton<IDepartmentService, DepartmentService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<ITagService, TagService>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<DepartmentsViewModel>();
        services.AddTransient<OrdersViewModel>();
        
        return services.BuildServiceProvider();
    }
    
    private static void RegisterGlobalExceptionHandling(ILogger<App> log)
    {
        AppDomain.CurrentDomain.UnhandledException += 
            (sender, args) => CurrentDomainOnUnhandledException(args, log);
    }

    private static void CurrentDomainOnUnhandledException(UnhandledExceptionEventArgs args, ILogger<App> log)
    {
        var exception = args.ExceptionObject as Exception;
        var terminatingMessage = args.IsTerminating ? " The application is terminating." : string.Empty;
        var exceptionMessage = exception?.Message ?? "An unmanaged exception occured.";
        var message = string.Concat(exceptionMessage, terminatingMessage);
        log.LogError(exception, message);
    }
}