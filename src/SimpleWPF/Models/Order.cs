using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleWPF.Repositories;

namespace SimpleWPF.Models;

public class Order : IEntity<int>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Employee? Employee { get; set; }
    
    public List<Tag> Tags { get; set; } = new();
}