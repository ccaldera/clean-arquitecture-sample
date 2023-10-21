using ScalableTeams.HumanResourcesManagement.Domain.Common.ValueObjects;

namespace ScalableTeams.HumanResourcesManagement.Domain.Exceptions;

public class BusinessLogicExceptions : BusinessLogicException
{
    public List<BusinessRuleError> Errors { get; private set; }

    public BusinessLogicExceptions(List<BusinessRuleError> validationErrors) : base($"There was {validationErrors.Count} errors detected for this operation.")
    {
        Errors = validationErrors;
    }
}