using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Models;
using ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Services;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.Security;

public class GetTokenEndpoint : IEndpoint
{
    private readonly IValidator<GetTokenRequest> validator;

    public GetTokenEndpoint(IValidator<GetTokenRequest> validator)
    {
        this.validator = validator;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/get-token",
            async (
                [FromServices] JwtService jwtService,
                [FromServices] IEmployeesRepository employeesRepository,
                [FromBody] GetTokenRequest request,
                CancellationToken cancellationToken) =>
            {
                validator.ValidateAndThrow(request);

                var employee = await employeesRepository.GetUserByEmailAndPassword(
                    request.Username, 
                    request.Password);

                if(employee == null)
                {
                    return Results.Unauthorized();
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

                var token = jwtService.GenerateAuthToken(claims);

                return Results.Ok(token);
            })
        .Produces<GetTokenResponse>()
        .WithTags("Managers Endpoints");
    }
}