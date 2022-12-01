using System;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Models;

namespace SimpleWPF.Data;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    public ApplicationContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureDeleted(); 
        Database.EnsureCreated();  
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;Password=root");
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Department)
            .WithMany(department => department.Employees)
            .HasForeignKey(employee => employee.DepartmentId);
        
        modelBuilder.Entity<Department>()
            .HasOne(department => department.Supervisor)
            .WithOne(employee => employee.SupervisorOfDepartment)
            .HasForeignKey<Department>(department => department.SupervisorId);
        
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "D1" },
            new Department { Id = 2, Name = "D2" }
        );
        
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "A1", Surname = "B1", Gender = Gender.Male, DepartmentId = 1 },
            new Employee { Id = 2, Name = "A2", Surname = "B2", Gender = Gender.Female, DepartmentId = 1 },
            new Employee { Id = 3, Name = "A3", Surname = "B3", Gender = Gender.Male, DepartmentId = 2 }
        );
    }
}