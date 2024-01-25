using System.Net;
using Burile.Financial.Tests.IntegrationTests.Features.RetrieveEtfs.Fixtures;
using FluentAssertions;
using Xunit;

namespace Burile.Financial.Tests.IntegrationTests.Features.RetrieveEtfs;

[Collection("RetrieveEtfsBackgroundServicesTest")]
public sealed class RetrieveEtfsTests(RetrieveEtfsBackgroundServicesFixture retrieveEtfsBackgroundServicesFixture)
{
    [Fact]
    public async Task RetrieveEtfs()
    {
        //Arrange

        //Act
        var responseMessage = await retrieveEtfsBackgroundServicesFixture.HttpClient.GetAsync("api/etfs");

        var content = await responseMessage.Content.ReadAsStringAsync();

        //Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        responseMessage.IsSuccessStatusCode.Should().BeTrue();
        content.Should()
               .Contain("{\"symbol\":\"VT\",\"name\":\"Vanguard Total World Stock Index Fund ETF Shares\",\"currency\":\"USD\",\"exchange\":\"NYSE\",\"mic_code\":\"ARCX\",\"country\":\"United States\"}");
    }
}