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
            employeesRepository ??= new EmployeesRepository(dbContext);
            return employeesRepository;
        }
    }

    public IDepartmentsRepository DepartmentsRepository
    {
        get
        {
            departmentsRepository ??= new DepartmentsRepository(dbContext);
            return departmentsRepository;
        }
    }

    public IVacationsRequestRepository VacationsRequestRepository
    {
        get
        {
            vacationsRequestRepository ??= new VacationsRequestRepository(dbContext);
            return vacationsRequestRepository;
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
