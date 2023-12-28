using System.Collections.Generic;
using System.Security.Claims;
using ScalableTeams.HumanResourcesManagement.API.Security.Models;

namespace ScalableTeams.HumanResourcesManagement.API.Security.Services;

public interface IJwtService
{
    GetTokenResponse GenerateAuthToken(IEnumerable<Claim> claims);
}
