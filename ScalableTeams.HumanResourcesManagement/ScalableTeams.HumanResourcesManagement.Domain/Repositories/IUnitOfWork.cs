namespace ScalableTeams.HumanResourcesManagement.Domain.Repositories;

public interface IUnitOfWork
{
    IEmployeesRepository EmployeesRepository { get; }
    IDepartmentsRepository DepartmentsRepository { get; }
    IVacationsRequestRepository VacationsRequestRepository { get; }

    Task BeginTransaction();
    Task RollBackTransaction();
    Task CommitTransaction();
    Task SaveChanges();
}
