using ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest.Models;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Rules;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestService : IFeatureService<VacationsRequestInput>
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IRuleValidator<VacationRequest> vacationRequestRuleValidator;

    public VacationsRequestService(
        IEmployeesRepository employeesRepository,
        IVacationsRequestRepository vacationsRequestRepository,
        IRuleValidator<VacationRequest> vacationRequestRuleValidator)
    {
        this.employeesRepository = employeesRepository;
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.vacationRequestRuleValidator = vacationRequestRuleValidator;
    }

    public async Task Execute(VacationsRequestInput input, CancellationToken cancellationToken)
    {
        var employee = await employeesRepository.Get(input.EmployeeId)
            ?? throw new ResourceNotFoundException($"The requested employee id {input.EmployeeId} does not exists");

        var vacationsRequest = new VacationRequest(employee, input.Dates);

        vacationRequestRuleValidator.ValidateAndThrow(vacationsRequest);

        await vacationsRequestRepository.Insert(vacationsRequest);

        await vacationsRequestRepository.SaveChanges();
    }
}
