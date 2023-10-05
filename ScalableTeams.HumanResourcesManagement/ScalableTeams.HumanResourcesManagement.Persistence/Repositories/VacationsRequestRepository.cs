using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Agreggates;
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

    public IEnumerable<VacationsRequestReview> GetAllVacationsRequests()
    {
        return dbContext
            .VacationsRequests
            .Include(x => x.Employee)
            .Select(x => new VacationsRequestReview
            {
                Id = x.Id,
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

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
