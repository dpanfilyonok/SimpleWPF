using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleWPF.Models.Observables;

public class ObservableDepartment : ObservableValidator
{
    private readonly Department _department;

    public ObservableDepartment(Department department) => _department = department;

    [Required]
    public string Name
    {
        get => _department.Name;
        set => SetProperty(_department.Name, value, _department, (entity, val) => entity.Name = val, true);
    }
    
    public Employee? Supervisor
    {
        get => _department.Supervisor;
        set => SetProperty(_department.Supervisor, value, _department, (entity, val) => entity.Supervisor = val);
    }
}