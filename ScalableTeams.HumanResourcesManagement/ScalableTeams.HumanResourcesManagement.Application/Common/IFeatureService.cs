namespace ScalableTeams.HumanResourcesManagement.Application.Common;

public interface IFeatureService<TInput, TResult>
{
    Task<TResult> Execute(TInput input, CancellationToken cancellationToken);
}
