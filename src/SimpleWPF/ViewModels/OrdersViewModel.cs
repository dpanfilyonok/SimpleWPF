using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SimpleWPF.Models;

namespace SimpleWPF.ViewModels;

public class OrdersViewModel : ICrudViewModel<Order>
{
    public IDictionary<string, string> ColumnsBindings { get; set; }
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }
    public Order SelectedItem { get; set; }
    public ObservableCollection<Order> Items { get; set; }
}