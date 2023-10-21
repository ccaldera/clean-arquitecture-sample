using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.HumanResources.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints.Validators;

public class ManagerReviewRequestValidator : AbstractValidator<ManagerReviewRequest>
{
    public ManagerReviewRequestValidator()
    {
        RuleFor(x => x.ReviewerId)
            .NotEmpty();

        RuleFor(x => x.VacationRequestId)
            .NotEmpty();

        RuleFor(x => x.NewStatus)
            .Must(x => x == ProcessStatus.ManagerApproves || x == ProcessStatus.ManagerRejects);
    }
}
