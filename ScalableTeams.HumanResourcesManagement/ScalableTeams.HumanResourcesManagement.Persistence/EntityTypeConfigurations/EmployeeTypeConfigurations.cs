using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Persistence.EntityTypeConfigurations;

public class EmployeeTypeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name);

        builder
            .Property(x => x.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.HasIndex(x => x.LastName);

        builder
            .Property(x => x.LastName)
            .HasMaxLength(120)
            .IsRequired();

        builder.HasMany(x => x.Subordinates)
            .WithOne(x => x.Manager)
            .HasForeignKey(x => x.ManagerId);

        builder.Property(x => x.DepartmentId)
            .IsRequired();
    }
}

