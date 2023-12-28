using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.ManagerReviewOpenVacationRequests.Models;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints.Validators;

public class GetAllActiveVacationsRequestInputValidator : AbstractValidator<GetOpenVacationsRequestsInput>
{
    public GetAllActiveVacationsRequestInputValidator()
    {
        RuleFor(x => x.ManagerId)
            .NotNull();
    }
}