using Microsoft.EntityFrameworkCore.Storage;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HumanResourcesManagementContext dbContext;
    private IDbContextTransaction? transaction;
    private IEmployeesRepository? employeesRepository;
    private IDepartmentsRepository? departmentsRepository;
    private IVacationsRequestRepository? vacationsRequestRepository;

    public UnitOfWork(HumanResourcesManagementContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEmployeesRepository EmployeesRepository
    {
        get
        {
            return employeesRepository ??= new EmployeesRepository(dbContext);
        }
    }

    public IDepartmentsRepository DepartmentsRepository
    {
        get
        {
            return departmentsRepository ??= new DepartmentsRepository(dbContext);
        }
    }

    public IVacationsRequestRepository VacationsRequestRepository
    {
        get
        {
            return vacationsRequestRepository ??= new VacationsRequestRepository(dbContext);
        }
    }


    public async Task BeginTransaction()
    {
        transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        await transaction!.CommitAsync();
    }

    public async Task RollBackTransaction()
    {
        await transaction!.RollbackAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
