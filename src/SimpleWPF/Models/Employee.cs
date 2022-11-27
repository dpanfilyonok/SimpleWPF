using System;
using System.ComponentModel.DataAnnotations;
using SimpleWPF.Repositories;

namespace SimpleWPF.Models;

public class Employee : IEntity<int>
{
    public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    
    public Department? SupervisorOfDepartment { get; set; }
}

public enum Gender
{
    Male,
    Female
}