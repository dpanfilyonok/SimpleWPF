using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimpleWPF.Models.Observables;

public class ObservableEmployee : ObservableValidator
{
    private readonly Employee _employee;

    public ObservableEmployee(Employee employee) => _employee = employee;

    [Required]
    [CustomValidation(typeof(ObservableEmployee), nameof(ValidateNamePart))]
    public string Name
    {
        get => _employee.Name;
        set => SetProperty(_employee.Name, value, _employee, (entity, val) => entity.Name = val, true);
    }
    
    [Required]
    [CustomValidation(typeof(ObservableEmployee), nameof(ValidateNamePart))]
    public string Surname
    {
        get => _employee.Surname;
        set => SetProperty(_employee.Surname, value, _employee, (entity, val) => entity.Surname = val, true);
    }
    
    [CustomValidation(typeof(ObservableEmployee), nameof(ValidateNamePart))]
    public string? Patronymic
    {
        get => _employee.Patronymic;
        set => SetProperty(_employee.Patronymic, value, _employee, (entity, val) => entity.Patronymic = val, true);
    }
    
    public DateTime DateOfBirth
    {
        get => _employee.DateOfBirth;
        set => SetProperty(_employee.DateOfBirth, value, _employee, (entity, val) => entity.DateOfBirth = val);
    }
    
    public Gender Gender
    {
        get => _employee.Gender;
        set => SetProperty(_employee.Gender, value, _employee, (entity, val) => entity.Gender = val);
    }
    
    public Department? Department
    {
        get => _employee.Department;
        set => SetProperty(_employee.Department, value, _employee, (entity, val) => entity.Department = val);
    }
    
    public static ValidationResult ValidateNamePart(string name, ValidationContext context)
    {
        var isValid = name.All(char.IsLetter);
        return isValid ? ValidationResult.Success : new ValidationResult("The name part should contains only letters");
    }
}