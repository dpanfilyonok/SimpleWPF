using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleWPF.Models;

public class ObservableEmployee : ObservableObject
{
    private readonly Employee _employee;

    public ObservableEmployee(Employee employee) => _employee = employee;

    public string Name
    {
        get => _employee.Name;
        set => SetProperty(_employee.Name, value, _employee, (e, n) => e.Name = n);
    }
}