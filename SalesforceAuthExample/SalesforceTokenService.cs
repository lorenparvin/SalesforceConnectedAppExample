using Azure.Core;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace SalesforceAuthExample;
public class SalesforceTokenService : ISalesforceTokenService
{
    private readonly SalesforceOptions _salesforceOptions;
    private readonly HttpClient _httpClient;
    public SalesforceTokenService(IOptions<SalesforceOptions> salesforceOptions, IHttpClientFactory httpClientFactory)
    {
        _salesforceOptions = salesforceOptions.Value;
        _httpClient = httpClientFactory.CreateClient();
    }

    private AccessToken _token;
    public async Task<AccessToken> GetToken()
    {
        if (IsExpired)
        {
            await GenerateNewToken();
        }

        return _token;
    }

    private async Task GenerateNewToken()
    {
        var response = await _httpClient.PostAsync($"{_salesforceOptions.BaseUri}services/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            { "grant_type", "client_credentials" },
            { "client_id", _salesforceOptions.ConsumerKey },
            { "client_secret", _salesforceOptions.ConsumerSecret },
        }));

        SalesforceTokenResponse tokenResponse = JsonSerializer.Deserialize<SalesforceTokenResponse>(await response.Content.ReadAsStringAsync()) ?? throw new NullReferenceException("Could not Deserialize Salesforce Token Response");

        _token = new AccessToken(tokenResponse.AccessToken, DateTime.UtcNow.AddMinutes(int.Parse(_salesforceOptions.TokenExpirationMinutes)));
    }

    private bool IsExpired => _token.ExpiresOn < DateTimeOffset.UtcNow;

}