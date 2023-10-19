using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;

namespace ScalableTeams.HumanResourcesManagement.Persistence.EntityTypeConfigurations;

public class DepartmentTypeConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name);

        builder
            .Property(x => x.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.HasMany(x => x.Members)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId);
    }
}

