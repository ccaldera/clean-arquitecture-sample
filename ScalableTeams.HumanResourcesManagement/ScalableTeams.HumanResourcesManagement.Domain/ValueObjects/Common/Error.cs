namespace ScalableTeams.HumanResourcesManagement.Domain.ValueObjects.Common;

public class Error
{
    public Error(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}
