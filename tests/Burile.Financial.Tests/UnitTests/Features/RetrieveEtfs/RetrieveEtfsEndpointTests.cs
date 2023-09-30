using Burile.Financial.Api.Features.RetrieveEtfs;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Burile.Financial.Tests.UnitTests.Features.RetrieveEtfs;

public sealed class RetrieveEtfsEndpointTests
{
    private readonly RetrieveEtfsEndpoint _retrieveEtfsEndpoint = new();

    [Fact]
    public async Task GetAsync_ShouldBeOk()
    {
        //Arrange
        var mediator = Substitute.For<IMediator>();
        mediator.Send(Arg.Any<RetrieveEtfsQuery>()).Returns("etfs");

        //Act
        var result = (OkObjectResult)await _retrieveEtfsEndpoint.GetAsync(mediator);

        //Assert
        mediator.Received();
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(200);
        result.Value.Should().NotBeNull();
        result.Value.Should().Be("etfs");
    }
}