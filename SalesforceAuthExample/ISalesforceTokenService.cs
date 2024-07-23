using Azure.Core;

namespace SalesforceAuthExample;

public interface ISalesforceTokenService
{
    Task<AccessToken> GetToken();
}
