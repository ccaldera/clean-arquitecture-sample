using ScalableTeams.HumanResourcesManagement.API.Security.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace ScalableTeams.HumanResourcesManagement.API.Security.Services;

public interface IJwtService
{
    GetTokenResponse GenerateAuthToken(IEnumerable<Claim> claims);
}
