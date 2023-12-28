using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using ScalableTeams.HumanResourcesManagement.Domain.Common.ValueObjects;

namespace ScalableTeams.HumanResourcesManagement.API.Models;

public class ValidationErrorResponse : ErrorResponse
{
    public ValidationErrorResponse(IEnumerable<ValidationFailure> errors)
        : base("Validation errors detected!")
    {
        Errors = errors;
    }

    public ValidationErrorResponse(IEnumerable<BusinessRuleError> errors)
       : base("Validation errors detected!")
    {
        Errors = errors.Select(x => new ValidationFailure
        {
            PropertyName = x.PropertyName,
            ErrorMessage = x.ErrorMessage
        });
    }

    public IEnumerable<ValidationFailure> Errors { get; } = Array.Empty<ValidationFailure>();
}
