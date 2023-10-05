using FluentValidation.Results;
using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScalableTeams.HumanResourcesManagement.API.Common.Models;

public class ValidationErrorResponse : ErrorResponse
{
    public ValidationErrorResponse(IEnumerable<ValidationFailure> errors)
        : base("Validation errors detected!")
    {
        Errors = errors;
    }

    public ValidationErrorResponse(IEnumerable<Error> errors)
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
