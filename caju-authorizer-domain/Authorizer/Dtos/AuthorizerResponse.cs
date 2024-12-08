using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace caju_authorizer_domain.Authorizer.Dtos
{
  [ExcludeFromCodeCoverage]
  public class AuthorizerResponse
  {
    [JsonProperty("code")]
    public required string Code { get; set; }
  }
}
