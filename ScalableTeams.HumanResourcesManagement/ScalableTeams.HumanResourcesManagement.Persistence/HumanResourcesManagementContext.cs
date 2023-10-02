using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Persistence.EntityTypeConfigurations;

namespace ScalableTeams.HumanResourcesManagement.Persistence;

public partial class HumanResourcesManagementContext : DbContext
{
    public HumanResourcesManagementContext()
    {
    }

    public HumanResourcesManagementContext(DbContextOptions<HumanResourcesManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<Department> Departments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("hrm");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeTypeConfigurations).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
