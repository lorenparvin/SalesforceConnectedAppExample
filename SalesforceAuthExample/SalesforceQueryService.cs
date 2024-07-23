using System;
using Azure.Core;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace SalesforceAuthExample
{
	public class SalesforceQueryService : ISalesforceQueryService
    {

        private readonly HttpClient _httpClient;
        public SalesforceQueryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        
        public async Task<string> querySalesforce(string soqlQuery, string token)
		{

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            
            string requestUri = $"https://zlinekitchen--aigbox.sandbox.my.salesforce.com/services/data/v60.0/query/?q={System.Net.WebUtility.UrlEncode(soqlQuery)}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

          
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;

		}
	}
}

