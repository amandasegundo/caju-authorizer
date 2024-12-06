using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace caju_authorizer_domain.Authorizer.Dtos
{
  public class AuthorizerRequest
  {
    [JsonProperty("account")]
    public required string Account {  get; set; }
    [JsonProperty("totalAmount")]
    public required decimal TotalAmount { get; set; }
    [JsonProperty("mcc")]
    public required string MCC { get; set; }
    [JsonProperty("merchant")]
    public required string Merchant { get; set; }
  }
}
