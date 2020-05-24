using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Frank.HaveIBeenPwned.Tests
{
    public class HibpClientTests
    {
        private ILogger<HaveIBeenPwnedClient> subLogger;
        private IOptions<HaveIBeenPwnedConfiguration> subOptions;
        private readonly ITestOutputHelper _outputHelper;

        private const string username = "frank@gmail.com";
        private const string password = "Password";
        private const string website = "adobe";

        public HibpClientTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            subOptions = Options.Create(new HaveIBeenPwnedConfiguration()
            {
                ApiKey = "#{APIKEY}#"
            });
            subLogger = outputHelper.BuildLoggerFor<HaveIBeenPwnedClient>();
        }

        private HaveIBeenPwnedClient CreateHibpClient()
        {
            return new HaveIBeenPwnedClient(new HaveIBeenPwnedConfiguration());
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