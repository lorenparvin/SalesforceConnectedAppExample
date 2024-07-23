using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SalesforceAuthExample;

public class Program
{
    public static async Task Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddHttpClient();

        var clientFactory = services.BuildServiceProvider()
            .GetRequiredService<IHttpClientFactory>();

        ISalesforceTokenService salesforceTokenService = new SalesforceTokenService(Microsoft.Extensions.Options.Options.Create<SalesforceOptions>(
            new SalesforceOptions()
            { 
                ConsumerKey = "YOUR_CONSUMER_KEY",
                ConsumerSecret = "YOUR_CONSUMER_SECRET",
                BaseUri = "https://zlinekitchen--aigbox.sandbox.my.salesforce.com/",
                TokenExpirationMinutes = "15",
            }),
            clientFactory
            );

        var token = await salesforceTokenService.GetToken();

        Console.WriteLine("token - " + token.Token);




        String soqlQuery = "SELECT Id FROM AIG_Product_Claim__c LIMIT 10";
        ISalesforceQueryService salesforceQueryService = new SalesforceQueryService(clientFactory);
        var queryResults = await salesforceQueryService.querySalesforce(soqlQuery, token.Token);

        Console.WriteLine("query results - " + queryResults);


        var claimUpdate = new
        {
            claimNumber = "Claim-000071",
            scheduledServiceDate = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd"),
            scheduledServiceStartTime = "9",
            scheduledServiceStopTime = "11",
            serviceCancelled = false,
            serviceScheduled = true,
            servicerName = "John Doe",
            servicerType = "Plumber",
            servicerPhone = "555-1234",
            servicerId = "1234567859",
            servicerAddress = "123 Main St",
            servicerAddress2 = "addy 2",
            servicerCity = "Anytown",
            servicerState = "CA",
            servicerPostalCode = "90210",
            servicerCountry = "USA"
        };


        string jsonPayload = System.Text.Json.JsonSerializer.Serialize(claimUpdate);

        ISalesforceClaimUpdateService salesforceClaimUpdateService = new SalesforceClaimUpdateService(clientFactory);

        var updateResults = await salesforceClaimUpdateService.updateClaimInSalesforce(jsonPayload, token.Token);
        Console.WriteLine(updateResults);

    }
}
