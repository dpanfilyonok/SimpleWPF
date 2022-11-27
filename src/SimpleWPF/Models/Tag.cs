using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleWPF.Models;

public class Tag
{
    [Key] public string? Name { get; set; }

    public List<Order> Orders { get; set; } = new();
}