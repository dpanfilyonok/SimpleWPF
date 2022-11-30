using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleWPF.ViewModels;

namespace SimpleWPF.Views;

public partial class MainWindow : Window 
{
    public MainWindow()
    {
        // try
        // {
        //     var db = new ApplicationContext();
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        //     throw;
        // }
        InitializeComponent();
        DataContext = Ioc.Default.GetService<MainViewModel>();
    }   
}