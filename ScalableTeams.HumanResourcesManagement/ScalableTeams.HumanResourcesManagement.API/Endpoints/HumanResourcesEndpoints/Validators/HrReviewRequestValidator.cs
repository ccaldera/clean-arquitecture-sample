using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResourcesReviewOpenRequests.Models;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.HumanResourcesEndpoints.Validators;

public class HrReviewRequestValidator : AbstractValidator<HumanResourcesReviewRequestsInput>
{
    public HrReviewRequestValidator()
    {
        RuleFor(x => x.HrEmployeeId)
            .NotEmpty();

        RuleFor(x => x.VacationRequestId)
            .NotEmpty();

        RuleFor(x => x.NewStatus)
            .Must(x => x == VactionRequestsStatus.ApprovedByHumanResources || x == VactionRequestsStatus.RejectedByHumanResources);
    }
}
