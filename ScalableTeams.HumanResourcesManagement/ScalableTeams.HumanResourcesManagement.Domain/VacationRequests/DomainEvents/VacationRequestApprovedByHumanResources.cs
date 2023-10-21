using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

public class VacationRequestApprovedByHumanResources : IDomainEvent
{
    public VacationRequest VacationRequest { get; private set; }

    public VacationRequestApprovedByHumanResources(VacationRequest vacationRequest)
    {
        VacationRequest = vacationRequest;
    }
}
