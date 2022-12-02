using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SimpleWPF.Models;

namespace SimpleWPF.Data;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();  
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.Department)
            .WithMany(department => department.Employees)
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Department>()
            .HasOne(department => department.Supervisor)
            .WithOne(employee => employee.SupervisorOfDepartment)
            .HasForeignKey<Department>(department => department.SupervisorId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Order>()
            .HasOne(order => order.Employee)
            .WithMany(employee => employee.Orders)
            .HasForeignKey(order => order.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}