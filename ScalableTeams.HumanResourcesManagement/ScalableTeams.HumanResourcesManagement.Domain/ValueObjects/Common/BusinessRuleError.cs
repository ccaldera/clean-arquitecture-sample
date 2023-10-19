namespace ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

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
