using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Extensions;

public static class HumanResourcesManagementContextExtensions
{
    public static IEnumerable<IDomainEvent> GetDomainEvents(this HumanResourcesManagementContext dbContext)
    {
        return dbContext
            .ChangeTracker
            .Entries<Entity>()
            .Where(x => x is not null)
            .SelectMany(x => x.Entity.PopDomainEvents());
    }
}
