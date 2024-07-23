using System.Text.Json.Serialization;

namespace SalesforceAuthExample;
public class SalesforceTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
    [JsonPropertyName("signature")]
    public string Signature { get; set; } = null!;
    [JsonPropertyName("instance_url")]
    public string instance_url { get; set; } = null!;
    [JsonPropertyName("id")]
    public string id { get; set; } = null!;
    [JsonPropertyName("token_type")]
    public string token_type { get; set; } = null!;
    [JsonPropertyName("issued_at")]
    public string issued_at { get; set; } = null!;
}
