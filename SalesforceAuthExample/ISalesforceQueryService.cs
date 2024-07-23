using Azure.Core;

namespace SalesforceAuthExample;

public interface ISalesforceQueryService
{
    Task<string> querySalesforce(string soqlQuery, string token);
}