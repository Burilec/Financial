using Burile.Financial.Api.Features.RetrieveEtfs;
using Burile.Financial.TwelveData.Clients;
using Burile.Financial.TwelveData.Clients.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Burile.Financial.Tests.UnitTests.Features.RetrieveEtfs;

public sealed class RetrieveEtfsServiceCollectionExtensionTests
{
    [Fact]
    public void AddRetrieveEtfsServices_ShouldContainServices()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();

        //Act
        serviceCollection.AddRetrieveEtfsServices();

        //Assert
        serviceCollection.Should().OnlyContain(static descriptor => descriptor.ServiceType == typeof(ITwelveDataClient));
        serviceCollection.Should().OnlyContain(static descriptor => descriptor.ImplementationType == typeof(TwelveDataClient));
    }
}