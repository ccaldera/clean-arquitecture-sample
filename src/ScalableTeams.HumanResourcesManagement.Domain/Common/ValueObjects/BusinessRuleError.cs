namespace ScalableTeams.HumanResourcesManagement.Domain.Common.ValueObjects;

public class BusinessRuleError
{
    public BusinessRuleError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}
