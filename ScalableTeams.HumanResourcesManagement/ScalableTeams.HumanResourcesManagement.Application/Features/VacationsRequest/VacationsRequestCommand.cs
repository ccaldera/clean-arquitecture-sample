using MediatR;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestCommand : IRequest<VacationsRequestResult>
{
    public required Guid EmployeeId { get; init; }
    public required List<DateTime> Dates { get; init; }
}
