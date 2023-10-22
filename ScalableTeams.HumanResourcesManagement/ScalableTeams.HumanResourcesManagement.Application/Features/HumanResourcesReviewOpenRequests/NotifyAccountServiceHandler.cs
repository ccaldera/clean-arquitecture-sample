using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests;

public class NotifyAccountServiceHandler : DomainEventHandlerBase<VacationRequestApprovedByHumanResources>
{
    private readonly IAccountingService accountingService;

    public NotifyAccountServiceHandler(IAccountingService accountingService)
    {
        this.accountingService = accountingService;
    }

    public override async Task Handle(VacationRequestApprovedByHumanResources @event, CancellationToken cancellationToken)
    {
        await accountingService.NotifyVacationsRequest(@event.VacationRequest, cancellationToken);
    }
}
