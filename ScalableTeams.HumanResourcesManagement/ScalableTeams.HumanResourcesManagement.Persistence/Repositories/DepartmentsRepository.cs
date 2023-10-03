using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public DepartmentsRepository(HumanResourcesManagementContext context)
    {
        this.dbContext = context;
    }
}
