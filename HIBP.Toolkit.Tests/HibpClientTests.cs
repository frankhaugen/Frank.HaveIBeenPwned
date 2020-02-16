using HIBP.Toolkit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HIBP.Toolkit.Tests
{
    public class HibpClientTests
    {
        private IHttpClientFactory subHttpClientFactory;
        private ILogger<HibpClient> subLogger;

        public HibpClientTests()
        {
            subHttpClientFactory = Substitute.For<IHttpClientFactory>();
            subLogger = Substitute.For<ILogger<HibpClient>>();
        }

        private HibpClient CreateHibpClient()
        {
            return new HibpClient(
                subHttpClientFactory,
                subLogger);
        }

        [Fact]
        public async Task CheckPassword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string password = null;
            int threshold = 0;

            // Act
            var result = await hibpClient.CheckPassword(
                password,
                threshold);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetPasswordDetails_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string password = null;

            // Act
            var result = await hibpClient.GetPasswordDetails(
                password);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task CheckForPastes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string username = null;

            // Act
            var result = await hibpClient.CheckForPastes(
                username);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetPastes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string username = null;

            // Act
            var result = await hibpClient.GetPastes(
                username);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task CheckSiteForBreaches_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string website = null;

            // Act
            var result = await hibpClient.CheckSiteForBreaches(
                website);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetBreachesForSite_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string website = null;

            // Act
            var result = await hibpClient.GetBreachesForSite(
                website);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetBreachesForAccount_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();
            string account = null;

            // Act
            var result = await hibpClient.GetBreachesForAccount(
                account);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAllBreaches_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetAllBreaches();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetDataClasses_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hibpClient = this.CreateHibpClient();

            // Act
            var result = await hibpClient.GetDataClasses();

            // Assert
            Assert.True(false);
        }
    }
}