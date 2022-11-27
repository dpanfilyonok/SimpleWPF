using System;
using System.Windows;
using SimpleWPF.Data;

namespace SimpleWPF.Views;

public partial class MainWindow : Window 
{
    public MainWindow()
    {
        try
        {
            var db = new ApplicationContext();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        InitializeComponent();
    }   
}