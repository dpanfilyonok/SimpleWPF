using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SimpleWPF.Common;
using SimpleWPF.Models;
using SimpleWPF.Repositories;

namespace SimpleWPF.ViewModels;

public class EmployeesViewModel : ICrudViewModel<Employee>
{
    private readonly ICrudRepository<Employee, int> _employees;

    public EmployeesViewModel(ICrudRepository<Employee, int> employees)
    {
        _employees = employees;
        GetCommand = new RelayCommand(GetEmployeesMethod);
        
        GetCommand.Execute(null);
    }
    
    public ICommand GetCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand UpdateCommand { get; }
    
    public Employee SelectedItem { get; set; }
    public ObservableCollection<Employee> Items { get; set; }

    public IDictionary<string, string> ColumnsBindings { get; set; } = new Dictionary<string, string> {{"s", "Name"}};

    public EmployeesViewModel()
    {
        throw new System.NotImplementedException();
    }

    private void GetEmployeesMethod()
    {
        Items = _employees.GetAll().ToObservableCollection();
        // this.RaisePropertyChanged(() => this.Employees);
        // Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
    }
    
    // private void AddEmployeeMethod(object? employee)
    // {
    //     // Employees = _employees.GetAll().ToObservableCollection();
    //     UserWindow userWindow = new UserWindow(new User());
    //     if (userWindow.ShowDialog() == true)
    //     {
    //         User user = userWindow.User;
    //         db.Users.Add(user);
    //         db.SaveChanges();
    //     }
    // }
}

/*
 * public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      UserWindow userWindow = new UserWindow(new User());
                      if (userWindow.ShowDialog() == true)
                      {
                          User user = userWindow.User;
                          db.Users.Add(user);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User? user = selectedItem as User;
                      if (user == null) return;
 
                      User vm = new User
                      {
                          Id = user.Id,
                          Name = user.Name,
                          Age = user.Age
                      };
                      UserWindow userWindow = new UserWindow(vm);
 
 
                      if (userWindow.ShowDialog() == true)
                      {
                          user.Name = userWindow.User.Name;
                          user.Age = userWindow.User.Age;
                          db.Entry(user).State = EntityState.Modified;
                          db.SaveChanges();
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      User? user = selectedItem as User;
                      if (user == null) return;
                      db.Users.Remove(user);
                      db.SaveChanges();
                  }));
            }
        }
 */