using FluentValidation;
using ScalableTeams.HumanResourcesManagement.API.Security.Models;

namespace ScalableTeams.HumanResourcesManagement.API.Security.Validators;

public class GetTokenRequestValidator : AbstractValidator<GetTokenRequest>
{
    public GetTokenRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
