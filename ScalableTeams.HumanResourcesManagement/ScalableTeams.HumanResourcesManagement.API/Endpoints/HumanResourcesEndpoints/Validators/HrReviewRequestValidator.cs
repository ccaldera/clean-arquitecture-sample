using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Enums;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.HumanResourcesEndpoints.Validators;

public class HrReviewRequestValidator : AbstractValidator<HrReviewRequest>
{
    public HrReviewRequestValidator()
    {
        RuleFor(x => x.HrEmployeeId)
            .NotEmpty();

        RuleFor(x => x.VacationRequestId)
            .NotEmpty();

        RuleFor(x => x.NewStatus)
            .Must(x => x == ProcessStatus.HrApproves || x == ProcessStatus.HrRejects);
    }
}
