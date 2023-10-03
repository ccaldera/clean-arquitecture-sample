using FluentValidation;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

namespace ScalableTeams.HumanResourcesManagement.Infrastucture.Features.VacationsRequest;

public class VacationsRequestInputValidator : Application.Common.IValidator<VacationsRequestInput>
{
    private readonly IValidator<VacationsRequestInput> validator;

    public VacationsRequestInputValidator(IValidator<VacationsRequestInput> validator)
    {
        this.validator = validator;
    }

    public void ValidateAndThrow(VacationsRequestInput input)
    {
        var result = validator.Validate(input);

        if(result.IsValid)
        {
            return;
        }

        var errors = new List<Error>();

        foreach (var error in result.Errors)
        {
            errors.Add(new Error(error.PropertyName, error.ErrorMessage));
        }

        throw new BusinessLogicExceptions(errors);
    }
}