using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.Managers.Models;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.ManagersEndpoints.Validators;

public class GetAllActiveVacationsRequestInputValidator : AbstractValidator<GetAllActiveVacationsRequestInput>
{
    public GetAllActiveVacationsRequestInputValidator()
    {
        RuleFor(x => x.ManagerId)
            .NotNull();
    }
}