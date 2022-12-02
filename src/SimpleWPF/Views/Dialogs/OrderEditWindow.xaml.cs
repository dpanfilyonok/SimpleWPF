using System.Windows;

namespace SimpleWPF.Views.Dialogs;

public partial class OrderEditWindow : Window
{
    public OrderEditWindow()
    {
        InitializeComponent();
    }
    
    private void OkButtonEventHandler(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}