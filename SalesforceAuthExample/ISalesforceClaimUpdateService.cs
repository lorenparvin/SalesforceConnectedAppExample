using System;
namespace SalesforceAuthExample;

public interface ISalesforceClaimUpdateService
{
    Task<string> updateClaimInSalesforce(string claimUpdate, string token);
}

