using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

namespace ScalableTeams.HumanResourcesManagement.Infrastucture.Features.VacationsRequest;

public class VacationRequestValidator : AbstractValidator<VacationsRequestInput>
{
    public VacationRequestValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull();

        RuleFor(x => x.Dates)
            .NotEmpty();
    }
}
