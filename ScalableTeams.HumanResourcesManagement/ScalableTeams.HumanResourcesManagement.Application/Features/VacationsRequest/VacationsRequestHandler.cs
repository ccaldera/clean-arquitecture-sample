using MediatR;
using ScalableTeams.HumanResourcesManagement.Domain.Exceptions;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Application.Features.VacationsRequest;

public class VacationsRequestHandler : IRequestHandler<VacationsRequestCommand, VacationsRequestResult>
{
    private readonly IEmployeeRepository employeeRepository;

    public VacationsRequestHandler(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async Task<VacationsRequestResult> Handle(VacationsRequestCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployeeAndManagerByEmployeeId(request.EmployeeId);

        if(employee is null)
        {
            throw new ResourceNotFoundException($"The requested employee id {request.EmployeeId} does not exists");
        }

        var result = new VacationsRequestResult();
        result.SetSuccess();

        return result;
    }
}
