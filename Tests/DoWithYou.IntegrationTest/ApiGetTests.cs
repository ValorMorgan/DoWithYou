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
    public class ApiGetTests : IDisposable
    {
        #region VARIABLES
        private readonly TestServer _server;
        private readonly HttpClient _client;
        #endregion

        #region CONSTRUCTORS
        public ApiGetTests()
        {
            _server = GetServer();
            _client = _server.CreateClient();
        }
        #endregion

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(999)]
        public async Task Get_Users_Returns_Success_And_Data(int id) =>
            WriteResponseToConsole(
                await GetEnsuredSuccessResponseString("/api/users", id)
            );

        public void Dispose()
        {
            try
            {
                _client?.Dispose();
                _server?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #region PRIVATE
        private TestServer GetServer() =>
            new TestServer(
                DoWithYou.Shared.Core.WebHost.GetWebHostBuilder(null, typeof(DoWithYou.API.Startup))
            );
    
        private async Task<string> GetEnsuredSuccessResponseString(string endpoint, params object[] args)
        {
            var response = args?.Any() ?? false ?
                await _client.GetAsync($"{endpoint}/{string.Join("/", args)}") :
                await _client.GetAsync(endpoint);
                
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.And.Not.Empty);
            
            return responseString;
        }

        private void WriteResponseToConsole(string responseString) =>
            Console.WriteLine($"\nResult:\n{responseString}");
        #endregion
    }
}