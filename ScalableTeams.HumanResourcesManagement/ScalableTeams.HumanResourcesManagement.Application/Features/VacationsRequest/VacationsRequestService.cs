using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestService : IFeatureService<VacationsRequestInput>
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IManagerNotificationService managerNotificationService;

    public VacationsRequestService(
        IEmployeesRepository employeesRepository,
        IVacationsRequestRepository vacationsRequestRepository,
        IManagerNotificationService managerNotificationService)
    {
        this.employeesRepository = employeesRepository;
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.managerNotificationService = managerNotificationService;
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

        await managerNotificationService.SendNewVacationRequestNotification(
            employee.ManagerId.Value, 
            vacationsRequest, 
            cancellationToken);
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
