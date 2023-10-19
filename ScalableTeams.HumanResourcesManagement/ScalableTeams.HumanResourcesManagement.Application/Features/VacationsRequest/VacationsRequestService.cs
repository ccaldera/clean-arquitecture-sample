using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestService : IFeatureService<VacationsRequestInput>
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IVacationsRequestRepository vacationsRequestRepository;

    public VacationsRequestService(
        IEmployeesRepository employeesRepository,
        IVacationsRequestRepository vacationsRequestRepository)
    {
        this.employeesRepository = employeesRepository;
        this.vacationsRequestRepository = vacationsRequestRepository;
    }

    public async Task Execute(VacationsRequestInput input, CancellationToken cancellationToken)
    {
        var employee = await employeesRepository.Get(input.EmployeeId)
            ?? throw new ResourceNotFoundException($"The requested employee id {input.EmployeeId} does not exists");

        var vacationsRequest = new VacationRequest(employee, input.Dates);

        ValidateAndThrow(vacationsRequest);

        await vacationsRequestRepository.Insert(vacationsRequest);

        await vacationsRequestRepository.SaveChanges();
    }

    private static void ValidateAndThrow(VacationRequest target)
    {
        var errors = new List<Error>();

        if (target.EmployeeId == Guid.Empty)
        {
            errors.Add(new Error(nameof(target.EmployeeId), "EmployeeId cannot be empty."));
        }

        if (!target.Dates.Any())
        {
            errors.Add(new Error(nameof(target.Dates), "The dates list cannot be empty."));
        }

        if (target.Dates.Any(x => x.Date <= DateTime.UtcNow.Date))
        {
            errors.Add(new Error(nameof(target.Dates), "Dates must be greater than today."));
        }

        if (target.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days < 14))
        {
            errors.Add(new Error(nameof(target.Dates), "You cannot request vacations for the next 14 days."));
        }

        if (target.Dates.Any(x => (x.Date - DateTime.UtcNow.Date).Days > 365))
        {
            errors.Add(new Error(nameof(target.Dates), "You cannot request vacations with a date greater than 120 days."));
        }

        if (target.Dates.GroupBy(x => x.Date).Any(x => x.Count() > 1))
        {
            errors.Add(new Error(nameof(target.Dates), "There are some duplicated dates in the request."));
        }

        if (errors.Any())
        {
            throw new BusinessLogicExceptions(errors);
        }
    }
}
