using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScalableTeams.HumanResourcesManagement.API.Configuration;
using ScalableTeams.HumanResourcesManagement.API.Security.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScalableTeams.HumanResourcesManagement.API.Security.Services;

public class JwtService : IJwtService
{
    private readonly JwtConfiguration jwtConfiguration;

    public JwtService(IOptions<JwtConfiguration> jwtConfiguration)
    {
        this.jwtConfiguration = jwtConfiguration.Value;
    }

    public GetTokenResponse GenerateAuthToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtConfiguration.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return new GetTokenResponse
        {
            Token = tokenHandler.WriteToken(token),
            ExpirationTimeInMinutes = jwtConfiguration.ExpirationInMinutes
        };
    }
}
