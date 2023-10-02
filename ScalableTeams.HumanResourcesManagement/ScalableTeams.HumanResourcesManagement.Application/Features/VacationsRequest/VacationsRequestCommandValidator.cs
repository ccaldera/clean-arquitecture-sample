using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

namespace ScalableTeams.HumanResourcesManagement.API.EmployeeEndpoints.Validators;

public class VacationsRequestCommandValidator : AbstractValidator<VacationsRequestCommand>
{
    public VacationsRequestCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.Dates)
            .NotEmpty();
    }
}
