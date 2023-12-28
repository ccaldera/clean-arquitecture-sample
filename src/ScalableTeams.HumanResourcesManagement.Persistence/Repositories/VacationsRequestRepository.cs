using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.ValueObjects;
using ScalableTeams.HumanResourcesManagement.Persistence.Extensions;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class VacationsRequestRepository : RepositoryBase, IVacationsRequestRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public VacationsRequestRepository(
        HumanResourcesManagementContext dbContext,
        IEventDispatcher eventDispatcher)
            : base(eventDispatcher)
    {
        this.dbContext = dbContext;
    }

    public async Task<VacationRequest?> Get(Guid id)
    {
        return await dbContext.VacationsRequests.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<VacationsRequestReview> GetAllVacationsRequests()
    {
        return dbContext
            .VacationsRequests
            .Include(x => x.Employee)
            .Select(x => new VacationsRequestReview
            {
                VacationRequestId = x.Id,
                ManagerId = x.Employee.ManagerId,
                EmployeeId = x.EmployeeId,
                EmployeeLastName = x.Employee.LastName,
                EmployeeName = x.Employee.Name,
                HrReviewDate = x.HrReviewDate,
                ManagerReviewDate = x.ManagerReviewDate,
                Status = x.Status,
                Dates = x.Dates
            })
            .AsQueryable();
    }

    public async Task Insert(VacationRequest vacationRequest)
    {
        await dbContext.AddAsync(vacationRequest);
    }

    protected override IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return dbContext.GetDomainEvents();
    }

    protected override async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}
