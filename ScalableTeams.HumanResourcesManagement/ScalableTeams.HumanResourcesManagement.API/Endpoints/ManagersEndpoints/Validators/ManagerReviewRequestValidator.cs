using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Enums;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints.Validators;

public class ManagerReviewRequestValidator : AbstractValidator<ManagerReviewRequestInput>
{
    public ManagerReviewRequestValidator()
    {
        RuleFor(x => x.ReviewerId)
            .NotEmpty();

        RuleFor(x => x.VacationRequestId)
            .NotEmpty();

        RuleFor(x => x.NewStatus)
            .Must(x => x == VactionRequestsStatus.ApprovedByManager || x == VactionRequestsStatus.RejectedByManager);
    }
}
