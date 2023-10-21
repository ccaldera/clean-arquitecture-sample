using ScalableTeams.HumanResourcesManagement.Application.Features.EmployeeRequestsVacations.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Common.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.Common.ValueObjects;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.DomainEvents;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.EmployeeRequestsVacations;

public class VacationsRequestService : IFeatureService<VacationsRequestInput>
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IEventDispatcher eventDispatcher;

    public VacationsRequestService(
        IEmployeesRepository employeesRepository,
        IVacationsRequestRepository vacationsRequestRepository,
        IEventDispatcher eventDispatcher)
    {
        this.employeesRepository = employeesRepository;
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.eventDispatcher = eventDispatcher;
    }

    public async Task Execute(VacationsRequestInput input, CancellationToken cancellationToken)
    {
        var employee = await employeesRepository.Get(input.EmployeeId)
            ?? throw new ResourceNotFoundException($"The requested employee id {input.EmployeeId} does not exists");

        _ = employee.ManagerId
            ?? throw new BusinessLogicException("The employee is not assigned to any manager.");

        var vacationsRequest = new VacationRequest(employee, input.Dates);

        ValidateAndThrow(vacationsRequest);

        await vacationsRequestRepository.Insert(vacationsRequest);

        await vacationsRequestRepository.SaveChanges();

        var vacationRequestCreatedEvent = new VacationRequestCreated(vacationsRequest);

        await eventDispatcher.Dispatch(vacationRequestCreatedEvent, cancellationToken);
    }

    private static void ValidateAndThrow(VacationRequest target)
    {
        var errors = new List<BusinessRuleError>();

        if (target.EmployeeId == Guid.Empty)
        {
            errors.Add(new BusinessRuleError(nameof(target.EmployeeId), "EmployeeId cannot be empty."));
        }

        if (!target.Dates.Any())
        {
            errors.Add(new BusinessRuleError(nameof(target.Dates), "The dates list cannot be empty."));
        }

        if (target.Dates.Any(x => x.Date <= DateTime.UtcNow.Date))
        {
            errors.Add(new BusinessRuleError(nameof(target.Dates), "Dates must be greater than today."));
        }

        if (target.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days < 14))
        {
            errors.Add(new BusinessRuleError(nameof(target.Dates), "You cannot request vacations for the next 14 days."));
        }

        if (target.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days > 365))
        {
            errors.Add(new BusinessRuleError(nameof(target.Dates), "You cannot request vacations with a date greater than 120 days."));
        }

        if (target.Dates.GroupBy(x => x.Date).Any(x => x.Count() > 1))
        {
            errors.Add(new BusinessRuleError(nameof(target.Dates), "There are some duplicated dates in the request."));
        }

        if (errors.Any())
        {
            throw new BusinessLogicExceptions(errors);
        }
    }
}
