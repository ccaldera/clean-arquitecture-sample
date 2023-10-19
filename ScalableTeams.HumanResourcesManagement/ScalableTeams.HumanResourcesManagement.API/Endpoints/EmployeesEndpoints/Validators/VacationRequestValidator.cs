using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using System;
using System.Linq;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.EmployeesEndpoints.Validators;

public class VacationRequestValidator : AbstractValidator<VacationsRequestInput>
{
    public VacationRequestValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotNull()
            .WithMessage("Employee id cannot be null");

        RuleFor(x => x.Dates)
            .NotEmpty()
            .WithMessage("There must be at least 1 date");

        RuleFor(x => x.Dates)
            .ForEach(x => x.GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(14).Date))
            .WithMessage("You cannot request vacations for the inmediate 14 days.");

        RuleFor(x => x.Dates)
            .ForEach(x => x.LessThanOrEqualTo(DateTime.UtcNow.AddDays(365).Date))
            .WithMessage("You cannot request vacations for the next year");

        RuleFor(x => x.Dates)
            .Must(x => x.GroupBy(x => x.Date).Any(x => x.Count() == 1))
            .WithMessage("There are some duplicated dates in the request.");
    }
}
