using ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Models;
using ScalableTeams.HumanResourcesManagement.Application.Features;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Services;

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
            new Claim(ClaimTypes.Name, employee.Name),
            new Claim(ClaimTypes.Surname, employee.LastName),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Department.Name)
        };

        if (isManager)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Manager"));
        }

        return jwtService.GenerateAuthToken(claims);
    }
}
