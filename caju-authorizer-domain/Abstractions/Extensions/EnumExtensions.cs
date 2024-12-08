using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace caju_authorizer_domain.Abstractions.Extensions
{
  [ExcludeFromCodeCoverage]
  public static class EnumExtensions
  {
    public static string GetDescription(this Enum value)
    {
      FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
      DescriptionAttribute descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
      return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description;
    }
  }
}