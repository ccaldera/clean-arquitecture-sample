namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;

public interface IRepository
{
    Task SaveChanges(CancellationToken cancellationToken);
}
