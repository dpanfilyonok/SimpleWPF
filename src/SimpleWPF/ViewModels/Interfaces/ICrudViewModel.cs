using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimpleWPF.ViewModels.Interfaces;

public interface ICrudViewModel<T> : IItemListViewModel
{
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }

    public T? SelectedItem { get; set; }
    public ObservableCollection<T> Items { get; set; }
    
}