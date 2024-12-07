using System.Text.RegularExpressions;

namespace caju_authorizer_domain.Authorizer.Helpers
{
  public static class MerchantHelper
  {
    public static string NormalizeName(string name)
    {
      if (name == null) return name;

      return Regex.Replace(name.Trim(), @"\s+", " ");
    }
  }
}
