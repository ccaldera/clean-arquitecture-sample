using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;

namespace ScalableTeams.HumanResourcesManagement.API.ManagersEndpoints.Validators;

public class GetAllActiveVacationsRequestInputValidator : AbstractValidator<GetAllActiveVacationsRequestInput>
{
    public GetAllActiveVacationsRequestInputValidator()
    {
        RuleFor(x => x.ManagerId)
            .NotNull();
    }
}
