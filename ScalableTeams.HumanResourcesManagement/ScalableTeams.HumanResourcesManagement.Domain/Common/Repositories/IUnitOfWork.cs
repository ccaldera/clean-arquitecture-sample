using ScalableTeams.HumanResourcesManagement.Domain.Departments.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Domain.Common.Repositories;

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
