using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

public class VacationRequestApprovedByManager : IDomainEvent
{
    public VacationRequest VacationRequest { get; private set; }

    public VacationRequestApprovedByManager(VacationRequest vacationRequest)
    {
        VacationRequest = vacationRequest;
    }
}
