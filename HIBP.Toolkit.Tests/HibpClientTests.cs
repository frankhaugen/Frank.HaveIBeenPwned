using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

//[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace HIBP.Toolkit.Tests
{
    public class HibpClientTests
    {
        private IHttpClientFactory subHttpClientFactory;
        private ILogger<HibpClient> subLogger;
        private readonly ITestOutputHelper _outputHelper;

        private const string username = "frank@gmail.com";
        private const string password = "Password";
        private const string website = "adobe";
        private const string apiKey = "<<apikey>>";

        public HibpClientTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            subHttpClientFactory = Substitute.For<IHttpClientFactory>();
            subLogger = outputHelper.BuildLoggerFor<HibpClient>();
        }

        private HibpClient CreateHibpClient()
        {
            subHttpClientFactory.CreateClient().Returns(CreateHttpClient());
            return new HibpClient(
                subHttpClientFactory,
                subLogger);
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("hibp-api-key", apiKey);
            client.DefaultRequestHeaders.Add("user-agent", "HIBP.Toolkit");
            client.BaseAddress = new Uri($"https://haveibeenpwned.com/api/v3");

            return client;
        }

        [Fact]
        public async Task CheckPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = CreateHibpClient();
            int threshold = 0;

            // Act
            var result = await hibpClient.CheckPassword(password, threshold);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task GetPasswordDetails_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetPasswordDetails(password);

            // Assert
            Assert.True(result.IsPwned);
        }

        [Fact]
        public async Task CheckForPastes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.CheckForPastes(username);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetPastes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetPastes(username);
            _outputHelper.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task CheckSiteForBreaches_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.CheckSiteForBreaches(website);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetBreachesForSite_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetBreachesForSite(website);

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetBreachesForAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetBreachesForAccount(username);

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetAllBreaches_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetAllBreaches();

            // Assert
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetDataClasses_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetDataClasses();

            // Assert
            Assert.True(result.Any());
        }
    }
}