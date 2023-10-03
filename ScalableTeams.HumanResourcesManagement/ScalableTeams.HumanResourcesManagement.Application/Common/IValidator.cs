namespace ScalableTeams.HumanResourcesManagement.Application.Common;

public interface IValidator<TInput>
{
    void ValidateAndThrow(TInput input);
}
