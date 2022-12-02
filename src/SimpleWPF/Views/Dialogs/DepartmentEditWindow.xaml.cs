using System.Windows;

namespace SimpleWPF.Views.Dialogs;

public partial class DepartmentEditWindow : Window
{
    public DepartmentEditWindow()
    {
        InitializeComponent();
    }

    private void OkButtonEventHandler(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}