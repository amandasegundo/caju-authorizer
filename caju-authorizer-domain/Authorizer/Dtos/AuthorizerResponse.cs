using Newtonsoft.Json;

namespace caju_authorizer_domain.Authorizer.Dtos
{
  public class AuthorizerResponse
  {
    [JsonProperty("code")]
    public required string Code { get; set; }
  }
}
