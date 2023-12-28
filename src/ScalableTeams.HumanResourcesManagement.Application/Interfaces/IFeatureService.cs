namespace ScalableTeams.HumanResourcesManagement.Application.Interfaces;

public interface IFeatureService<TInput, TResult>
{
    Task<TResult> Execute(TInput input, CancellationToken cancellationToken);
}

public interface IFeatureService<TInput>
{
    Task Execute(TInput input, CancellationToken cancellationToken);
}