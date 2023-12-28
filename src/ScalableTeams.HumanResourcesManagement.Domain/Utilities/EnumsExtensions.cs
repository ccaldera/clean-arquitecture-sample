using System.ComponentModel;
using System.Reflection;

namespace ScalableTeams.HumanResourcesManagement.Domain.Utilities;

public static class EnumsExtensions
{
    public static string GetDescription(this Enum en)
    {
        Type type = en.GetType();
        MemberInfo[] memInfo = type.GetMember(en.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        var stringValue = ((DescriptionAttribute)attributes[0]).Description;
        return stringValue;
    }
}
