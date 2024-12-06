using System.ComponentModel;

namespace caju_authorizer_domain.Authorizer.Enums
{
  public enum ResponseCodes
  {
    [Description("51")]
    Rejected,
    [Description("00")]
    Approved,
    [Description("07")]
    Error
   }
}
