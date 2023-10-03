using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class VacationsRequestRepository : IVacationsRequestRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public VacationsRequestRepository(HumanResourcesManagementContext context)
    {
        this.dbContext = context;
    }

    public async Task Insert(VacationRequest vacationRequest)
    {
        await dbContext.AddAsync(vacationRequest);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
