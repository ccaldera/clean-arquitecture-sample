using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ScalableTeams.HumanResourcesManagement.API.Security.Models;
using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Enums;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Repositories;
using ScalableTeams.HumanResourcesManagement.Domain.Utilities;

namespace ScalableTeams.HumanResourcesManagement.API.Security.Services;

public class GetTokenRequestService : IFeatureService<GetTokenRequest, GetTokenResponse?>
{
    private readonly IJwtService jwtService;
    private readonly IEmployeesRepository employeesRepository;

    public GetTokenRequestService(
        IJwtService jwtService,
        IEmployeesRepository employeesRepository)
    {
        this.jwtService = jwtService;
        this.employeesRepository = employeesRepository;
    }

    public async Task<GetTokenResponse?> Execute(GetTokenRequest input, CancellationToken cancellationToken)
    {
        var employee = await employeesRepository.GetUserByEmailAndPassword(
                    input.Username,
                    input.Password);

        if (employee == null)
        {
            return null;
        }

        var isManager = await employeesRepository.IsManager(employee.Id);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, employee.Id.ToString()),
            new Claim(ClaimTypes.GivenName, employee.Name),
            new Claim(ClaimTypes.Surname, employee.LastName),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Department.Name)
        };

        if (employee.Department.Name == DepartmentType.HumanResources.GetDescription())
        {
            claims.Add(new Claim(ClaimTypes.Role, SecurityRoles.HumanResourcesRole));
        }

        if (isManager)
        {
            claims.Add(new Claim(ClaimTypes.Role, SecurityRoles.ManagerRole));
        }

        return jwtService.GenerateAuthToken(claims);
    }
}
