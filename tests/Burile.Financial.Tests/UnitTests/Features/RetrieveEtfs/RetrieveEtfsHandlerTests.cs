using Bogus;
using Burile.Financial.Api.Features.RetrieveEtfs;
using Burile.Financial.TwelveData.Clients.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Burile.Financial.Tests.UnitTests.Features.RetrieveEtfs;

public sealed class RetrieveEtfsHandlerTests
{
    private readonly RetrieveEtfsHandler _retrieveEtfsHandler;
    private readonly ITwelveDataClient _twelveDataClient;

    public RetrieveEtfsHandlerTests()
    {
        _twelveDataClient = Substitute.For<ITwelveDataClient>();

        _retrieveEtfsHandler = new RetrieveEtfsHandler(_twelveDataClient);
    }

    [Fact]
    public async Task Handle_ShouldBeOk()
    {
        //Arrange
        var retrieveEtfsQuery = new RetrieveEtfsQuery();

        var text = new Faker().Lorem.Text();

        _twelveDataClient.GetEtfsAsync().Returns(text);

        //Act
        var handle = await _retrieveEtfsHandler.Handle(retrieveEtfsQuery);

        //Assert
        handle.Should().Be(text);
    }
}