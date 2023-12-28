using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

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

        List<DateTime> emptyDatesList = [];

        builder.Property(x => x.Dates)
            .HasConversion(
                x => JsonSerializer.Serialize(x ?? emptyDatesList, (JsonSerializerOptions?)null),
                x => JsonSerializer.Deserialize<List<DateTime>>(x, (JsonSerializerOptions?)null) ?? emptyDatesList);
    }
}
