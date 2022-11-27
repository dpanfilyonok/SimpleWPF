using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleWPF.Models;

public class Department
{
    public int Id { get; set; }
    [Required] public string? Name { get; set; }
    
    public int? SupervisorId { get; set; }
    public Employee? Supervisor { get; set; } 

    public List<Employee> Employees { get; set; } = new();
}