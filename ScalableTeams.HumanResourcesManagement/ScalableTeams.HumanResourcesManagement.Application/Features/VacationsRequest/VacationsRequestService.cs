using ScalableTeams.HumanResourcesManagement.Application.Common;
using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestService : IFeatureService<VacationsRequestInput, VacationsRequestResult>
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

    public async Task<VacationsRequestResult> Execute(VacationsRequestInput input, CancellationToken cancellationToken)
    {
        var employee = await employeesRepository.GetEmployeeAndManagerByEmployeeId(input.EmployeeId);

        if (employee is null)
        {
            throw new ResourceNotFoundException($"The requested employee id {input.EmployeeId} does not exists");
        }

        var vacationsRequest = VacationRequest.Create(employee, input.Dates);

        await vacationsRequestRepository.Insert(vacationsRequest);

        await vacationsRequestRepository.SaveChanges();

        return new VacationsRequestResult();
    }
}
