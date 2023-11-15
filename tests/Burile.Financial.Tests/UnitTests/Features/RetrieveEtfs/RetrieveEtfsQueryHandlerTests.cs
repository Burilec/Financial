using Bogus;
using Burile.Financial.Api.Features.RetrieveEtfs;
using Burile.Financial.TwelveData.Clients.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Burile.Financial.Tests.UnitTests.Features.RetrieveEtfs;

public sealed class RetrieveEtfsQueryHandlerTests
{
    private readonly RetrieveEtfsQueryHandler _retrieveEtfsQueryHandler;
    private readonly ITwelveDataClient _twelveDataClient;

    public RetrieveEtfsQueryHandlerTests()
    {
        _twelveDataClient = Substitute.For<ITwelveDataClient>();

        _retrieveEtfsQueryHandler = new RetrieveEtfsQueryHandler(_twelveDataClient);
    }

    [Fact]
    public async Task Handle_ShouldBeOk()
    {
        //Arrange
        var retrieveEtfsQuery = new RetrieveEtfsQuery();

        var text = new Faker().Lorem.Text();

        _twelveDataClient.GetEtfsAsync().Returns(text);

        //Act
        var handle = await _retrieveEtfsQueryHandler.Handle(retrieveEtfsQuery);

        //Assert
        handle.Should().Be(text);
    }
}