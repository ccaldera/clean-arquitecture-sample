using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;
using System.Text.Json;

namespace ScalableTeams.HumanResourcesManagement.Persistence.EntityTypeConfigurations;

public class VacationRequestTypeConfiguration : IEntityTypeConfiguration<VacationRequest>
{
    public void Configure(EntityTypeBuilder<VacationRequest> builder)
    {
        builder.ToTable("VacationsRequests");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(x => x.Dates)
            .HasConversion(
                v => JsonSerializer.Serialize(v ?? new List<DateTime>(), (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions?)null) ?? new List<DateTime>());
    }
}