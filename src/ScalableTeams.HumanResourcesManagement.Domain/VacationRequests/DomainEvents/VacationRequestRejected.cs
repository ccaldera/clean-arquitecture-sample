using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

public class VacationRequestRejected : IDomainEvent
{
    public VacationRequest VacationRequest { get; private set; }

    public VacationRequestRejected(VacationRequest vacationRequest)
    {
        VacationRequest = vacationRequest;
    }
}
