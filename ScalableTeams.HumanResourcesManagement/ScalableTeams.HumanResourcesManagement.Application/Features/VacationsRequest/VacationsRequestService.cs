using ScalableTeams.HumanResourcesManagement.Application.Common;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestService : IFeatureService<VacationsRequestInput, OperationResponses>
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IVacationsRequestRepository vacationsRequestRepository;
    private readonly IValidator<VacationsRequestInput> validator;

    public VacationsRequestService(
        IEmployeesRepository employeesRepository,
        IVacationsRequestRepository vacationsRequestRepository,
        IValidator<VacationsRequestInput> validator)
    {
        this.employeesRepository = employeesRepository;
        this.vacationsRequestRepository = vacationsRequestRepository;
        this.validator = validator;
    }

    public async Task<OperationResponses> Execute(VacationsRequestInput input, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(input);

        var employee = await employeesRepository.GetEmployeeAndManagerByEmployeeId(input.EmployeeId);

        if (employee is null)
        {
            throw new ResourceNotFoundException($"The requested employee id {input.EmployeeId} does not exists");
        }

        var vacationsRequest = VacationRequest.NewRequest(employee, input.Dates);

        await vacationsRequestRepository.Insert(vacationsRequest);

        await vacationsRequestRepository.SaveChanges();

        return OperationResponses.Empty;
    }
}
