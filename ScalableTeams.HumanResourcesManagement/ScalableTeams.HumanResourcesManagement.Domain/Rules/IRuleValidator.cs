namespace ScalableTeams.HumanResourcesManagement.Domain.Rules;

public interface IRuleValidator<TRequest>
{
    void ValidateAndThrow(TRequest request);
}
