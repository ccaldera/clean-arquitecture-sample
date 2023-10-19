using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext httpContext)
    {
        var userName = httpContext?.User?.Claims
            .FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Sid)?
            .Value;

        if (userName is null)
        {
            throw new UnauthorizedAccessException();
        }

        var userId = new Guid(userName);

        if (userId == Guid.Empty)
        {
            throw new UnauthorizedAccessException();
        }

        return userId;
    }
}
