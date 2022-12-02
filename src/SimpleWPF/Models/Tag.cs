using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleWPF.Repositories;

namespace SimpleWPF.Models;

public class Tag : IEntity<int>
{
    public int Id { get; set; }
    [Required] public string? Name { get; set; }

    public List<Order> Orders { get; set; } = new();
    
    public override string ToString()
    {
        return $"{Name}";
    }
}