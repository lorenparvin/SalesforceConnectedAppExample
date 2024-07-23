using System;
using System.Linq;
using System.Text;

namespace SalesforceAuthExample
{
	public class SalesforceClaimUpdateService : ISalesforceClaimUpdateService
	{

        private readonly HttpClient _httpClient;
        public SalesforceClaimUpdateService(IHttpClientFactory httpClientFactory)
		{
            _httpClient = httpClientFactory.CreateClient();
        }


        public async Task<string> updateClaimInSalesforce(string claimUpdate, string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string claimUpdateURI = $"https://zlinekitchen--aigbox.sandbox.my.salesforce.com/services/apexrest/ClaimUpdate";


            HttpContent content = new StringContent(claimUpdate, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await _httpClient.PostAsync(claimUpdateURI, content);

            string postResult = await postResponse.Content.ReadAsStringAsync();

            return postResult;
        }
    }
}

