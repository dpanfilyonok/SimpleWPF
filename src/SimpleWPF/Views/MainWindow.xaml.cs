using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using SimpleWPF.ViewModels;

namespace SimpleWPF.Views;

public partial class MainWindow : Window 
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<MainViewModel>();
    }   
}