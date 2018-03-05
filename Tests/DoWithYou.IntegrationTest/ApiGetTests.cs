using System;
using System.Net.Http;
using System.Threading.Tasks;
using DoWithYou.API;
using DoWithYou.Shared.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DoWithYou.IntegrationTest
{
    [TestFixture]
    public class ApiGetTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiGetTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(config => config.AddInMemoryCollection(WebHost.GetInMemorySettings()))
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private async Task<string> GetEnsuredSuccessResponseString(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private void WriteResponseToConsole(string responseString)
        {
            Console.WriteLine("Result:");
            Console.WriteLine(responseString);
        }

        [Test]
        public async Task Get_Users_Returns_Success_And_Data()
        {
            string responseString = await GetEnsuredSuccessResponseString("/api/users");

            Assert.That(responseString, Is.Not.Null.And.Not.Empty);

            WriteResponseToConsole(responseString);
        }
    }
}