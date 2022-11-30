using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SimpleWPF.Models;

namespace SimpleWPF.ViewModels;

public class DepartmentsViewModel : ICrudViewModel<Department>
{
    public IDictionary<string, string> ColumnsBindings { get; set; }
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }
    public Department SelectedItem { get; set; }
    public ObservableCollection<Department> Items { get; set; }
}