using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

namespace ScalableTeams.HumanResourcesManagement.Domain.Exceptions;

public class BusinessLogicExceptions : BusinessLogicException
{
    public List<Error> Errors { get; private set; }

    public BusinessLogicExceptions(List<Error> validationErrors) : base($"There was {validationErrors.Count} errors detected for this operation.")
    {
        Errors = validationErrors;
    }
}