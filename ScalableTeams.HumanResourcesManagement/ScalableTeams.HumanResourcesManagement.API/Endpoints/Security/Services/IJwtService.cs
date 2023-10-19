using ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace ScalableTeams.HumanResourcesManagement.API.Endpoints.Security.Services;

public interface IJwtService
{
    GetTokenResponse GenerateAuthToken(IEnumerable<Claim> claims);
}
