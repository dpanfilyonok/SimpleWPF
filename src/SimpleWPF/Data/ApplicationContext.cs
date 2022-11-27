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
        Database.EnsureDeleted(); 
        Database.EnsureCreated();  
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;Password=root");
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
    }
}