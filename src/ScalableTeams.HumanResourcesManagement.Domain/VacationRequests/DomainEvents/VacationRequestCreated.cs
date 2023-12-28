using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

public class VacationRequestCreated : IDomainEvent
{
    public VacationRequest VacationRequest { get; private set; }

    public VacationRequestCreated(VacationRequest vacationRequest)
    {
        VacationRequest = vacationRequest;
    }
}
