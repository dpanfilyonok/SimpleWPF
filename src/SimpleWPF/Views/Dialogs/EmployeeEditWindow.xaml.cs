using System.Windows;

namespace SimpleWPF.Views.Dialogs;

public partial class EmployeeEditWindow : Window
{
    public EmployeeEditWindow()
    {
        InitializeComponent();
    }

    private void OkButtonEventHandler(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}