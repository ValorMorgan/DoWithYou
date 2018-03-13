using System;
using System.Linq;
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
        private TestServer _server;
        private  HttpClient _client;

        private TestServer Server
        {
            get 
            {
                if (_server == null)
                    InitializeServerAndClient(); 
                return _server;
            }
        }

        private HttpClient Client 
        {
            get
            {
                if (_client == null)
                    InitializeServerAndClient();
                return _client;
            }
        }

        public ApiGetTests()
        {
            InitializeServerAndClient();
        }

        private void InitializeServerAndClient()
        {
            _server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(config => config.AddInMemoryCollection(WebHost.GetInMemorySettings()))
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private async Task<string> GetEnsuredSuccessResponseString(string endpoint, params object[] args)
        {
            var response = args?.Any() ?? false ?
                await Client.GetAsync($"{endpoint}/{string.Join("/", args)}") :
                await Client.GetAsync(endpoint);
                
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.And.Not.Empty);
            
            return responseString;
        }

        private void WriteResponseToConsole(string responseString) =>
            Console.WriteLine($"\nResult:\n{responseString}");

        [Test]
        public async Task Get_Users_Returns_Success_And_Data()
        {
            string responseString = await GetEnsuredSuccessResponseString("/api/users", 1);

            WriteResponseToConsole(responseString);
        }
    }
}