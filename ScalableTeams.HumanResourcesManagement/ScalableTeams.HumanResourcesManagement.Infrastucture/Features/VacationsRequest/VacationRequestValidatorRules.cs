using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

namespace ScalableTeams.HumanResourcesManagement.Infrastucture.Features.VacationsRequest;

public class VacationRequestValidatorRules : AbstractValidator<VacationsRequestInput>
{
    public VacationRequestValidatorRules()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull();

        RuleFor(x => x.Dates)
            .NotEmpty();
    }
}
