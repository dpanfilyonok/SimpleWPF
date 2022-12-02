using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleWPF.Repositories;
using SimpleWPF.TypeConverters;

namespace SimpleWPF.Models;

public class Order : IEntity<int>
{
    public int Id { get; set; }
    [Required] public string? Name { get; set; }
    
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public List<Tag> Tags { get; set; } = new();
    
    public override string ToString()
    {
        return $"{Name}";
    }
}