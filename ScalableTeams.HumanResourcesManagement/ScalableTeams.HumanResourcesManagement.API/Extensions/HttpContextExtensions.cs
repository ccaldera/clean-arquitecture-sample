using Microsoft.AspNetCore.Http;
using System;

namespace ScalableTeams.HumanResourcesManagement.API.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext httpContext)
    {
        var userName = httpContext?.User?.Identity?.Name;

        if (userName is null)
        {
            throw new UnauthorizedAccessException();
        }

        var userId = new Guid(userName);

        if(userId == Guid.Empty)
        {
            throw new UnauthorizedAccessException();
        }

        return userId;
    }
}
